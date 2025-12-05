using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System.Linq;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly ICustomerService _customerService;
        private readonly IOrderLineService _orderLineService;

        public OrderService(
            IProductService productService,
            IDiscountService discountService,
            ICustomerService customerService,
            IOrderLineService orderLineService)
        {
            _productService = productService;
            _discountService = discountService;
            _customerService = customerService;
            _orderLineService = orderLineService;
        }

        public Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
            => _productService.GetProductsByIdsAsync(productIds);

        public async Task<Order> CreateOrUpdateOrderForBookingAsync(int bookingId)
        {
            // Load booking with all necessary related data
            var booking = await _customerService.GetBookingWithTreatmentsAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            var productsList = new List<Product>();

            // 1️⃣ Add booked treatments
            if (booking.Treatments != null)
            {
                productsList.AddRange(
                    booking.Treatments
                           .Where(tb => tb.Treatment != null)
                           .Select(tb => tb.Treatment)!
                );
            }

            // 2️⃣ Add products from TreatmentBookingProducts
            if (booking.Treatments != null)
            {
                foreach (var tb in booking.Treatments)
                {
                    if (tb.TreatmentBookingProducts != null)
                    {
                        foreach (var tbp in tb.TreatmentBookingProducts)
                        {
                            for (int i = 0; i < tbp.NumberOfProducts; i++)
                                productsList.Add(tbp.Product);
                        }
                    }
                }
            }

            if (!productsList.Any())
                throw new Exception("No products or treatments found for this booking");

            // Calculate totals & best discounts
            var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal) =
                await CalculateBestDiscountsAsync(booking.CustomerId, productsList);

            // Get existing order (if any)
            var order = await _customerService.GetOrderByBookingIdAsync(bookingId);

            if (order == null)
            {
                // Create new order
                order = new Order(bookingId, originalTotal, finalTotal, appliedDiscount?.Id ?? 0);
                await _customerService.AddOrderAsync(order);
            }
            else
            {
                // Update existing order
                order.UpdateTotals(originalTotal, finalTotal, appliedDiscount?.Id);
                await _customerService.UpdateOrderAsync(order);
            }

            // Mark booking as completed
            booking.Status = BookingStatus.Completed;
            await _customerService.UpdateBookingAsync(booking);

            return order;
        }


        public async Task<(decimal originalTotal,
                           Discount? appliedDiscount,
                           Discount? loyaltyDiscount,
                           decimal finalTotal)>
            CalculateBestDiscountsAsync(int customerId, List<Product> products)
        {
            var result = await CalculateBestDiscountsPerItemAsync(customerId, products);

            return (result.originalTotal,
                    result.appliedDiscount,
                    result.loyaltyDiscount,
                    result.finalTotal);
        }

        public async Task<(decimal originalTotal,
                           Discount? appliedDiscount,
                           Discount? loyaltyDiscount,
                           decimal finalTotal,
                           List<ProductDiscountInfo> itemDiscounts)>
            CalculateBestDiscountsPerItemAsync(int customerId, List<Product> products)
        {
            if (products == null || !products.Any())
                throw new Exception("No products provided for discount calculation");

            var itemDiscounts = new List<ProductDiscountInfo>();
            var originalTotal = products.Sum(p => p.Price);

            var allDiscounts = await _discountService.GetAllDiscountsAsync();
            var customer = await _customerService.GetCustomerByIdAsync(customerId)
                           ?? throw new Exception("Customer not found");

            // 1️ Determine loyalty discount
            var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);
            Discount? loyaltyDiscount = null;

            if (loyaltyEntity != null)
            {
                loyaltyDiscount = new Discount
                {
                    Id = loyaltyEntity.Id,
                    Name = loyaltyEntity.Name,
                    DiscountAmount = loyaltyEntity.DiscountAmount,
                    IsLoyalty = true,
                    AppliesToProduct = loyaltyEntity.AppliesToProduct,
                    AppliesToTreatment = loyaltyEntity.AppliesToTreatment
                };
            }

            // 2️ Build VALID discount list (Loyalty OR Active Campaigns)
            var validDiscounts = new List<Discount>();
            var today = DateTime.Today;

            foreach (var discount in allDiscounts)
            {
                if (discount.IsLoyalty)
                {
                    // Loyalty handled separately above, ignore here
                    continue;
                }

                // Lookup campaign tied to this discount ID
                var campaign = await _discountService.GetCampaignByDiscountIdAsync(discount.Id);

                if (campaign != null && campaign.CheckTime())
                {
                    // Campaign is currently active → discount valid
                    validDiscounts.Add(discount);
                }
            }

            // Note: loyalty discount is not added here so we can compare per-item later

            // 3️ Apply best discount per item

            Discount? bestProductDiscount = null;
            Discount? bestTreatmentDiscount = null;

            decimal finalTotal = 0;

            foreach (var product in products)
            {
                bool isTreatment = product is Treatment;

                // Only active (validCampaign) non-loyalty discounts
                var applicableDiscounts = validDiscounts
                    .Where(d =>
                        (isTreatment && d.AppliesToTreatment) ||
                        (!isTreatment && d.AppliesToProduct))
                    .OrderByDescending(d => d.DiscountAmount)
                    .ToList();

                var bestRegular = applicableDiscounts.FirstOrDefault();

                if (!isTreatment)
                    bestProductDiscount ??= bestRegular;
                else
                    bestTreatmentDiscount ??= bestRegular;

                // Loyalty per-item applicability
                Discount? loyaltyForItem = null;
                if (loyaltyDiscount != null)
                {
                    bool applies =
                        (isTreatment && loyaltyDiscount.AppliesToTreatment) ||
                        (!isTreatment && loyaltyDiscount.AppliesToProduct);

                    if (applies)
                        loyaltyForItem = loyaltyDiscount;
                }

                // Pick strongest
                Discount? finalItemDiscount = null;
                if (bestRegular != null && loyaltyForItem != null)
                    finalItemDiscount = bestRegular.DiscountAmount >= loyaltyForItem.DiscountAmount
                        ? bestRegular
                        : loyaltyForItem;
                else
                    finalItemDiscount = bestRegular ?? loyaltyForItem;

                decimal finalPrice = product.Price;
                if (finalItemDiscount != null)
                    finalPrice = finalPrice * (1 - finalItemDiscount.DiscountAmount);

                finalTotal += finalPrice;

                itemDiscounts.Add(new ProductDiscountInfo
                {
                    ProductId = product.Id,
                    ProductName = product.Name ?? "",
                    OriginalPrice = product.Price,
                    FinalPrice = finalPrice,
                    DiscountAmount = finalItemDiscount?.DiscountAmount ?? 0,
                    DiscountName = finalItemDiscount?.Name,
                    IsLoyalty = finalItemDiscount?.IsLoyalty ?? false
                });
            }

            // This is only for the order header (same behavior as before)
            Discount? appliedDiscount = bestProductDiscount ?? bestTreatmentDiscount;

            return (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal, itemDiscounts);
        }

        //public async Task AddOrderLineAsync(OrderLine orderLine)
        //{
        //    await _orderLineService.AddOrderLineAsync(orderLine);
        //}


    }
}

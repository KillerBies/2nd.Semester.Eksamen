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
            // Load booking with treatments & attached products
            var booking = await _customerService.GetBookingWithTreatmentsAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            // Flatten all products/treatments for this booking
            var productsList = new List<Product>();

            if (booking.Treatments != null)
            {
                foreach (var tb in booking.Treatments)
                {
                    if (tb.Treatment != null)
                        productsList.Add(tb.Treatment); // treatment itself counts as a product

                    if (tb.TreatmentBookingProducts != null)
                    {
                        foreach (var tbp in tb.TreatmentBookingProducts)
                        {
                            if (tbp.Product != null)
                            {
                                for (int i = 0; i < tbp.NumberOfProducts; i++)
                                    productsList.Add(tbp.Product);
                            }
                        }
                    }
                }
            }

            if (!productsList.Any())
                throw new Exception("No products or treatments found for this booking");

            // Calculate totals & best discounts
            var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal, itemDiscounts) =
                await CalculateBestDiscountsPerItemAsync(booking.CustomerId, productsList);

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
                // Update existing order totals
                order.UpdateTotals(originalTotal, finalTotal, appliedDiscount?.Id);
                await _customerService.UpdateOrderAsync(order);
            }

            // Add order lines grouped by product
            foreach (var productGroup in productsList.GroupBy(p => p.Id))
            {
                var productId = productGroup.Key;
                var quantity = productGroup.Count();

                var orderLine = new OrderLine
                {
                    OrderID = order.Id,
                    ProductId = productId,
                    NumberOfProducts = quantity
                };

                await _orderLineService.AddOrderLineAsync(orderLine);
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

            // 1️⃣ Determine loyalty discount
            var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);
            Discount? loyaltyDiscount = null;

            if (loyaltyEntity != null)
            {
                loyaltyDiscount = new Discount
                {
                    Id = loyaltyEntity.Id,
                    Name = loyaltyEntity.Name,
                    TreatmentDiscount = loyaltyEntity.TreatmentDiscount,
                    ProductDiscount = loyaltyEntity.ProductDiscount,
                    IsLoyalty = true,
                    AppliesToProduct = loyaltyEntity.AppliesToProduct,
                    AppliesToTreatment = loyaltyEntity.AppliesToTreatment
                };
            }

            // 2️⃣ Build list of valid campaign discounts
            var validCampaignDiscounts = new List<Discount>();
            foreach (var discount in allDiscounts.Where(d => !d.IsLoyalty))
            {
                var campaign = await _discountService.GetCampaignByDiscountIdAsync(discount.Id);
                if (campaign != null && campaign.CheckTime())
                {
                    validCampaignDiscounts.Add(discount);
                }
            }

            decimal finalTotal = 0;
            Discount? appliedDiscountForHeader = null;

            // 3️⃣ Calculate potential savings per discount
            var discountSavingsMap = new Dictionary<Discount, decimal>();

            foreach (var discount in validCampaignDiscounts.Concat(loyaltyDiscount != null ? new[] { loyaltyDiscount } : Array.Empty<Discount>()))
            {
                decimal totalSavings = 0;

                foreach (var product in products)
                {
                    bool isTreatment = product is Treatment;

                    // Skip if discount doesn’t apply
                    bool applies = (isTreatment && discount.AppliesToTreatment) || (!isTreatment && discount.AppliesToProduct);
                    if (!applies) continue;

                    totalSavings += product.Price * discount.GetDiscountAmountFor(product);
                }

                discountSavingsMap[discount] = totalSavings;
            }

            // 4️⃣ Pick discount with max flat money saved
            Discount? bestDiscount = discountSavingsMap
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .FirstOrDefault();

            // 5️⃣ Apply this single best discount per item
            foreach (var product in products)
            {
                bool isTreatment = product is Treatment;

                decimal finalPrice = product.Price;
                decimal discountAmount = 0;
                string discountName = string.Empty;
                bool isLoyaltyApplied = false;

                if (bestDiscount != null)
                {
                    bool applies = (isTreatment && bestDiscount.AppliesToTreatment) || (!isTreatment && bestDiscount.AppliesToProduct);
                    if (applies)
                    {
                        discountAmount = bestDiscount.GetDiscountAmountFor(product);
                        finalPrice = product.Price * (1 - discountAmount);
                        discountName = bestDiscount.Name ?? "";
                        isLoyaltyApplied = bestDiscount.IsLoyalty;
                    }
                }

                finalTotal += finalPrice;

                itemDiscounts.Add(new ProductDiscountInfo
                {
                    ProductId = product.Id,
                    ProductName = product.Name ?? "",
                    OriginalPrice = product.Price,
                    FinalPrice = finalPrice,
                    DiscountAmount = discountAmount,
                    DiscountName = discountName,
                    IsLoyalty = isLoyaltyApplied
                });
            }

            appliedDiscountForHeader = bestDiscount;

            return (originalTotal, appliedDiscountForHeader, loyaltyDiscount, finalTotal, itemDiscounts);
        }



        public async Task AddOrderLineAsync(OrderLine orderLine)
        {
            await _orderLineService.AddOrderLineAsync(orderLine);
        }


    }
}

using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly ICustomerService _customerService;

        public OrderService(
            IProductService productService,
            IDiscountService discountService,
            ICustomerService customerService)
        {
            _productService = productService;
            _discountService = discountService;
            _customerService = customerService;
        }

        public Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
            => _productService.GetProductsByIdsAsync(productIds);

        public async Task<Order> CreateOrUpdateOrderForBookingAsync(int bookingId)
        {
            var booking = await _customerService.GetBookingWithTreatmentsAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            var productsList = new List<Product>();

            // Add treatment products (TPH)
            if (booking.Treatments != null)
            {
                productsList.AddRange(
                    booking.Treatments
                           .Where(tb => tb.Treatment != null)
                           .Select(tb => tb.Treatment)
                );
            }

            if (!productsList.Any())
                throw new Exception("No products or treatments found for this booking");

            var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal) =
                await CalculateBestDiscountsAsync(booking.CustomerId, productsList);

            var order = await _customerService.GetOrderByBookingIdAsync(bookingId);

            if (order == null)
            {
                order = new Order(bookingId, originalTotal, finalTotal, appliedDiscount?.Id ?? 0);
                await _customerService.AddOrderAsync(order);
            }
            else
            {
                order.UpdateTotals(originalTotal, finalTotal, appliedDiscount?.Id);
                await _customerService.UpdateOrderAsync(order);
            }

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

            var productItems = products.Where(p => !(p is Treatment)).ToList();
            var treatmentItems = products.OfType<Treatment>().ToList();

            decimal originalTotal = products.Sum(p => p.Price);

            var allDiscounts = await _discountService.GetAllDiscountsAsync();
            var customer = await _customerService.GetCustomerByIdAsync(customerId)
                           ?? throw new Exception("Customer not found");

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

            Discount? bestProductDiscount = null;
            Discount? bestTreatmentDiscount = null;

            decimal finalTotal = 0;

            foreach (var product in products)
            {
                bool isTreatment = product is Treatment;

                var regularApplicable = allDiscounts
                    .Where(d => !d.IsLoyalty &&
                                ((isTreatment && d.AppliesToTreatment) ||
                                 (!isTreatment && d.AppliesToProduct)))
                    .OrderByDescending(d => d.DiscountAmount)
                    .ToList();

                var bestRegular = regularApplicable.FirstOrDefault();

                if (!isTreatment)
                    bestProductDiscount ??= bestRegular;
                else
                    bestTreatmentDiscount ??= bestRegular;

                Discount? loyaltyForItem = null;

                if (loyaltyDiscount != null)
                {
                    bool applies =
                        (isTreatment && loyaltyDiscount.AppliesToTreatment) ||
                        (!isTreatment && loyaltyDiscount.AppliesToProduct);

                    if (applies)
                        loyaltyForItem = loyaltyDiscount;
                }

                Discount? finalItemDiscount;

                if (bestRegular != null && loyaltyForItem != null)
                    finalItemDiscount = (bestRegular.DiscountAmount >= loyaltyForItem.DiscountAmount)
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

            Discount? appliedDiscount = bestProductDiscount ?? bestTreatmentDiscount;

            return (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal, itemDiscounts);
        }
    }
}

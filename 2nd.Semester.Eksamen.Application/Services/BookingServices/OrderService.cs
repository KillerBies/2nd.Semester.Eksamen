using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
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

            // Grab all treatments as products
            var productsList = new List<Product>();
            if (booking.Treatments != null)
                productsList.AddRange(
                    booking.Treatments
                           .Where(tb => tb.Treatment != null)
                           .Select(tb => tb.Treatment)
                );

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

        public async Task<(decimal originalTotal, Discount? appliedDiscount, Discount? loyaltyDiscount, decimal finalTotal)>
            CalculateBestDiscountsAsync(int customerId, List<Product> products)
        {
            if (products == null || !products.Any())
                throw new Exception("No products provided for discount calculation");

            // Split products by runtime type (TPH)
            var productItems = products.Where(p => !(p is Treatment)).ToList();
            var treatmentItems = products.OfType<Treatment>().ToList();

            decimal originalTotal = products.Sum(p => p.Price);

            // Get all discounts
            var allDiscounts = await _discountService.GetAllDiscountsAsync();

            // Best regular discounts
            var bestProductDiscount = productItems.Any()
                ? allDiscounts.Where(d => !d.IsLoyalty && d.AppliesToProduct)
                              .OrderByDescending(d => d.DiscountAmount)
                              .FirstOrDefault()
                : null;

            var bestTreatmentDiscount = treatmentItems.Any()
                ? allDiscounts.Where(d => !d.IsLoyalty && d.AppliesToTreatment)
                              .OrderByDescending(d => d.DiscountAmount)
                              .FirstOrDefault()
                : null;

            // Loyalty discount
            var customer = await _customerService.GetCustomerByIdAsync(customerId)
                           ?? throw new Exception("Customer not found");
            var loyaltyEntity = await _discountService.GetLoyaltyDiscountForVisitsAsync(customer.NumberOfVisists);

            Discount? loyaltyDiscount = null;
            if (loyaltyEntity != null)
            {
                bool applies = (loyaltyEntity.AppliesToProduct && productItems.Any()) ||
                               (loyaltyEntity.AppliesToTreatment && treatmentItems.Any());
                if (applies)
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
            }

            // Calculate total
            decimal finalTotal = 0;

            if (productItems.Any())
            {
                decimal total = productItems.Sum(p => p.Price);
                finalTotal += bestProductDiscount != null
                    ? total * (1 - bestProductDiscount.DiscountAmount)
                    : total;
            }

            if (treatmentItems.Any())
            {
                decimal total = treatmentItems.Sum(t => t.Price);
                finalTotal += bestTreatmentDiscount != null
                    ? total * (1 - bestTreatmentDiscount.DiscountAmount)
                    : total;
            }

            // Apply loyalty discount on top
            if (loyaltyDiscount != null)
            {
                if (loyaltyDiscount.AppliesToProduct && productItems.Any())
                    finalTotal -= productItems.Sum(p => p.Price) * loyaltyDiscount.DiscountAmount;

                if (loyaltyDiscount.AppliesToTreatment && treatmentItems.Any())
                    finalTotal -= treatmentItems.Sum(t => t.Price) * loyaltyDiscount.DiscountAmount;
            }

            var appliedDiscount = bestProductDiscount ?? bestTreatmentDiscount;

            return (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal);
        }
    }
}

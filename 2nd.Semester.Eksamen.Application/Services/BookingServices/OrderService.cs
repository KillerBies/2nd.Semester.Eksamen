using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderLineService _orderLineService;
        private readonly IDiscountCalculator _discountCalculator;

        public OrderService(ICustomerService customerService,
                            IOrderLineService orderLineService,
                            IDiscountCalculator discountCalculator)
        {
            _customerService = customerService;
            _orderLineService = orderLineService;
            _discountCalculator = discountCalculator;
        }

        public async Task<Order> CreateOrUpdateOrderForBookingAsync(int bookingId)
        {
            var booking = await _customerService.GetBookingWithTreatmentsAsync(bookingId)
                          ?? throw new Exception("Booking not found");



            var products = GetAllProductsFromBooking(booking);

            var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal, itemDiscounts) =
                await _discountCalculator.CalculateAsync(booking.CustomerId, products);
            
            var order = await GetOrCreateOrderAsync(bookingId, booking.CustomerId, originalTotal, finalTotal, finalTotal * 0.20m, appliedDiscount);

            await AddOrderLinesAsync(order, products);

            await MarkBookingCompletedAsync(booking);

            return order;
        }

        // ---------------- HELPERS ----------------

        private List<Product> GetAllProductsFromBooking(Booking booking)
        {
            var productsList = new List<Product>();

            foreach (var tb in booking.Treatments)
            {
                if (tb.Treatment != null && tb.Employee != null)
                {
                    productsList.Add(new Treatment
                    {
                        Id = tb.Treatment.Id,
                        Name = tb.Treatment.Name,
                        Price = tb.Treatment.Price * tb.Employee.BasePriceMultiplier
                    });
                }

                if (tb.TreatmentBookingProducts != null)
                {
                    foreach (var tbp in tb.TreatmentBookingProducts)
                    {
                        for (int i = 0; i < tbp.NumberOfProducts; i++)
                        {
                            productsList.Add(new Product
                            {
                                Id = tbp.Product.Id,
                                Name = tbp.Product.Name,
                                Price = tbp.Product.Price
                            });
                        }
                    }
                }
            }

            if (!productsList.Any())
                throw new Exception("No products or treatments found for this booking");

            return productsList;
        }

        private async Task<Order> GetOrCreateOrderAsync(int bookingId, int customerId, decimal originalTotal, decimal finalTotal, decimal vat, Discount? appliedDiscount)
        {
            var order = await _customerService.GetOrderByBookingIdAsync(bookingId);
            if (order == null)
            {
                order = new Order(bookingId, originalTotal, finalTotal, vat, appliedDiscount?.Id ?? 0){ Guid = Guid.NewGuid() };

                await _customerService.AddOrderAsync(order);    

            }       
            else
            {
                order = new Order(bookingId, originalTotal, finalTotal, vat ,appliedDiscount?.Id ?? 0);
                await _customerService.UpdateOrderAsync(order);
            }
            return order;
        }

        private async Task AddOrderLinesAsync(Order order, List<Product> products)
        {
            foreach (var group in products.GroupBy(p => p.Id))
            {
                await _orderLineService.AddOrderLineAsync(new OrderLine
                {
                    
                    OrderID = order.Id,
                    ProductId = group.Key,
                    NumberOfProducts = group.Count()
                    
                });
            }

        }

        private async Task MarkBookingCompletedAsync(Booking booking)
        {
            booking.Status = BookingStatus.Completed;
            await _customerService.UpdateBookingAsync(booking);
        }
    }
}

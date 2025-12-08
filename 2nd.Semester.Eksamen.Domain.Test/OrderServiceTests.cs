using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Tests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IDiscountService> _discountServiceMock;
        private Mock<ICustomerService> _customerServiceMock;
        private Mock<IOrderLineService> _orderLineServiceMock;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _discountServiceMock = new Mock<IDiscountService>();
            _customerServiceMock = new Mock<ICustomerService>();
            _orderLineServiceMock = new Mock<IOrderLineService>();

            _orderService = new OrderService(
                _discountServiceMock.Object,
                _customerServiceMock.Object,
                _orderLineServiceMock.Object
            );
        }

        [Test]
        public async Task CalculateBestDiscountsPerItemAsync_ShouldPickBestDiscountAmongMultiple()
        {
            // Arrange
            var customerId = 1;

            // Products / treatments
            var deepTissueMassage = new Treatment("Deep Tissue Massage", 75.00m, "Intense massage targeting deep muscle layers to relieve tension.", "Massage", TimeSpan.FromHours(1));
            var facialRejuvenation = new Treatment("Facial Rejuvenation", 50.00m, "Revitalizing facial treatment to cleanse and hydrate skin.", "Skincare", TimeSpan.FromMinutes(45));
            var soap = new Product("Soap", 100.00m, "Sweet Soap");
            var products = new List<Product> { deepTissueMassage, facialRejuvenation, soap };

            // Customer with enough visits for Gold loyalty
            var customer = new Customer("Test Name", new Address("City", "1234", "Street", "10"), "12345678", "email@test.com", "", true);
            typeof(Customer).GetProperty("NumberOfVisists", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                             .SetValue(customer, 12);

            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);


            // Campaign discounts – **bumped above loyalty**
            var winterCampaign = new Campaign("Winter Campaign", 0.25m, 0.12m, new DateTime(2025, 12, 1), new DateTime(2026, 2, 28))
            {
                Description = "Winter Campaign",
                AppliesToProduct = true,
                AppliesToTreatment = true
            };
            var springCampaign = new Campaign("Spring Campaign", 0.15m, 0.07m, new DateTime(2026, 3, 1), new DateTime(2026, 5, 31))
            {
                Description = "Spring Campaign",
                AppliesToProduct = true,
                AppliesToTreatment = true
            };

            // Loyalty discounts
            var bronzeLoyalty = new LoyaltyDiscount(3, "Bronze", "Bronze Loyalty", 0.05m, 0.02m) { AppliesToProduct = true, AppliesToTreatment = true };
            var silverLoyalty = new LoyaltyDiscount(6, "Silver", "Silver Loyalty", 0.10m, 0.05m) { AppliesToProduct = true, AppliesToTreatment = true };
            var goldLoyalty = new LoyaltyDiscount(12, "Gold", "Gold Loyalty", 0.15m, 0.08m) { AppliesToProduct = true, AppliesToTreatment = true };

            // List of all discounts
            var discounts = new List<Discount> { winterCampaign, springCampaign, bronzeLoyalty, silverLoyalty, goldLoyalty };

            // Mock discount service methods
            _discountServiceMock.Setup(d => d.GetAllDiscountsAsync()).ReturnsAsync(discounts);
            _discountServiceMock.Setup(d => d.GetLoyaltyDiscountForVisitsAsync(It.IsAny<int>()))
                .ReturnsAsync((int visits) =>
                {
                    if (visits >= 12) return goldLoyalty;
                    if (visits >= 6) return silverLoyalty;
                    if (visits >= 3) return bronzeLoyalty;
                    return null;
                });

            winterCampaign.Id = 1;
            springCampaign.Id = 2;
            bronzeLoyalty.Id = 3;
            silverLoyalty.Id = 4;
            goldLoyalty.Id = 5;

            _discountServiceMock.Setup(d => d.GetCampaignByDiscountIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return id switch
                    {
                        1 => winterCampaign,
                        2 => springCampaign,
                        _ => null
                    };
                });

            // Act
            var (originalTotal, appliedDiscount, loyaltyDiscount, finalTotal, itemDiscounts) =
                await _orderService.CalculateBestDiscountsPerItemAsync(customerId, products);

            // Assert
            Assert.That(originalTotal, Is.EqualTo(225m)); // 75 + 50 + 100
            Console.WriteLine(appliedDiscount.TreatmentDiscount + " " + appliedDiscount.ProductDiscount);
            Assert.That(appliedDiscount, Is.Not.Null);
            Assert.That(appliedDiscount.Name, Is.EqualTo("Winter Campaign")); // best overall discount

            Assert.That(loyaltyDiscount, Is.Not.Null);
            Assert.That(loyaltyDiscount.Name, Is.EqualTo("Gold Loyalty")); // exists but not applied

            // Check final totals per product
            var massageDiscount = itemDiscounts.First(i => i.ProductName == "Deep Tissue Massage");
            Assert.That(massageDiscount.FinalPrice, Is.EqualTo(56.25m)); // 75 * 0.75

            var facialDiscount = itemDiscounts.First(i => i.ProductName == "Facial Rejuvenation");
            Assert.That(facialDiscount.FinalPrice, Is.EqualTo(37.50m)); // 50 * 0.75

            var soapDiscount = itemDiscounts.First(i => i.ProductName == "Soap");
            Assert.That(soapDiscount.FinalPrice, Is.EqualTo(88m)); // 100 * 0.88
        }
        [Test]
        public async Task CreateOrUpdateOrderForBookingAsync_ShouldCreateOrderAndOrderLines()
        {
            // Arrange
            var customerId = 1;
            var bookingId = 1;
            var employee = new Employee("Jane Doe", "1234", "jane@example.com", "", new Address("City", "1234", "Street", "10"), 0, "", "", "", "", TimeSpan.Zero, TimeSpan.Zero);

            var deepTissueMassage = new Treatment("Deep Tissue Massage", 75m, "desc", "Massage", TimeSpan.FromHours(1)) { Id = 1 };
            var soap = new Product("Soap", 50m, "desc") { Id = 2 };

            var tb = new TreatmentBooking(deepTissueMassage, employee, DateTime.Now, DateTime.Now.AddHours(1));
            tb.TreatmentBookingProducts.Add(new TreatmentBookingProduct(soap, 2));

            var booking = new Booking(customerId, DateTime.Now, DateTime.Now.AddHours(1), new List<TreatmentBooking> { tb });

            var customer = new Customer("Test Name", new Address("City", "1234", "Street", "10"), "12345678", "email@test.com", "", true);
            typeof(Customer).GetProperty("NumberOfVisists", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .SetValue(customer, 7);

            _customerServiceMock.Setup(x => x.GetBookingWithTreatmentsAsync(bookingId))
                                .ReturnsAsync(booking);
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(customerId))
                                .ReturnsAsync(customer);
            _customerServiceMock.Setup(x => x.GetOrderByBookingIdAsync(bookingId))
                                .ReturnsAsync((Order?)null);
            _customerServiceMock.Setup(x => x.AddOrderAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _customerServiceMock.Setup(x => x.UpdateBookingAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);

            _orderLineServiceMock.Setup(x => x.AddOrderLineAsync(It.IsAny<OrderLine>())).Returns(Task.CompletedTask);

            _discountServiceMock.Setup(d => d.GetAllDiscountsAsync()).ReturnsAsync(new List<Discount>());
            _discountServiceMock.Setup(d => d.GetLoyaltyDiscountForVisitsAsync(It.IsAny<int>()))
                                .ReturnsAsync((LoyaltyDiscount?)null);
            _discountServiceMock.Setup(d => d.GetCampaignByDiscountIdAsync(It.IsAny<int>()))
                                .ReturnsAsync((Campaign?)null);

            // Act
            var order = await _orderService.CreateOrUpdateOrderForBookingAsync(bookingId);

            // Assert
            Assert.That(order, Is.Not.Null);
            Assert.That(order.Total, Is.EqualTo(75 + 2 * 50)); // deepTissueMassage + 2 soaps

            _orderLineServiceMock.Verify(x => x.AddOrderLineAsync(
                It.Is<OrderLine>(ol => ol.NumberOfProducts == 1 && ol.ProductId == deepTissueMassage.Id)), Times.Once);

            _orderLineServiceMock.Verify(x => x.AddOrderLineAsync(
                It.Is<OrderLine>(ol => ol.NumberOfProducts == 2 && ol.ProductId == soap.Id)), Times.Once);

            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Completed));
        }



    }
}

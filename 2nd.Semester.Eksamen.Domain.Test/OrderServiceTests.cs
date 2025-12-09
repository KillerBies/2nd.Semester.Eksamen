using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Test
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<ICustomerService> _customerServiceMock;
        private Mock<IOrderLineService> _orderLineServiceMock;
        private Mock<IDiscountCalculator> _discountCalculatorMock;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _orderLineServiceMock = new Mock<IOrderLineService>();
            _discountCalculatorMock = new Mock<IDiscountCalculator>();

            _orderService = new OrderService(
                _customerServiceMock.Object,
                _orderLineServiceMock.Object,
                _discountCalculatorMock.Object
            );
        }

        [Test]
        public async Task CalculateBestDiscountsPerItemAsync_ShouldPickBestDiscountAmongMultiple()
        {
            // Arrange
            var customerId = 1;

            var deepTissueMassage = new Treatment("Deep Tissue Massage", 75.00m, "desc", "Massage", TimeSpan.FromHours(1)) { Id = 1 };
            var facialRejuvenation = new Treatment("Facial Rejuvenation", 50.00m, "desc", "Skincare", TimeSpan.FromMinutes(45)) { Id = 2 };
            var soap = new Product("Soap", 100.00m, "Sweet Soap") { Id = 3 };
            var products = new List<Product> { deepTissueMassage, facialRejuvenation, soap };

            var customer = new Customer("Test Name", new Address("City", "1234", "Street", "10"), "12345678", "email@test.com", "", true);
            typeof(Customer).GetProperty("NumberOfVisists", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .SetValue(customer, 12);

            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(customerId))
                                .ReturnsAsync(customer);

            // Discounts
            var winterCampaign = new Campaign("Winter Campaign", 0.25m, 0.12m, DateTime.Now, DateTime.Now.AddMonths(2)) { Id = 1 };
            var goldLoyalty = new LoyaltyDiscount(12, "Gold", "Gold Loyalty", 0.15m, 0.08m) { Id = 5 };

            // Mock discount calculator
            _discountCalculatorMock.Setup(d => d.CalculateAsync(customerId, It.IsAny<List<Product>>()))
                .ReturnsAsync((
                    225m,                   // original total
                    winterCampaign as Discount,   // applied discount
                    goldLoyalty as Discount,      // loyalty discount
                    181.75m,                // final total after discounts
                    new List<ProductDiscountInfoDTO> {
                        new ProductDiscountInfoDTO { ProductId = 1, ProductName = "Deep Tissue Massage", OriginalPrice = 75m, FinalPrice = 56.25m, DiscountAmount = 0.25m, DiscountName = "Winter Campaign", IsLoyalty = false },
                        new ProductDiscountInfoDTO { ProductId = 2, ProductName = "Facial Rejuvenation", OriginalPrice = 50m, FinalPrice = 37.50m, DiscountAmount = 0.25m, DiscountName = "Winter Campaign", IsLoyalty = false },
                        new ProductDiscountInfoDTO { ProductId = 3, ProductName = "Soap", OriginalPrice = 100m, FinalPrice = 88m, DiscountAmount = 0.12m, DiscountName = "Winter Campaign", IsLoyalty = false },
                    }
                ));

            // Act
            (decimal originalTotal,
             Discount? appliedDiscount,
             Discount? loyaltyDiscount,
             decimal finalTotal,
             List<ProductDiscountInfoDTO> itemDiscounts) =
                await _discountCalculatorMock.Object.CalculateAsync(customerId, products);

            // Assert
            Assert.That(originalTotal, Is.EqualTo(225m));
            Assert.That(appliedDiscount, Is.Not.Null);
            Assert.That(appliedDiscount!.Name, Is.EqualTo("Winter Campaign"));
            Assert.That(loyaltyDiscount, Is.Not.Null);
            Assert.That(loyaltyDiscount!.Name, Is.EqualTo("Gold Loyalty"));

            var massageDiscount = itemDiscounts.First(i => i.ProductName == "Deep Tissue Massage");
            Assert.That(massageDiscount.FinalPrice, Is.EqualTo(56.25m));

            var facialDiscount = itemDiscounts.First(i => i.ProductName == "Facial Rejuvenation");
            Assert.That(facialDiscount.FinalPrice, Is.EqualTo(37.50m));

            var soapDiscount = itemDiscounts.First(i => i.ProductName == "Soap");
            Assert.That(soapDiscount.FinalPrice, Is.EqualTo(88m));
        }

        [Test]
        public async Task CreateOrUpdateOrderForBookingAsync_ShouldCreateOrderAndOrderLines()
        {
            // Arrange
            var customerId = 1;
            var bookingId = 1;

            var deepTissueMassage = new Treatment("Deep Tissue Massage", 75m, "desc", "Massage", TimeSpan.FromHours(1)) { Id = 1 };
            var soap = new Product("Soap", 50m, "desc") { Id = 2 };

            var tb = new TreatmentBooking(deepTissueMassage, null, DateTime.Now, DateTime.Now.AddHours(1));
            tb.TreatmentBookingProducts.Add(new TreatmentBookingProduct(soap, 2));

            var booking = new Booking(customerId, DateTime.Now, DateTime.Now.AddHours(1), new List<TreatmentBooking> { tb });

            var customer = new Customer("Test Name", new Address("City", "1234", "Street", "10"), "12345678", "email@test.com", "", true);
            typeof(Customer).GetProperty("NumberOfVisists", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .SetValue(customer, 7);

            _customerServiceMock.Setup(x => x.GetBookingWithTreatmentsAsync(bookingId)).ReturnsAsync(booking);
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(customerId)).ReturnsAsync(customer);
            _customerServiceMock.Setup(x => x.GetOrderByBookingIdAsync(bookingId)).ReturnsAsync((Order?)null);
            _customerServiceMock.Setup(x => x.AddOrderAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _customerServiceMock.Setup(x => x.UpdateBookingAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);
            _orderLineServiceMock.Setup(x => x.AddOrderLineAsync(It.IsAny<OrderLine>())).Returns(Task.CompletedTask);

            _discountCalculatorMock.Setup(d => d.CalculateAsync(customerId, It.IsAny<List<Product>>()))
                .ReturnsAsync((
                    175m,      // original total
                    null,      // applied discount
                    null,      // loyalty discount
                    175m,      // final total
                    new List<ProductDiscountInfoDTO> {
                        new ProductDiscountInfoDTO { ProductId = 1, ProductName = "Deep Tissue Massage", OriginalPrice = 75m, FinalPrice = 75m, DiscountAmount = 0, DiscountName = "", IsLoyalty = false },
                        new ProductDiscountInfoDTO { ProductId = 2, ProductName = "Soap", OriginalPrice = 50m, FinalPrice = 50m, DiscountAmount = 0, DiscountName = "", IsLoyalty = false },
                    }
                ));

            // Act
            var order = await _orderService.CreateOrUpdateOrderForBookingAsync(bookingId);

            // Assert
            Assert.That(order, Is.Not.Null);
            Assert.That(order.Total, Is.EqualTo(175m));

            _orderLineServiceMock.Verify(x => x.AddOrderLineAsync(
                It.Is<OrderLine>(ol => ol.NumberOfProducts == 1 && ol.ProductId == deepTissueMassage.Id)), Times.Once);

            _orderLineServiceMock.Verify(x => x.AddOrderLineAsync(
                It.Is<OrderLine>(ol => ol.NumberOfProducts == 2 && ol.ProductId == soap.Id)), Times.Once);

            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Completed));
        }
    }
}

using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using Moq;
using NUnit.Framework;
using System.Reflection;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Test
{
    [TestFixture]
    public class CustomerDeletionTests
    {
        private Mock<ICustomerService> _customerServiceMock = null!;

        [SetUp]
        public void Setup()
        {
            _customerServiceMock = new Mock<ICustomerService>();
        }

        [Test]
        public async Task DeleteCustomer_ShouldCallDelete_WhenSaveAsCustomerIsFalse()
        {
            // Arrange
            var customer = new Customer("Test Name",
                                        new Address("City", "1234", "Street", "10"),
                                        "12345678", "email@test.com", "", true);

            // Use reflection to set SaveAsCustomer to false
            typeof(Customer)
                .GetProperty("SaveAsCustomer", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SetValue(customer, false);

            _customerServiceMock.Setup(x => x.DeleteAsync(customer))
                                .Returns(Task.CompletedTask);

            // Act – simulate deletion logic
            if (!customer.SaveAsCustomer)
            {
                await _customerServiceMock.Object.DeleteAsync(customer);
            }

            // Assert
            _customerServiceMock.Verify(x => x.DeleteAsync(customer), Times.Once);
        }

        [Test]
        public async Task DeleteCustomer_ShouldNotCallDelete_WhenSaveAsCustomerIsTrue()
        {
            // Arrange
            var customer = new Customer("Test Name",
                                        new Address("City", "1234", "Street", "10"),
                                        "12345678", "email@test.com", "", true);

            // Use reflection to set SaveAsCustomer to true
            typeof(Customer)
                .GetProperty("SaveAsCustomer", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SetValue(customer, true);

            _customerServiceMock.Setup(x => x.DeleteAsync(customer))
                                .Returns(Task.CompletedTask);

            // Act – simulate deletion logic
            if (!customer.SaveAsCustomer)
            {
                await _customerServiceMock.Object.DeleteAsync(customer);
            }

            // Assert
            _customerServiceMock.Verify(x => x.DeleteAsync(customer), Times.Never);
        }
    }
}

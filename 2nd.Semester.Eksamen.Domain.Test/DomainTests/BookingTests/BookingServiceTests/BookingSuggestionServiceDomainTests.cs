using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;

namespace _2nd.Semester.Eksamen.Domain.Test.DomainTests.BookingTests.BookingServiceTests
{
    [TestFixture]
    public class BookingSuggestionServiceDomainTests
    {
        private Mock<IScheduleDayRepository> _mockRepo;
        private BookingSuggestionService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IScheduleDayRepository>();
            _service = new BookingSuggestionService(_mockRepo.Object);
        }

        private Employee CreateEmployee(int id = 1)
        {
            return new Employee
            {
                Id = id,
                WorkStart = new TimeOnly(9, 0),
                WorkEnd = new TimeOnly(17, 0),
            };
        }

        private Treatment CreateTreatment(string name = "Facial", double hours = 1)
        {
            return new Treatment(name, 100, "desc", "Beauty", TimeSpan.FromHours(hours));
        }

        private TreatmentBooking CreateBooking(Employee emp, DateTime start, DateTime end)
        {
            var treatment = CreateTreatment();
            var tb = new TreatmentBooking(treatment, emp, start, end);
            return tb;
        }

        [Test]
        public async Task GetBookingSugestions_ReturnsSuggestions_WhenSlotsAvailable()
        {
            // Arrange
            var emp = CreateEmployee();
            var booking = CreateBooking(emp, DateTime.Now.AddHours(1), DateTime.Now.AddHours(2));

            var scheduleDay = new ScheduleDay(DateOnly.FromDateTime(DateTime.Today), emp.WorkStart, emp.WorkEnd);
            _mockRepo.Setup(r => r.GetByEmployeeIDAsync(emp.Id))
                     .ReturnsAsync(new List<ScheduleDay> { scheduleDay });

            var treatments = new List<TreatmentBooking> { booking };

            // Act
            var suggestions = await _service.GetBookingSugestions(
                treatments,
                startDate: DateOnly.FromDateTime(DateTime.Today),
                numberOfDaysToCheck: 1,
                neededSuggestions: 5,
                interval: 30
            );

            // Assert
            Assert.That(suggestions, Is.Not.Null);
            Assert.That(suggestions.Count, Is.GreaterThan(0));
            Assert.That(suggestions.First().Items.First().Employee, Is.EqualTo(emp));
            Assert.That(suggestions.First().Items.First().Treatment, Is.EqualTo(booking.Treatment));
        }

        [Test]
        public async Task GetBookingSugestions_ReturnsEmpty_WhenNoTreatments()
        {
            // Act
            var suggestions = await _service.GetBookingSugestions(
                new List<TreatmentBooking>(),
                startDate: DateOnly.FromDateTime(DateTime.Today),
                numberOfDaysToCheck: 1,
                neededSuggestions: 5,
                interval: 30
            );

            // Assert
            Assert.That(suggestions, Is.Empty);
        }

        [Test]
        public async Task GetBookingSugestions_RespectsIntervalAndNeededSuggestions()
        {
            // Arrange
            var emp = CreateEmployee();
            var booking = CreateBooking(emp, DateTime.Now.AddHours(1), DateTime.Now.AddHours(2));

            var scheduleDay = new ScheduleDay(DateOnly.FromDateTime(DateTime.Today), emp.WorkStart, emp.WorkEnd);
            _mockRepo.Setup(r => r.GetByEmployeeIDAsync(emp.Id))
                     .ReturnsAsync(new List<ScheduleDay> { scheduleDay });

            var treatments = new List<TreatmentBooking> { booking };

            // Act
            var suggestions = await _service.GetBookingSugestions(
                treatments,
                startDate: DateOnly.FromDateTime(DateTime.Today),
                numberOfDaysToCheck: 1,
                neededSuggestions: 1,
                interval: 60
            );

            // Assert
            Assert.That(suggestions.Count, Is.LessThanOrEqualTo(1));
        }
    }
}

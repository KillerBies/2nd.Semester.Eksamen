using _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.DomainServiceTests
{
    [TestFixture]
    public class BookingDomainServiceTests
    {
        private Mock<IBookingRepository> _bookingRepoMock = null!;
        private Mock<ITreatmentBookingRepository> _treatmentBookingRepoMock = null!;
        private Mock<IEmployeeRepository> _employeeRepoMock = null!;
        private BookingDomainService _service = null!;

        [SetUp]
        public void Setup()
        {
            _bookingRepoMock = new Mock<IBookingRepository>();
            _treatmentBookingRepoMock = new Mock<ITreatmentBookingRepository>();
            _employeeRepoMock = new Mock<IEmployeeRepository>();

            _service = new BookingDomainService(_bookingRepoMock.Object, _treatmentBookingRepoMock.Object, _employeeRepoMock.Object);
        }

        // IsCustomerBookingOverlappingAsync

        [Test]
        public async Task IsCustomerBookingOverlappingAsync_ReturnsTrue_WhenNoOverlap()
        {
            var customerId = 1;
            var start = new DateTime(2025, 1, 1, 10, 0, 0);
            var end = new DateTime(2025, 1, 1, 12, 0, 0);

            // No bookings
            _bookingRepoMock.Setup(r => r.GetByCustomerId(customerId))
                .ReturnsAsync(new List<Booking>());

            var result = await _service.IsCustomerBookingOverlappingAsync(customerId, start, end);

            Assert.That(result, Is.True); // true means "no overlap" in your service
        }

        [Test]
        public async Task IsCustomerBookingOverlappingAsync_ReturnsFalse_WhenOverlapExists()
        {
            var customerId = 1;
            var start = new DateTime(2025, 1, 1, 10, 0, 0);
            var end = new DateTime(2025, 1, 1, 12, 0, 0);

            var booking = new Booking
            {
                Start = new DateTime(2025, 1, 1, 11, 0, 0),
                End = new DateTime(2025, 1, 1, 13, 0, 0)
            };

            _bookingRepoMock.Setup(r => r.GetByCustomerId(customerId))
                .ReturnsAsync(new List<Booking> { booking });

            var result = await _service.IsCustomerBookingOverlappingAsync(customerId, start, end);

            Assert.That(result, Is.False); // false because there is an overlap
        }

        // IsEmployeeBookingOverlapping

        [Test]
        public async Task IsEmployeeBookingOverlapping_ReturnsTrue_WhenOverlapExists()
        {
            // Arrange: Create a professional employee
            var employeeId = 1;
            var employee = new Employee(
                firstname: "John",
                lastname: "Doe",
                email: "john.doe@example.com",
                phoneNumber: "555-0100",
                address: new Address("Vejle", "7100", "Kolding Vej", "15"),
                basePriceMultiplier: 1.2m,
                experience: "Senior",
                type: "Staff",
                specialties: "Massage",
                gender: "Male",
                workStart: new TimeOnly(9, 0),
                workEnd: new TimeOnly(17, 0)
            );

            // Arrange: Create a treatment
            var treatment = new Treatment(
                name: "Shoulder Massage",
                price: 200m,
                discription: "The best shoulder massage",
                category: "Massage",
                duration: TimeSpan.FromHours(1)
            );

            // Arrange: Add an existing booking
            var existingBooking = new TreatmentBooking(treatment, employee,
                start: new DateTime(2025, 1, 1, 10, 0, 0),
                end: new DateTime(2025, 1, 1, 11, 0, 0)
            );
            employee.Appointments.Add(existingBooking);

            // Mock employee repository
            _employeeRepoMock.Setup(repo => repo.GetByIDAsync(employeeId))
                .ReturnsAsync(employee);

            // Act: Check for overlap with a new booking
            var overlapStart = new DateTime(2025, 1, 1, 10, 30, 0);
            var overlapEnd = new DateTime(2025, 1, 1, 11, 30, 0);
            var result = await _service.IsEmployeeBookingOverlapping(employeeId, overlapStart, overlapEnd);

            // Assert: Should detect overlap
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsEmployeeBookingOverlapping_ReturnsFalse_WhenNoOverlap()
        {
            // Arrange: Create a professional employee
            var employeeId = 1;
            var employee = new Employee(
                firstname: "John",
                lastname: "Doe",
                email: "john.doe@example.com",
                phoneNumber: "555-0100",
                address: new Address("Vejle", "7100", "Kolding Vej", "15"),
                basePriceMultiplier: 1.2m,
                experience: "Senior",
                type: "Staff",
                specialties: "Massage",
                gender: "Male",
                workStart: new TimeOnly(9, 0),
                workEnd: new TimeOnly(17, 0)
            );

            // Arrange: Create a treatment
            var treatment = new Treatment(
                name: "Shoulder Massage",
                price: 200m,
                discription: "The best shoulder massage",
                category: "Massage",
                duration: TimeSpan.FromHours(1)
            );

            // Arrange: Add an existing booking that does NOT overlap
            var existingBooking = new TreatmentBooking(treatment, employee,
                start: new DateTime(2025, 1, 1, 11, 0, 0),  // starts AFTER the range we test
                end: new DateTime(2025, 1, 1, 12, 0, 0)
            );
            employee.Appointments.Add(existingBooking);

            // Mock employee repository
            _employeeRepoMock.Setup(r => r.GetByIDAsync(employeeId))
                .ReturnsAsync(employee);

            // Act: Check for overlap with a new booking
            var testStart = new DateTime(2025, 1, 1, 9, 0, 0);   // before existing booking
            var testEnd = new DateTime(2025, 1, 1, 10, 0, 0);    // ends before existing booking
            var result = await _service.IsEmployeeBookingOverlapping(employeeId, testStart, testEnd);

            // Assert: Should detect NO overlap
            Assert.That(result, Is.False);
        }

    }
}

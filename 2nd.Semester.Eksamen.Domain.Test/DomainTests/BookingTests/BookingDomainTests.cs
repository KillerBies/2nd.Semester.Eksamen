using NUnit.Framework;
using System;
using System.Collections.Generic;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;

namespace _2nd.Semester.Eksamen.Domain.Test.DomainTests.BookingTests
{
    [TestFixture]
    public class BookingDomainTests
    {

        private Customer CreateCustomer(int id = 1)
        {
            return new Customer(
                name: "Test Customer",
                email: "test@example.com",
                phoneNumber: "12345678",
                address: new Address("City", "0000", "Street", "10"),
                notes: "Test Note",
                saveAsCustomer: true
            )
            {
                Id = id
            };
        }

        private TreatmentBooking CreateTreatmentBooking(DateTime start, DateTime end)
        {
            var treatment = new Treatment(
                name: "Test Treatment",
                price: 100,
                discription: "desc",
                category: "cat",
                duration: TimeSpan.FromMinutes(30)
            );

            var employee = new Employee(
                firstname: "Emp",
                lastname: "Worker",
                email: "emp@example.com",
                phoneNumber: "11111111",
                address: new Address("City", "0000", "Road", "22"),
                basePriceMultiplier: 1.0m,
                experience: "Expert",
                type: "Massage",
                specialties: "Deep",
                gender: "Male",
                workStart: new TimeOnly(9, 0),
                workEnd: new TimeOnly(17, 0)
            );

            return new TreatmentBooking(treatment, employee, start, end);
        }

        // Constructors

        [Test]
        public void Constructor_WithCustomer_SetsPropertiesCorrectly()
        {
            var customer = CreateCustomer(5);
            DateTime start = new DateTime(2025, 1, 1, 12, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 13, 0, 0);

            var treatments = new List<TreatmentBooking>
            {
                CreateTreatmentBooking(start, end)
            };

            var booking = new Booking(customer, start, end, treatments);

            Assert.That(booking.CustomerId, Is.EqualTo(5));
            Assert.That(booking.Customer, Is.EqualTo(customer));
            Assert.That(booking.Start, Is.EqualTo(start));
            Assert.That(booking.End, Is.EqualTo(end));
            Assert.That(booking.Duration, Is.EqualTo(end - start));
            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Pending));
            Assert.That(booking.Treatments.Count, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithCustomerId_SetsPropertiesCorrectly()
        {
            int customerId = 22;
            DateTime start = new DateTime(2025, 1, 1, 10, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 11, 0, 0);

            var treatments = new List<TreatmentBooking>();

            var booking = new Booking(customerId, start, end, treatments);

            Assert.That(booking.CustomerId, Is.EqualTo(customerId));
            Assert.That(booking.Start, Is.EqualTo(start));
            Assert.That(booking.End, Is.EqualTo(end));
            Assert.That(booking.Duration, Is.EqualTo(end - start));
            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Pending));
        }

        // TryChangeStatus

        [Test]
        public void TryChangeStatus_AlwaysReturnsTrue_AndUpdatesStatus()
        {
            var booking = new Booking();
            bool result = booking.TryChangeStatus(BookingStatus.Completed);

            Assert.That(result, Is.True);
            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Completed));
        }

        // TryAddTreatment

        [Test]
        public void TryAddTreatment_ValidTreatment_ReturnsTrue_AndAdds()
        {
            var booking = new Booking();
            var treatment = CreateTreatmentBooking(
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 11, 0, 0)
            );

            bool result = booking.TryAddTreatment(treatment);

            Assert.That(result, Is.True);
            Assert.That(booking.Treatments.Count, Is.EqualTo(1));
        }

        [Test]
        public void TryAddTreatment_Null_ReturnsFalse_AndDoesNotAdd()
        {
            var booking = new Booking();

            bool result = booking.TryAddTreatment(null);

            Assert.That(result, Is.False);
            Assert.That(booking.Treatments.Count, Is.EqualTo(0));
        }

        // FinishBooking

        [Test]
        public void FinishBooking_SetsStatusToCompleted()
        {
            var booking = new Booking();
            booking.FinishBooking();

            Assert.That(booking.Status, Is.EqualTo(BookingStatus.Completed));
        }

        // Overlaps()

        [Test]
        public void Overlaps_WhenOverlapExists_ReturnsTrue()
        {
            var booking = new Booking
            {
                Start = new DateTime(2025, 1, 1, 10, 0, 0),
                End = new DateTime(2025, 1, 1, 12, 0, 0)
            };

            bool result = booking.Overlaps(
                new DateTime(2025, 1, 1, 11, 0, 0),
                new DateTime(2025, 1, 1, 13, 0, 0)
            );

            Assert.That(result, Is.True);
        }

        [Test]
        public void Overlaps_NoOverlap_ReturnsFalse()
        {
            var booking = new Booking
            {
                Start = new DateTime(2025, 1, 1, 8, 0, 0),
                End = new DateTime(2025, 1, 1, 9, 0, 0)
            };

            bool result = booking.Overlaps(
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 11, 0, 0)
            );

            Assert.That(result, Is.False);
        }

        [Test]
        public void Overlaps_TouchingButNotOverlapping_ReturnsFalse()
        {
            var booking = new Booking
            {
                Start = new DateTime(2025, 1, 1, 10, 0, 0),
                End = new DateTime(2025, 1, 1, 11, 0, 0)
            };

            bool result = booking.Overlaps(
                new DateTime(2025, 1, 1, 9, 0, 0),
                new DateTime(2025, 1, 1, 10, 0, 0)
            );

            Assert.That(result, Is.False);
        }
    }
}

using NUnit.Framework;
using System;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;

namespace _2nd.Semester.Eksamen.Domain.Test.DomainTests.TreatmentBookingTests
{
    [TestFixture]
    public class TreatmentBookingTests
    {
        private Employee CreateEmployee()
        {
            return new Employee(
                firstname: "John",
                lastname: "Doe",
                email: "John.Doe@example.com",
                phoneNumber: "12345678",
                address: new Address("Vejle", "7100", "Havnegade", "12A"),
                basePriceMultiplier: 1.0m,
                experience: "Expert",
                type: "Therapist",
                specialties: "Massage",
                gender: "Male",
                workStart: new TimeOnly(9, 0),
                workEnd: new TimeOnly(17, 0)
            );
        }

        private Treatment CreateTreatment()
        {
            return new Treatment(
                name: "Relaxing Massage",
                price: 100m,
                discription: "Full body massage",
                category: "Massage",
                duration: TimeSpan.FromMinutes(60)
            );
        }

        // TrySetTimeRange

        [Test]
        public void TrySetTimeRange_StartBeforeEnd_ReturnsTrue_AndSetsCorrectly()
        {
            var booking = new TreatmentBooking();
            DateTime start = new DateTime(2025, 05, 10, 10, 0, 0);
            DateTime end = new DateTime(2025, 05, 10, 11, 0, 0);

            bool success = booking.TrySetTimeRange(start, end);

            Assert.That(success, Is.True);
            Assert.That(booking.Start, Is.EqualTo(start));
            Assert.That(booking.End, Is.EqualTo(end));
        }

        [Test]
        public void TrySetTimeRange_EndBeforeStart_SwapsCorrectly()
        {
            var booking = new TreatmentBooking();
            DateTime start = new DateTime(2025, 05, 10, 14, 0, 0);
            DateTime end = new DateTime(2025, 05, 10, 12, 0, 0);

            bool success = booking.TrySetTimeRange(start, end);

            Assert.That(success, Is.True);
            Assert.That(booking.Start, Is.EqualTo(end));   // swapped here
            Assert.That(booking.End, Is.EqualTo(start));   // swapped here
        }

        [Test]
        public void TrySetTimeRange_StartEqualsEnd_ReturnsFalse()
        {
            var booking = new TreatmentBooking();
            DateTime time = new DateTime(2025, 05, 10, 12, 0, 0);

            bool success = booking.TrySetTimeRange(time, time);

            Assert.That(success, Is.False);
        }

        // Constructors

        [Test]
        public void Constructor_WithTreatmentAndEmployee_SetsStartAndEnd()
        {
            var treatment = CreateTreatment();
            var employee = CreateEmployee();

            DateTime start = new DateTime(2025, 05, 10, 9, 0, 0);
            DateTime end = new DateTime(2025, 05, 10, 10, 0, 0);

            var booking = new TreatmentBooking(treatment, employee, start, end);

            Assert.That(booking.Start, Is.EqualTo(start));
            Assert.That(booking.End, Is.EqualTo(end));
            Assert.That(booking.Employee, Is.EqualTo(employee));
            Assert.That(booking.Treatment, Is.EqualTo(treatment));
        }

        [Test]
        public void Constructor_WithIds_SetsOnlyIds_AndTimeRange()
        {
            int treatmentId = 22;
            int employeeId = 7;

            DateTime start = new DateTime(2025, 05, 10, 15, 0, 0);
            DateTime end = new DateTime(2025, 05, 10, 16, 0, 0);

            var booking = new TreatmentBooking(treatmentId, employeeId, start, end);

            Assert.That(booking.Start, Is.EqualTo(start));
            Assert.That(booking.End, Is.EqualTo(end));
            Assert.That(booking.TreatmentId, Is.EqualTo(treatmentId));
            Assert.That(booking.EmployeeId, Is.EqualTo(employeeId));
        }

        [Test]
        public void Constructor_WithTreatmentAndEmployeeId_SetsCorrectValues()
        {
            var treatment = CreateTreatment();
            int employeeId = 3;

            DateTime start = new DateTime(2025, 05, 10, 13, 0, 0);
            DateTime end = new DateTime(2025, 05, 10, 14, 0, 0);

            var booking = new TreatmentBooking(treatment, employeeId, start, end);

            Assert.That(booking.Start, Is.EqualTo(start));
            Assert.That(booking.End, Is.EqualTo(end));
            Assert.That(booking.EmployeeId, Is.EqualTo(employeeId));
            Assert.That(booking.Treatment, Is.EqualTo(treatment));
        }

        // Overlaps

        [Test]
        public void Overlaps_WhenRangesOverlap_ReturnsTrue()
        {
            var treatment = CreateTreatment();
            var employee = CreateEmployee();

            var booking = new TreatmentBooking(
                treatment,
                employee,
                new DateTime(2025, 05, 10, 10, 0, 0),
                new DateTime(2025, 05, 10, 12, 0, 0)
            );

            bool result = booking.Overlaps(
                new DateTime(2025, 05, 10, 11, 0, 0),
                new DateTime(2025, 05, 10, 13, 0, 0)
            );

            Assert.That(result, Is.True);
        }

        [Test]
        public void Overlaps_WhenCompletelySeparate_ReturnsFalse()
        {
            var treatment = CreateTreatment();
            var employee = CreateEmployee();

            var booking = new TreatmentBooking(
                treatment,
                employee,
                new DateTime(2025, 05, 10, 8, 0, 0),
                new DateTime(2025, 05, 10, 9, 0, 0)
            );

            bool result = booking.Overlaps(
                new DateTime(2025, 05, 10, 10, 0, 0),
                new DateTime(2025, 05, 10, 11, 0, 0)
            );

            Assert.That(result, Is.False);
        }

        [Test]
        public void Overlaps_WhenTouchingEdge_ReturnsFalse()
        {
            var treatment = CreateTreatment();
            var employee = CreateEmployee();

            var booking = new TreatmentBooking(
                treatment,
                employee,
                new DateTime(2025, 05, 10, 10, 0, 0),
                new DateTime(2025, 05, 10, 11, 0, 0)
            );

            // ends exactly when booking starts → NOT overlapping
            bool result = booking.Overlaps(
                new DateTime(2025, 05, 10, 9, 0, 0),
                new DateTime(2025, 05, 10, 10, 0, 0)
            );

            Assert.That(result, Is.False);
        }
    }
}

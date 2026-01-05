using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2nd.Semester.Eksamen.Domain.Test.DomainTests.ScheduleDayTests
{
    [TestFixture]
    public class ScheduleDayDomainTests
    {
        private ScheduleDay CreateDay()
        {
            return new ScheduleDay(
                new DateOnly(2025, 1, 1),
                new TimeOnly(9, 0),
                new TimeOnly(17, 0)
            );
        }

        private TreatmentBooking CreateBooking(DateTime start, DateTime end)
        {
            var duration = end - start;

            var treatment = new Treatment(
                name: "Shoulder Massage",
                price: 100,
                discription: "Massage for your shoulders",
                category: "Massage",
                duration: duration
            );

            var emp = new Employee();

            return new TreatmentBooking(treatment, emp, start, end);
        }


        // CONSTRUCTOR TEST
        [Test]
        public void Constructor_AddsInitialFreetime()
        {
            var day = CreateDay();

            Assert.That(day.TimeRanges.Count, Is.EqualTo(1));
            var tr = day.TimeRanges.First();
            Assert.That(tr.Type, Is.EqualTo("Freetime"));
            Assert.That(tr.Start, Is.EqualTo(new TimeOnly(9, 0)));
            Assert.That(tr.End, Is.EqualTo(new TimeOnly(17, 0)));
        }

        // BookDayForVacation

        [Test]
        public void BookDayForVacation_ReplacesAllTimeRanges()
        {
            var day = CreateDay();

            var id = Guid.NewGuid();
            var result = day.BookDayForVacation(id, new TimeOnly(12, 0), new TimeOnly(16, 0));

            Assert.That(result, Is.True);
            Assert.That(day.TimeRanges.Count, Is.EqualTo(1));
            Assert.That(day.TimeRanges.First().Type, Is.EqualTo("Unavailable"));
        }

        [Test]
        public void BookDayForVacation_Fails_WhenOtherNonFreeRangesExist()
        {
            var day = CreateDay();
            // Add a booked range
            day.TimeRanges.Add(new TimeRange(new TimeOnly(9, 0), new TimeOnly(10, 0)) { Type = "Booked" });

            var result = day.BookDayForVacation(Guid.NewGuid(), new TimeOnly(12, 0), new TimeOnly(14, 0));

            Assert.That(result, Is.False);
        }

        // AvailableInTimeRange

        [Test]
        public void AvailableInTimeRange_True_WhenNoOverlapWithNonFree()
        {
            var day = CreateDay();

            var range = new TimeRange(new TimeOnly(10, 0), new TimeOnly(11, 0));
            Assert.That(day.AvailableInTimeRange(range), Is.True);
        }

        [Test]
        public void AvailableInTimeRange_False_WhenOverlapWithBooked()
        {
            var day = CreateDay();
            day.TimeRanges.Add(new TimeRange(new TimeOnly(10, 0), new TimeOnly(12, 0)) { Type = "Booked" });

            var range = new TimeRange(new TimeOnly(11, 0), new TimeOnly(13, 0));

            Assert.That(day.AvailableInTimeRange(range), Is.False);
        }

        // CheckIfAvailable

        [Test]
        public void CheckIfAvailable_True_WhenFreetimeBigEnough()
        {
            var day = CreateDay();
            Assert.That(day.CheckIfAvailable(TimeSpan.FromHours(1)), Is.True);
        }

        [Test]
        public void CheckIfAvailable_False_WhenNotEnoughTime()
        {
            var day = CreateDay();
            day.TimeRanges = new List<TimeRange>
            {
                new TimeRange(new TimeOnly(9,0), new TimeOnly(9,30)) { Type="Freetime" }
            };

            Assert.That(day.CheckIfAvailable(TimeSpan.FromHours(1)), Is.False);
        }

        // GetAvailableTimeRange

        [Test]
        public void GetAvailableTimeRange_ReturnsCorrectSlot()
        {
            var day = CreateDay();

            var slot = day.GetAvailableTimeRange(TimeSpan.FromHours(2));

            Assert.That(slot, Is.Not.Null);
            Assert.That(slot.Duration, Is.EqualTo(TimeSpan.FromHours(8)));
        }

        // GetAllAvailableSlots

        [Test]
        public void GetAllAvailableSlots_ReturnsAllValidFreetime()
        {
            var day = CreateDay();
            var slots = day.GetAllAvailableSlots(TimeSpan.FromHours(1));

            Assert.That(slots.Count(), Is.EqualTo(1));
            Assert.That(slots.First().Type, Is.EqualTo("Freetime"));
        }

        // GetOverlappingFreetime

        [Test]
        public void GetOverlappingFreetime_ReturnsCorrectRanges()
        {
            var day = CreateDay();

            var bookingRange = new TimeRange(new TimeOnly(10, 0), new TimeOnly(11, 0));
            var result = day.GetOverlappingFreetime(bookingRange);

            Assert.That(result.Any(), Is.True);
        }

        // AddBooking

        [Test]
        public void AddBooking_SplitsFreetimeCorrectly()
        {
            var day = CreateDay();

            var booking = CreateBooking(
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 12, 0, 0)
            );

            var id = Guid.NewGuid();

            var result = day.AddBooking(booking, id);

            Assert.That(result, Is.True);
            Assert.That(day.TimeRanges.Count, Is.EqualTo(3)); // before, booked, after

            Assert.That(day.TimeRanges.Any(r => r.Type == "Booked"), Is.True);
        }

        [Test]
        public void AddBooking_Fails_WhenNoFreetimeCoversRange()
        {
            var day = CreateDay();
            day.TimeRanges = new List<TimeRange> // no coverage
            {
                new TimeRange(new TimeOnly(13,0), new TimeOnly(14,0)) { Type = "Freetime" }
            };

            var booking = CreateBooking(
                new DateTime(2025, 1, 1, 9, 0, 0),
                new DateTime(2025, 1, 1, 10, 0, 0)
            );

            var result = day.AddBooking(booking, Guid.NewGuid());

            Assert.That(result, Is.False);
        }

        // CancelBooking

        [Test]
        public void CancelBooking_ReturnsSlotToFreetime()
        {
            var day = CreateDay();

            var booking = CreateBooking(
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 12, 0, 0)
            );

            var id = Guid.NewGuid();

            day.AddBooking(booking, id);
            var cancel = day.CancelBooking(booking);

            Assert.That(cancel, Is.True);
            Assert.That(day.TimeRanges.Any(r => r.Type == "Freetime"), Is.True);
        }

        // UpdateDaySchedule

        [Test]
        public void UpdateDaySchedule_UpdatesBookingAndCreatesFreetime()
        {
            // Arrange: Create a professional employee
            var employee = new Employee(
                firstname: "John",
                lastname: "Doe",
                email: "john.doe@corporate.com",
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

            // Arrange: Create a ScheduleDay for the employee
            var day = new ScheduleDay(DateOnly.FromDateTime(new DateTime(2025, 1, 1)), employee.WorkStart, employee.WorkEnd)
            {
                Employee = employee,
                EmployeeId = 1
            };

            // Arrange: Create a treatment and booking
            var treatment = new Treatment(
                name: "Shoulder Massage",
                price: 250m,
                discription: "Best shoulder massage",
                category: "Massage",
                duration: TimeSpan.FromHours(2)
            );

            var booking = new TreatmentBooking(treatment, employee,
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 12, 0, 0)
            );

            // Arrange: Generate ActivityGuid and add booking to the day
            var activityId = Guid.NewGuid();
            day.AddBooking(booking, activityId);

            // Act: Update the schedule **first** (simulate the employee rescheduling)
            var newStart = new DateTime(2025, 1, 1, 11, 0, 0);
            var newEnd = new DateTime(2025, 1, 1, 13, 0, 0);

            // You can temporarily create a "rescheduled booking" object to pass
            var tempBooking = new TreatmentBooking(treatment, employee, newStart, newEnd);

            var result = day.UpdateDaySchedule(tempBooking, activityId);

            // Assert: Schedule updated and freetime properly created
            Assert.That(result, Is.True);
            Assert.That(day.TimeRanges.Any(r => r.Type == "Freetime"), Is.True);
        }




        [Test]
        public void UpdateDaySchedule_Fails_WhenBookingNotFound()
        {
            var day = CreateDay();

            var booking = CreateBooking(
                new DateTime(2025, 1, 1, 10, 0, 0),
                new DateTime(2025, 1, 1, 12, 0, 0)
            );

            var result = day.UpdateDaySchedule(booking, Guid.NewGuid());

            Assert.That(result, Is.False);
        }
    }
}

using NUnit.Framework;
using System;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;

namespace Tests.ScheduleTests
{
    [TestFixture]
    public class TimeRangeDomainTests
    {
        // Constructor

        [Test]
        public void Constructor_SetsStartAndEnd()
        {
            var start = new TimeOnly(9, 0);
            var end = new TimeOnly(17, 0);
            var range = new TimeRange(start, end);

            Assert.That(range.Start, Is.EqualTo(start));
            Assert.That(range.End, Is.EqualTo(end));
        }

        // Duration

        [Test]
        public void Duration_CalculatesCorrectly()
        {
            var start = new TimeOnly(9, 0);
            var end = new TimeOnly(12, 30);
            var range = new TimeRange(start, end);

            Assert.That(range.Duration, Is.EqualTo(TimeSpan.FromHours(3.5)));
        }

        // HasOverlap

        [Test]
        public void HasOverlap_ReturnsTrue_WhenRangesOverlap()
        {
            var range1 = new TimeRange(new TimeOnly(9, 0), new TimeOnly(11, 0));
            var range2 = new TimeRange(new TimeOnly(10, 0), new TimeOnly(12, 0));

            Assert.That(range1.HasOverlap(range2), Is.True);
            Assert.That(range2.HasOverlap(range1), Is.True);
        }

        [Test]
        public void HasOverlap_ReturnsFalse_WhenRangesDoNotOverlap()
        {
            var range1 = new TimeRange(new TimeOnly(9, 0), new TimeOnly(11, 0));
            var range2 = new TimeRange(new TimeOnly(11, 0), new TimeOnly(13, 0));

            Assert.That(range1.HasOverlap(range2), Is.False);
            Assert.That(range2.HasOverlap(range1), Is.False);
        }

        [Test]
        public void HasOverlap_ReturnsFalse_WhenRangesTouchButDoNotOverlap()
        {
            var range1 = new TimeRange(new TimeOnly(9, 0), new TimeOnly(10, 0));
            var range2 = new TimeRange(new TimeOnly(10, 0), new TimeOnly(11, 0));

            Assert.That(range1.HasOverlap(range2), Is.False);
        }

        // Default Constructor

        [Test]
        public void DefaultConstructor_InitializesProperties()
        {
            var range = new TimeRange();

            Assert.That(range.Start, Is.EqualTo(default(TimeOnly)));
            Assert.That(range.End, Is.EqualTo(default(TimeOnly)));
            Assert.That(range.Type, Is.Null.Or.Empty);
            Assert.That(range.Name, Is.EqualTo(""));
            Assert.That(range.ActivityId, Is.Null);
            Assert.That(range.Booking, Is.Null);
        }
    }
}

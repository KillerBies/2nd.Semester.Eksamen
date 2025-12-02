using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules
{
    public class TimeRange : BaseEntity
    {
        public int ScheduleDayId { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
        public string Name { get; set; } = "";
        public int? BookingID { get; set; }
        public Booking? Booking { get; set; }
        public TimeRangeType Type { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public TimeSpan Duration => End - Start;
        public TimeRange()
        { }
        public TimeRange(TimeOnly start, TimeOnly end)
        {
            Start = start;
            End = end;
        }
        public bool HasOverlap(TimeRange other)
        {
            return Start < other.End && End > other.Start;
        }
    }
}

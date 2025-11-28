using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules
{
    public class TimeRange : BaseEntity
    {
        public ScheduleDay ScheduleDay { get; set; }
        public string Name { get; set; } = "";
        public TimeRangeType Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration => End - Start;
        public TimeRange()
        { }
        public TimeRange(DateTime start, DateTime end)
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

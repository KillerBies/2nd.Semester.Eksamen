using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities
{
    public class TimeRange
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeRange(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }
    }
}

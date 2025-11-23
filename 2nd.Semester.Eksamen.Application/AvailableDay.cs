using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities;

namespace _2nd.Semester.Eksamen.Application
{
    public class AvailableDay
    {
        public DayOfWeek Day { get; set; }
        public List<TimeRange> Times { get; set; } = new List<TimeRange>();
    }
}

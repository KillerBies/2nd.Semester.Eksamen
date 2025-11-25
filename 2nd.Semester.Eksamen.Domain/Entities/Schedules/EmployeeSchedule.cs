using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules
{
    public class EmployeeSchedule
    {
        private Dictionary<DateOnly, ScheduleDay> _days = new();

        public ScheduleDay GetOrCreateDay(DateOnly date)
        {
            if (!_days.TryGetValue(date, out var day))
            {
                day = new ScheduleDay(date.ToDateTime(new TimeOnly(0, 0)), new TimeOnly(8, 0), new TimeOnly(16, 0));
                _days[date] = day;
            }
            return day;
        }
    }
}

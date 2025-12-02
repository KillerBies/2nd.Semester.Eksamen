using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class AvailableDayDTO
    {
        public DayOfWeek Day { get; set; }
        public List<TimeRange> Times { get; set; } = new List<TimeRange>();
    }
}

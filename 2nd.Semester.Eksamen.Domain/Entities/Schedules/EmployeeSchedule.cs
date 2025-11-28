using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules
{
    public class EmployeeSchedule
    {
        public Employee Employee { get; }

        private Dictionary<DateOnly, ScheduleDay> _days = new();
        public ScheduleDay GetOrCreateDay(DateOnly date, TimeOnly workStart, TimeOnly workEnd)
        {
            if (!_days.TryGetValue(date, out var day))
            {
                day = new ScheduleDay(date.ToDateTime(new TimeOnly(0, 0)), workStart, workEnd);
                _days[date] = day;
            }
            return day;
        }

        public bool BookTreatmentOnDate(TreatmentBooking bookedTreatment)
        {
            if(_days[DateOnly.FromDateTime(bookedTreatment.Start)].AddBooking(new TimeRange() { Name = bookedTreatment.Treatment.Name, Start = bookedTreatment.Start, End = bookedTreatment.End, Type = TimeRangeType.Booked })) return true;
            return false;
        }
        public bool CancelBookedTreatment(TreatmentBooking bookedTreatment)
        {
            if (_days[DateOnly.FromDateTime(bookedTreatment.Start)].DeleteBooking(bookedTreatment.Start,bookedTreatment.End)) return true;
            return false;
        }
        public IEnumerable<ScheduleDay> GetUpcomingDays(DateOnly startDate, int numberOfDays)
        {
            var daysList = new List<ScheduleDay>();
            for (int i = 0; i < numberOfDays; i++)
            {
                var date = startDate.AddDays(i);
                daysList.Add(GetOrCreateDay(date, new TimeOnly(8, 0), new TimeOnly(16, 0)));
            }
            return daysList;
        }
        public bool IsAvailableOnDate(DateTime date, TimeSpan duration)
        {
            return (!(_days.ContainsKey(DateOnly.FromDateTime(date)) || _days[DateOnly.FromDateTime(date)].CheckIfAvailable(duration)));
        }
        public bool IsAvailableInTimeRange(TimeRange timeRange)
        {
            var date = DateOnly.FromDateTime(timeRange.Start);
            return (!(_days.ContainsKey((date))) || _days[date].AvailableInTimeRange(timeRange));
        }
        public bool 
    }
}

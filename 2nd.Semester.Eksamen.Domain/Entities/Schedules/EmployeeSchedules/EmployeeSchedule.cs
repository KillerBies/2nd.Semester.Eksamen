//using _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService;
//using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
//using _2nd.Semester.Eksamen.Domain.Entities.Products;
//using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
//using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules
//{
//    public class EmployeeSchedule 
//    {
//        private Dictionary<DateOnly, ScheduleDay> _days { get; set; } = new();
//        public List<ScheduleDay> Days { get; set; }
//        IScheduleDayRepository _dayRepository;
//        public EmployeeSchedule(IScheduleDayRepository scheduleDayRepository)
//        {
//            _dayRepository = scheduleDayRepository;
//        }
//        public ScheduleDay GetOrCreateDay(DateOnly date, TimeOnly workStart, TimeOnly workEnd)
//        {
//            if (!_days.TryGetValue(date, out var day))
//            {
//                day = new ScheduleDay(date, workStart, workEnd);
//                _days[date] = day;
//            }
//            return day;
//        }

//        public bool BookTreatmentOnDate(DateTime start, DateTime end, string name, TimeOnly workStart, TimeOnly workEnd, int bookingId=default)
//        {
//            if (bookingId == default) bookingId = 0;
//            var day = GetOrCreateDay(DateOnly.FromDateTime(start), workStart, workEnd);
//            DateOnly Date = DateOnly.FromDateTime(start);
//            if (day.AddBooking(new TimeRange(start, end) {Type = TimeRangeType.Booked, BookingID=bookingId, Name=name})) return true;
//            return false;
//        }
//        public bool CancelBookedTreatment(DateTime start, DateTime end, int bookingId = default)
//        {
//            if (bookingId == default) bookingId = 0;
//            DateOnly Date = DateOnly.FromDateTime(start);
//            if (_days[Date].CancelBooking(start, end, bookingId)) return true;
//            return false;
//        }
//        public bool UpdateBooking(DateTime start, DateTime end, int bookingId = default)
//        {
//            if (bookingId == default) bookingId = 0;
//            DateOnly Date = DateOnly.FromDateTime(start);
//            if (_days[Date].UpdateDaySchedule(start, end, bookingId)) return true;
//            return false;
//        }
//        public IEnumerable<ScheduleDay> GetUpcomingDays(DateOnly startDate, int numberOfDays)
//        {
//            var daysList = new List<ScheduleDay>();
//            for (int i = 0; i < numberOfDays; i++)
//            {
//                var date = startDate.AddDays(i);
//                daysList.Add(GetOrCreateDay(date, new TimeOnly(8, 0), new TimeOnly(16, 0)));
//            }
//            return daysList;
//        }
//        public bool IsAvailableOnDate(DateTime date, TimeSpan duration)
//        {
//            return !(_days.ContainsKey(DateOnly.FromDateTime(date)) || _days[DateOnly.FromDateTime(date)].CheckIfAvailable(duration));
//        }
//        public bool IsAvailableInTimeRange(TimeRange timeRange)
//        {
//            var date = DateOnly.FromDateTime(timeRange.Start);
//            return !_days.ContainsKey(date) || _days[date].AvailableInTimeRange(timeRange);
//        }
       
//    }
//}

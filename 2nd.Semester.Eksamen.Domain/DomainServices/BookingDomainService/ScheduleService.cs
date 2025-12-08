//using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
//using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
//using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService
//{
//    public class ScheduleService
//    {
//        private readonly IEmployeeRepository _employeeRepository;
//        private readonly int EmployeeId;
//        public ScheduleService(IEmployeeRepository employeeRepository, int employeeId)
//        {
//            _employeeRepository = employeeRepository;
//            // Map to a dictionary keyed by DateOnly
//            EmployeeId = employeeId;
//        }
//        var scheduleDays = await _employeeRepository.ScheduleDays.Include(d => d.TimeRanges).Where(d => d.EmployeeId == employeeId).ToListAsync();
//        public ScheduleDay GetOrCreateDay(DateOnly date, TimeOnly workStart, TimeOnly workEnd)
//        {
//            if (!_days.TryGetValue(date, out var day))
//            {
//                day = new ScheduleDay(date, workStart, workEnd);
//                _days[date] = day;
//            }
//            return day;
//        }
//    }
//}

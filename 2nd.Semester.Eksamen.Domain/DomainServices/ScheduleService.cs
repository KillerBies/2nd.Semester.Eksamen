using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices
{
    public class ScheduleService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public ScheduleService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<bool> BookSchedule(int employeeId, DateTime start, DateTime end)
        {
            var employee = await _employeeRepository.GetByIDAsync(employeeId);
            if (employee == null) throw new Exception("Employee not found");
            return employee.Schedule.BookTreatmentOnDate(start, end);
        }
    }
}

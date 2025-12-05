using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleDayRepository _dayRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Domain_to_DTO domain_To_DTO;
        public ScheduleService(IScheduleDayRepository dayRepository, IEmployeeRepository employeeRepository, Domain_to_DTO domain_to_DTO) 
        {
            _dayRepository = dayRepository;
            _employeeRepository = employeeRepository;
            domain_To_DTO = domain_to_DTO;
        }
        public async Task<List<ScheduleDay>> GetEmployeeSchedule(int employeeId)
        {
            return (List <ScheduleDay>) await _dayRepository.GetByEmployeeIDAsync(employeeId);
        }
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            var employees = (await _employeeRepository.GetAllAsync());
            return employees.Select(e => domain_To_DTO.EmployeeToDTO(e)).ToList();
        }

    }
}

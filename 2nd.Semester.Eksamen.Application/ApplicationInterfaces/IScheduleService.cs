using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IScheduleService
    {
        public Task<List<ScheduleDay>> GetEmployeeSchedule(int employeeId);
        public Task<IEnumerable<EmployeeDTO>> GetEmployees();
        public Task CreateEmployeeVication(EmployeeVicationDTO vicationDTO);
    }
}

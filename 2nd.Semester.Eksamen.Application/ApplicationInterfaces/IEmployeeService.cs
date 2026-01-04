using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task<EmployeeDetailsDTO> GetByIdAsync(int id);
        Task UpdateEmployeeAsync(Employee employee);
        Task<EmployeeDetailsDTO?> GetEmployeeDetailsAsync(int employeeId);
        Task<IEnumerable<EmployeeUserCardDTO>> GetAllEmployeeUserCardsAsync();
        Task<IEnumerable<EmployeeDetailsDTO>> GetAllEmployeeDetailsAsync();
        public Task<EmployeeDetailsDTO?> GetEmployeeByGuidAsync(Guid guid);



    }
}

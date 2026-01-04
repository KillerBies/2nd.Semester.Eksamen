using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class EmployeeUpdateService : IEmployeeUpdateService
    {
        public IEmployeeRepository _repo { get; set; }
        public EmployeeUpdateService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<EmployeeInputDTO> GetEmployeeInputDTOByIdAsync(int id)
        {
            var emp = (await _repo.GetByIDAsync(id));
            return new EmployeeInputDTO(emp);
        }
        public async Task UpdateEmployee(EmployeeInputDTO employee)
        {
            var address = new Address(employee.Address.City, employee.Address.PostalCode, employee.Address.StreetName, employee.Address.HouseNumber);
            await _repo.UpdateAsync(new Employee(employee.FirstName, employee.LastName, employee.Email, employee.PhoneNumber, address, employee.BasePriceMultiplier, employee.ExperienceLevel.ToString(), employee.Type.ToString(), string.Join(",", employee.SpecialtiesList), employee.Gender.ToString(), TimeOnly.FromTimeSpan(employee.WorkStart), TimeOnly.FromTimeSpan(employee.WorkEnd)) { Id = employee.id});
        }
    }
}

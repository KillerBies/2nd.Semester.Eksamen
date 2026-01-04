using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.PersonService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly Domain_to_DTO _domainToDTO;

        public EmployeeService(IEmployeeRepository employeeRepo, IAddressRepository addressRepo)
        {
            _employeeRepo = employeeRepo;
            _addressRepo = addressRepo;
            _domainToDTO = new Domain_to_DTO();
        }

        public async Task<EmployeeDetailsDTO> GetByIdAsync(int id)
        {
            var emp = await _employeeRepo.GetByIDAsync(id);

            if (emp == null) return null;

            return new EmployeeDetailsDTO(emp);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepo.UpdateAsync(employee);
        }
        public async Task<EmployeeDetailsDTO?> GetEmployeeDetailsAsync(int employeeId)
        {
            var employee = await _employeeRepo.GetByIDAsync(employeeId);
            if (employee == null) return null;

            // USE the new helper here
            return _domainToDTO.EmployeeToDetailsDTO(employee);
        }


        public async Task DeleteEmployeeAsync(int id)
        {
            var emp = await _employeeRepo.GetByIDAsync(id);
            if (emp == null)
                return;

            var address = emp.Address;

            await _employeeRepo.DeleteAsync(emp);

            if (emp.Address != null)
            {
                await _addressRepo.DeleteAsync(address);
            }
        }
        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _employeeRepo.CreateNewAsync(employee);
        }
        public async Task<EmployeeDetailsDTO?> GetEmployeeByGuidAsync(Guid guid)
        {
            var emp = await _employeeRepo.GetByGuidAsync(guid);
            if (emp == null) return null;
            return new EmployeeDetailsDTO(emp);
        }
        public async Task<IEnumerable<EmployeeUserCardDTO>> GetAllEmployeeUserCardsAsync()
        {
            var employees = await _employeeRepo.GetAllAsync();

            return employees.Select(e => new EmployeeUserCardDTO
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                PhoneNumber = e.PhoneNumber
                // Color can stay default
            }).ToList();
        }


    }


}

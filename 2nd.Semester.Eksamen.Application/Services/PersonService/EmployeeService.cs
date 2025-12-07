using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
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

        public EmployeeService(IEmployeeRepository employeeRepo, IAddressRepository addressRepo)
        {
            _employeeRepo = employeeRepo;
            _addressRepo = addressRepo;
        }

        public async Task<EmployeeDetailsDTO> GetByIdAsync(int id)
        {
            var emp = await _employeeRepo.GetByIDAsync(id);

            if (emp == null) return null;

            return new EmployeeDetailsDTO
            {
                Id = emp.Id,
                FirstName = emp.Name,
                LastName = emp.LastName,
                Type = emp.Type,
                Specialty = emp.Specialties,
                Experience = emp.ExperienceLevel,
                Gender = emp.Gender,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                BasePriceMultiplier = emp.BasePriceMultiplier,

                City = emp.Address?.City,
                PostalCode = emp.Address?.PostalCode,
                StreetName = emp.Address?.StreetName,
                HouseNumber = emp.Address?.HouseNumber
            };
        }

        public async Task UpdateEmployeeAsync(EmployeeDetailsDTO dto)
        {
            var emp = await _employeeRepo.GetByIDAsync(dto.Id);
            if (emp == null) return;

            // Update basic properties
            emp.TrySetName(dto.FirstName);
            emp.TrySetLastName(dto.FirstName, dto.LastName);
            emp.TrySetType(dto.Type);
            emp.TrySetSpecialties(dto.Specialty);
            emp.TrySetExperience(dto.Experience);
            emp.TrySetGender(dto.Gender);
            emp.TrySetBasePriceMultiplier(dto.BasePriceMultiplier);

            emp.TrySetEmail(dto.Email);
            emp.TrySetPhoneNumber(dto.PhoneNumber);

            // Update address
            if (emp.Address == null)
            {
                emp.TrySetAddress(dto.City, dto.PostalCode, dto.StreetName, dto.HouseNumber);
            }
            else
            {
                emp.Address.UpdateCity(dto.City);
                emp.Address.UpdatePostalCode(dto.PostalCode);
                emp.Address.UpdateStreetName(dto.StreetName);
                emp.Address.UpdateHouseNumber(dto.HouseNumber);
            }

            await _employeeRepo.UpdateAsync(emp);
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


    }


}

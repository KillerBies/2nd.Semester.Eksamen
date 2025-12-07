using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd
{
    public class ReadEmployeeDetailsCommand
    {
        private readonly IEmployeeRepository _repo;

        public ReadEmployeeDetailsCommand(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<EmployeeDetailsDTO?> ExecuteAsync(int id)
        {
            var employee = await _repo.GetByIDAsync(id);

            return new EmployeeDetailsDTO
            {
                Id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.LastName,
                Type = employee.Type,
                Specialty = employee.Specialties,
                Experience = employee.ExperienceLevel,
                Gender = employee.Gender,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                City = employee.Address.City,
                PostalCode = employee.Address.PostalCode,
                StreetName = employee.Address.StreetName,
                HouseNumber = employee.Address.HouseNumber,
                BasePriceMultiplier = employee.BasePriceMultiplier
            };
        }
    }
}

using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;

namespace _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd
{
    public class CreateEmployeeCommand
    {
        // Inject your repository/DbContext if needed (via constructor)
        private readonly IEmployeeRepository _repo;

        public CreateEmployeeCommand(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(EmployeeInputDTO dto)
        {
            // Convert specialties list -> string
            var specialtyString = string.Join(", ", dto.Specialties.Select(s => s.Value));

            // Map DTO -> Domain Address
            var domainAddress = new Address(
                dto.Address.City,
                dto.Address.PostalCode,
                dto.Address.StreetName,
                dto.Address.HouseNumber
            );
            // Map DTO -> Domain Entity
            var employee = new Employee(
                firstname: dto.FirstName,
                lastname: dto.LastName,
                type: dto.Type.GetDescription(),
                specialties: string.Join(", ", dto.Specialties.Select(s => s.Value)),
                address: domainAddress,
                experience: dto.ExperienceLevel.GetDescription(),
                gender: dto.Gender.GetDescription(),
                email: dto.Email,
                phoneNumber: dto.PhoneNumber,
                basePriceMultiplier: dto.BasePriceMultiplier,
                workEnd: new(08,0,0),
                workStart: new(18,0,0)
            );

            // TODO: Add way to add appointments


            await _repo.CreateNewAsync(employee);
        }

    }
}


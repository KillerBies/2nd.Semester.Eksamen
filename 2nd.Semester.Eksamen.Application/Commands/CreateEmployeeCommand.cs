using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public class CreateEmployeeCommand
    {
        // Inject your repository/DbContext if needed (via constructor)
        private readonly IEmployeeRepository _repo;

        public CreateEmployeeCommand(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(EmployeeInputModel dto)
        {
            // Map DTO -> Domain Address
            var domainAddress = new Domain.Entities.Persons.Address(
                city: dto.Address.City,
                postalCode: dto.Address.PostalCode,
                streetName: dto.Address.StreetName,
                houseNumber: dto.Address.HouseNumber
            );
            // Map DTO -> Domain Entity
            var employee = new Domain.Entities.Persons.Employee(
                firstname: dto.FirstName,
                lastname: dto.LastName,
                type: dto.Type.GetDescription(),
                specialty: dto.Specialty,
                experience: dto.ExperienceLevel.GetDescription(),
                gender: dto.Gender.GetDescription(),
                email: dto.Email,
                phoneNumber: dto.PhoneNumber,
                address: domainAddress,
                basePriceMultiplier: dto.BasePriceMultiplier
                
                
                
            );

            // These collections depend on how Employee handles them:
            // If Employee exposes AddAppointment, AddTreatment, etc:
            //foreach (var appt in dto.Appointments)
            //    employee.AddAppointment(appt);

            //foreach (var treatment in dto.TreatmentHistory)
            //    employee.AddTreatment(treatment);

            await _repo.CreateNewAsync(employee);
        }

    }
}


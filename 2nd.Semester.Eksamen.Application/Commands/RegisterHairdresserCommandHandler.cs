using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public class RegisterHairdresserCommandHandler
    {
        private readonly IEmployeeRepository _repo;

        public RegisterHairdresserCommandHandler(IEmployeeRepository repository)
        {
            _repo = repository;
        }

        public async Task<int> Handle(RegisterHairdresserCommand cmd)
        {
            var employee = new Employee(
                name: cmd.Name,
                address: cmd.Address,
                phoneNumber: cmd.PhoneNumber,
                email: cmd.Email,
                type: cmd.Type,
                specialty: cmd.Specialty,
                experienceLevel: cmd.ExperienceLevel,
                gender: cmd.Gender,
                age: cmd.Age,
                basePriceMultiplier: cmd.BasePriceMultiplier
        );

            await _repo.AddAsync(employee);

            return employee.Id;
        }
    }
}

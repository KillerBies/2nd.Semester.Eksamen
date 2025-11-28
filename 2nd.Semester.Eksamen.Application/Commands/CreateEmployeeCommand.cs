using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Helpers;

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

            var employee = new Employee(
                firstname: dto.FirstName,
                lastname: dto.LastName,
                type: dto.Type.GetDescription(),
                specialty: string.Join(", ", dto.Specialties.Select(s => s.Value)),
                address: domainAddress,
                experience: dto.ExperienceLevel.GetDescription(),
                gender: dto.Gender.GetDescription(),
                email: dto.Email,
                phoneNumber: dto.PhoneNumber,
                basePriceMultiplier: dto.BasePriceMultiplier
            );

            // TODO: Add way to add appointments


            await _repo.CreateNewAsync(employee, domainAddress);
        }

    }
}


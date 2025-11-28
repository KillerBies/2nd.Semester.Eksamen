using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Helpers;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public class UpdateEmployeeCommand
    {
        private readonly IEmployeeRepository _repo;

        public UpdateEmployeeCommand(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(int employeeId, EmployeeUpdateDTO dto)
        {
            var employee = await _repo.GetByIDAsync(employeeId);

            if (employee == null) return;

            employee.TrySetName(dto.FirstName);
            employee.TrySetLastName(dto.FirstName, dto.LastName);
            employee.TrySetEmail(dto.Email);
            employee.TrySetPhoneNumber(dto.PhoneNumber);
            employee.TrySetSpecialty(string.Join(", ", dto.Specialties.Select(s => s.Value)));
            employee.TrySetGender(dto.Gender.GetDescription()); // Enum to string
            employee.TrySetBasePriceMultiplier(dto.BasePriceMultiplier);
            employee.TrySetExperience(dto.ExperienceLevel.GetDescription());
            employee.TrySetType(dto.Type.GetDescription());
            employee.TrySetAddress(dto.Address.City, dto.Address.PostalCode, dto.Address.StreetName, dto.Address.HouseNumber);

            await _repo.UpdateAsync(employee);
        }
    }
}


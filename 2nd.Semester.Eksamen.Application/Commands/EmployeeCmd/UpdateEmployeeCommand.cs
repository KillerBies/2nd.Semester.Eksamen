using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.Adapters;

namespace _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd
{
    public class UpdateEmployeeCommand
    {
        private readonly IEmployeeService _service;
        private readonly DTO_to_Domain _adapter;

        public UpdateEmployeeCommand(IEmployeeService service, DTO_to_Domain adapter)
        {
            _service = service;
            _adapter = adapter;
        }

        public async Task ExecuteAsync(int employeeId, EmployeeUpdateDTO dto)
        {
            // optional: validation
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ArgumentException("First name is required.");

            // Map DTO → Domain Employee entity
            var employee = await _adapter.DTOEmployeeUpdateToDomain(employeeId, dto);

            // Delegate persistence to the service
            await _service.UpdateEmployeeAsync(employee);
        }
    }
}

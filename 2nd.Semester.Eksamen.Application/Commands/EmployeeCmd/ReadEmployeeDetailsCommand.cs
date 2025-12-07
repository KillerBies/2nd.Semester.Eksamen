using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd
{
    public class ReadEmployeeDetailsCommand
    {
        private readonly IEmployeeService _employeeService;

        public ReadEmployeeDetailsCommand(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<EmployeeDetailsDTO?> ExecuteAsync(int id)
        {
            return await _employeeService.GetEmployeeDetailsAsync(id);
        }


    }
}

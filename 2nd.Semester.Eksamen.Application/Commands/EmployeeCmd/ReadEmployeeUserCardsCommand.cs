using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd
{
    public class ReadEmployeeUserCardsCommand
    {
        private readonly IEmployeeService _employeeService;

        public ReadEmployeeUserCardsCommand(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<EmployeeUserCardDTO>> ExecuteAsync()
        {
            return await _employeeService.GetAllEmployeeUserCardsAsync();
        }
    }
}

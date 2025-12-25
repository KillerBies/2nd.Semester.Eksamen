using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IEmployeeUpdateService
    {
        public Task<EmployeeInputDTO> GetEmployeeInputDTOByIdAsync(int id);
        public Task UpdateEmployee(EmployeeInputDTO employee);
    }
}

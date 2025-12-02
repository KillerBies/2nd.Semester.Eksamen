using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IEmployeeService
    {
        Task DeleteEmployeeAsync(int id);
        Task<EmployeeDetailsDTO> GetByIdAsync(int id);
        Task UpdateEmployeeAsync(EmployeeDetailsDTO dto);

    }
}

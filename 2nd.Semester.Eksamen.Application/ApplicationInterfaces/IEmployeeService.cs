using _2nd.Semester.Eksamen.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDetailsDTO> GetByIdAsync(int id);
        Task UpdateEmployeeAsync(EmployeeDetailsDTO dto);

    }
}

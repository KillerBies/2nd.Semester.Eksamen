using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO
{
    public class EmployeeVicationDTO
    {  
        public int EmployeeId { get; set; }
        public DateOnly Start { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly End { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public EmployeeVicationDTO() { }
    }
}

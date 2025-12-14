using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; } = 0;
        public string Name { get; set; } = null!;
        public string ExperienceLevel { get; set; } = "";
        public decimal BasePriceMultiplier { get; set; } = 1;
        public string Specialties { get; set; }
    }
}

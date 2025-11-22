using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; } = new();
        public string Name { get; set; } = null!;
        public ExperienceLevels ExperienceLevel { get; set; } = new();
        public decimal BasePriceMultiplier { get; set; } = new();
        public string Specialization { get; set; } = null!;
    }
}

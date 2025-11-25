using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class EmployeeUserCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Color { get; set; } = "#d3d3d3";

    }
}

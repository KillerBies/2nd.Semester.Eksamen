using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO
{
    public class EmployeeUserCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PhoneNumber { get; set; }
        public string Color { get; set; } = "#d3d3d3";
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string City { get; set; }
        public decimal BasePriceMultiplier { get; set; }
        public ExperienceLevels ExperienceLevel { get; set; }

    }
}

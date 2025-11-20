using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class EmployeeInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public List<Appointment> Appointments { get; set; } = new();

        //takes a DTO model as input value to store multiple variables in 1
        public AddressInputModel Address { get; set; } = new AddressInputModel();
        public string PhoneNumber { get; set; }
        public decimal BasePriceMultiplier { get; set; }
        public ExperienceLevels ExperienceLevel { get; set; }
        public EmployeeType Type { get; set; }
        public string Specialty { get; set; }
        public List<Treatment> TreatmentHistory { get; set; } = new();
    }

}

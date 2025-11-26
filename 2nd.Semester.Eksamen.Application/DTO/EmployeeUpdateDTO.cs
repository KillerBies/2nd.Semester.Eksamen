using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class EmployeeUpdateDTO : IHasSpecialties
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<SpecialtyItemBase> Specialties { get; set; } = new();
        public Gender Gender { get; set; }
        public decimal BasePriceMultiplier { get; set; }
        public ExperienceLevels ExperienceLevel { get; set; }
        public EmployeeType Type { get; set; }

        public AddressUpdateDTO Address { get; set; } = new AddressUpdateDTO();
    }
    public class AddressUpdateDTO
    {
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

}

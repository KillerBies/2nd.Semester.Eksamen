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
    public class EmployeeInputDTO : IHasSpecialties
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Udfyld venligst fornavn eller firmanavn")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Udfyld venligst fornavn eller firmanavn")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Indtast venligst email")]
        [EmailAddress(ErrorMessage = "Indtast venligst en gyldig email")]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public List<Appointment> Appointments { get; set; } = new();

        //takes a DTO model as input value to store multiple variables in 1
        public AddressInputDTO Address { get; set; } = new AddressInputDTO();
        [Required(ErrorMessage = "Indtast venligst telefonnummer")]
        [Phone(ErrorMessage = "Indtast venligst et gyldigt telefonnummer")]
        public string PhoneNumber { get; set; }
        public decimal BasePriceMultiplier { get; set; }
        public ExperienceLevels ExperienceLevel { get; set; }
        public EmployeeType Type { get; set; }
        public List<SpecialtyItemBase> Specialties { get; set; } = new();
        public List<Treatment> TreatmentHistory { get; set; } = new();
        public List<string> SpecialtiesList { get; set; } = new();
        public TimeSpan WorkStart { get; set; } = new TimeSpan(0, 0, 0);
        public TimeSpan WorkEnd { get; set; } = new TimeSpan(0, 0, 0);

        public EmployeeInputDTO(Employee emp)
        {
            id = emp.Id;
            FirstName = emp.Name;
            LastName = emp.LastName;
            Email = emp.Email;
            Gender = Enum.Parse<Gender>(emp.Gender);
            PhoneNumber = emp.PhoneNumber;
            BasePriceMultiplier = emp.BasePriceMultiplier;
            ExperienceLevel = Enum.Parse<ExperienceLevels>(emp.ExperienceLevel);
            Type = Enum.Parse<EmployeeType>(emp.Type);
            SpecialtiesList = emp.Specialties.Split(',').ToList();
            Address = new AddressInputDTO()
            {
                City = emp.Address.City,
                HouseNumber = emp.Address.HouseNumber,
                PostalCode = emp.Address.PostalCode,
                StreetName = emp.Address.StreetName
            };
            WorkStart = emp.WorkStart.ToTimeSpan();
            WorkEnd = emp.WorkEnd.ToTimeSpan();
        }
        public EmployeeInputDTO()
        {
        }
    }

}

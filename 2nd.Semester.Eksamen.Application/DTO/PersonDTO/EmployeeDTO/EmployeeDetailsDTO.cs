using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO
{
    public class EmployeeDetailsDTO
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public TimeOnly WorkStart { get; set; }
        public TimeOnly WorkEnd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Specialty { get; set; }
        public string Experience { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BasePriceMultiplier { get; set; }

        // Address
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }

        public EmployeeDetailsDTO(Employee employee)
        {
            Id = employee.Id;
            Guid = employee.Guid;
            FirstName = employee.Name;
            LastName = employee.LastName;
            Type = employee.Type;
            Specialty = employee.Specialties;
            Experience = employee.ExperienceLevel;
            Gender = employee.Gender;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            BasePriceMultiplier = employee.BasePriceMultiplier;

            City = employee.Address?.City;
            PostalCode = employee.Address?.PostalCode;
            StreetName = employee.Address?.StreetName;
            HouseNumber = employee.Address?.HouseNumber;
            WorkStart = employee.WorkStart;
            WorkEnd = employee.WorkEnd;
        }
        public EmployeeDetailsDTO(TreatmentSnapshot ts)
        {
            Guid = ts.EmployeeGuid;
            FirstName = ts.EmployeeName;
        }
        public EmployeeDetailsDTO()
        {
        }
    }
}

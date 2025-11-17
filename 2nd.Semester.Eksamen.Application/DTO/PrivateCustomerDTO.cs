using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class PrivateCustomerDTO
    {
        public string Name { get; private set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public Gender Gender { get; private set; }
        public DateOnly Birthday { get; private set; }

        public PrivateCustomerDTO(string name, string city, string postalCode, string streetName, int houseNumber, string phoneNumber, string email, Gender gender, DateOnly birthday)
        {
            Name = name;
            City = city;
            PostalCode = postalCode;
            StreetName = streetName;
            HouseNumber = houseNumber;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
            Birthday = birthday;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class Address : BaseEntity
    {
        //Adress of an entity
        public string? City { get; private set; } = null!;
        public string? PostalCode { get; private set; } = null!;
        public string? StreetName { get; private set; } = null!;
        public string? HouseNumber { get; set; } = null!;

        public Address() { }
        public Address(string city, string postalCode, string streetName, string houseNumber)
        {
            City = city;
            PostalCode = postalCode;
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        //methods to set adress elements
        public bool TrySetCity(string city)
        {
            if (!string.IsNullOrWhiteSpace(city))
            {
                City = city;
                return true;
            }
            return false;
        }

        public bool TrySetPostalCode(string postalCode)
        {
            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                PostalCode = postalCode;
                return true;
            }
            return false;
        }

        public bool TrySetStreetName(string streetName)
        {
            if (!string.IsNullOrWhiteSpace(streetName))
            {
                StreetName = streetName;
                return true;
            }
            return false;
        }
    }
}

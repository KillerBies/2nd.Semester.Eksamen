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
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string city, string postalCode, string streetName, int houseNumber)
        {
            City = city;
            PostalCode = postalCode;
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
    }
}

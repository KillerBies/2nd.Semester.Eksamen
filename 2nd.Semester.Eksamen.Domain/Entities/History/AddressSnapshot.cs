using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record AddressSnapshot : BaseSnapshot
    {
        public CustomerSnapshot CustomerSnapshot { get; set; }
        public string City { get; protected set; } = null!;
        public string PostalCode { get; protected set; } = null!;
        public string StreetName { get; protected set; } = null!;
        public string HouseNumber { get; protected set; } = null!;



        private AddressSnapshot() { }
        public AddressSnapshot(Address address) : base(address.RefrenceId)
        {
            City = address.City;
            PostalCode = address.PostalCode;
            StreetName = address.StreetName;
            HouseNumber = address.HouseNumber;
        }

    }
}
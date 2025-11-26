using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class EmployeeDetailsDTO
    {
        public int Id {  get; set; }
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

    }

}

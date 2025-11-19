using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class PrivateCustomerDTO
    {
        [Required]
        public string Name { get;  set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get;  set; }
        [Required]
        [EmailAddress]
        public string Email { get;  set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateOnly Birthday { get; set; }

        public PrivateCustomerDTO() { }
    }
}

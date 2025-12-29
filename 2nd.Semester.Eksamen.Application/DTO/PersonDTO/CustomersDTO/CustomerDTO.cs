using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO
{
    public class CustomerDTO
    {
        
        public string Type { get; set; }
        public int NumberOfVisits { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "Udfyld venligst fornavn eller firmanavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string Name { get; set; } = "";
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst bynavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string City { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst postnummer")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Postnummer skal være 4 cifre")]
        public string PostalCode { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst vejnavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string StreetName { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst husnr")]
        public string HouseNumber { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst telefonnummer")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Indtast venligst et gyldigt telefonnummer med 8 cifre")]
        public string PhoneNumber { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst email")]
        [EmailAddress(ErrorMessage = "Indtast venligst en gyldig email")]
        public string Email { get; set; }
        public bool SaveAsCustomer { get; set; } = false;
        public CustomerDTO(Customer customer) 
        {
            NumberOfVisits = customer.NumberOfVisists;
            id = customer.Id;
            City = customer.Address.City;
            StreetName = customer.Address.StreetName;
            PostalCode = customer.Address.PostalCode;
            HouseNumber = customer.Address.HouseNumber;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            Name = customer.Name;
            if (customer is PrivateCustomer pc)
            {
                Type = "Private Customer";
            }
            if (customer is CompanyCustomer cc)
            {
                Type = "Company Customer";
            }
        }
        public CustomerDTO() { }
    }
}

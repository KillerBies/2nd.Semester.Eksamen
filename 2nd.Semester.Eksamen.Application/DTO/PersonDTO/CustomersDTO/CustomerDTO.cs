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
        [Phone(ErrorMessage = "Indtast venligst et gyldigt telefonnummer")]
        public string PhoneNumber { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst email")]
        [EmailAddress(ErrorMessage = "Indtast venligst en gyldig email")]
        public string Email { get; set; }
        public CustomerDTO() { }
    }
}

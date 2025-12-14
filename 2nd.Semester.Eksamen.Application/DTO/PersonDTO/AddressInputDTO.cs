using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO
{
    public class AddressInputDTO
    {
        [Required(ErrorMessage = "Indtast venligst bynavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string City { get; set; }
        [Required(ErrorMessage = "Indtast venligst postnummer")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Postnummer skal være 4 cifre")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Indtast venligst vejnavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Indtast venligst husnr")]
        public string HouseNumber { get; set; }

    }

}

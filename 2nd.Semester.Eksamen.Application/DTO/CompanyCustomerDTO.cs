using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class CompanyCustomerDTO : CustomerDTO
    {
        [Required(ErrorMessage = "Indtast venligst dit CVR-nummer")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CVR-nummer skal være 8 cifre")]
        public string CVRNumber {  get; set; }
        
        public CompanyCustomerDTO() { }
    }
}

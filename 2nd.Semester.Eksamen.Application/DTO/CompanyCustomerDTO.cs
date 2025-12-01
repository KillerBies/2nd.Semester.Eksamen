using System.ComponentModel.DataAnnotations;
namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class CompanyCustomerDTO : CustomerDTO
    {
        [Required(ErrorMessage = "Indtast venligst dit CVR-nummer")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CVR-nummer skal være 8 cifre")]
        public string CVRNumber {  get; set; }
        
        public string CVRNumber { get; set; }
        [MaxLength(200)]
        public string? Notes { get; set; }

        public bool SaveAsCustomer { get; set; }




        public CompanyCustomerDTO() { }
    }
}

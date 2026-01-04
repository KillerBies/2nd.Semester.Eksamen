using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System.ComponentModel.DataAnnotations;
namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO
{
    public class CompanyCustomerDTO : CustomerDTO
    {
        [Required(ErrorMessage = "Indtast venligst dit CVR-nummer")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CVR-nummer skal være 8 cifre")]
        public string CVRNumber {  get; set; }
        [MaxLength(200)]
        public string? Notes { get; set; }

        public CompanyCustomerDTO(CompanyCustomer customer) : base(customer)
        {
            CVRNumber = customer.CVRNumber;
            Notes = customer.Notes;
        }
        public CompanyCustomerDTO(CompanyCustomerSnapshot customer) : base(customer)
        {
            CVRNumber = customer.CVR;
        }
        public CompanyCustomerDTO() { }
    }
}

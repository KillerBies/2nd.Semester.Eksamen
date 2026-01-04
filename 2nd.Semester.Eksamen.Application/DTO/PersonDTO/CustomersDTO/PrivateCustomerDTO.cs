using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System.ComponentModel.DataAnnotations;

namespace _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO
{
    public class PrivateCustomerDTO : CustomerDTO
    {
        [Required(ErrorMessage = "Udfyld venligst efternavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string LastName { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Vælg venligst et køn")]
        public Gender Gender { get; set; }

        public DateOnly Birthday { get; set; }

        [Required(ErrorMessage = "")]
        public DateTime BirthdayWrapper
        {
            get => Birthday.ToDateTime(TimeOnly.MinValue);
            set => Birthday = DateOnly.FromDateTime(value);
        }
        [MaxLength(200)]
        public string? Notes { get; set; }
        public PrivateCustomerDTO(PrivateCustomer customer) : base(customer)
        {
            LastName = customer.LastName;
            Gender = (Gender)customer.Gender;
            Birthday = customer.BirthDate;
            Notes = customer.Notes;
        }
        public PrivateCustomerDTO(PrivateCustomerSnapshot customerSnapshot) : base(customerSnapshot)
        {
            LastName = customerSnapshot.LastName;
        }
        public PrivateCustomerDTO()
        {

        }
    }
}
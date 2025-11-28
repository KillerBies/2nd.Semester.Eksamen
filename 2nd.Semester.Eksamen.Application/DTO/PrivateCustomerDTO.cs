using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System.ComponentModel.DataAnnotations;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class PrivateCustomerDTO
    {

        [Required(ErrorMessage = "Udfyld venligst navn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string FirstName { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Udfyld venligst efternavn")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Kun bogstaver er tilladt")]
        public string LastName { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Udfyld venligst by")]
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
        [Required(ErrorMessage = "Indtast venligst husnummer")]
        public string HouseNumber { get; set; }
        //----------------------------------------------------------------------------
        [Phone(ErrorMessage = "Indtast venligst et gyldigt telefonnummer")]
        [Required(ErrorMessage = "Udfyld venligst telefonnummer")]
        public string PhoneNumber { get; set; }
        //----------------------------------------------------------------------------
        [Required(ErrorMessage = "Indtast venligst email")]
        [EmailAddress(ErrorMessage = "Indtast venligst en gyldig email")]
        public string Email { get; set; }
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
        public bool SaveAsCustomer { get; set; }
        public PrivateCustomerDTO()
        {

        }
    }
}
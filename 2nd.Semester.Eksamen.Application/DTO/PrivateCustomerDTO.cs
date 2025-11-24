using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Application.DTO
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

        public PrivateCustomerDTO()
        {

        }
    }
}
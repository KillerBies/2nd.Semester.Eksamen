using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class TreatmentBookingDTO
    {
        [Required]
        public TreatmentDTO Treatment { get; set; } = new();
        [Required]
        public EmployeeDTO Employee { get; set; } = new();
        public DateTime Start { get; set; } = new();
        public DateTime End { get; set; } = new();
        [Required]
        public decimal Price { get; set; } = new();
        public void UpdatePrice()
        {
            if (Treatment.TreatmentId != 0 && Employee.EmployeeId != 0)
            {
                Price = Employee.BasePriceMultiplier * Treatment.BasePrice;
            }
        }

    }
}

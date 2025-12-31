using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
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
                Price = Math.Round(Employee.BasePriceMultiplier * Treatment.BasePrice);
            }
        }
        public TreatmentBookingDTO(TreatmentBooking tb)
        {
            Treatment = new TreatmentDTO(tb.Treatment);
            Employee = new EmployeeDTO(tb.Employee);
            Start = tb.Start;
            End = tb.End;
            Price = tb.Price;
        }
        public TreatmentBookingDTO(TreatmentBookingDTO tb)
        {
            Treatment = new TreatmentDTO(tb.Treatment);
            Employee = new EmployeeDTO(tb.Employee);
            Start = tb.Start;
            End = tb.End;
            Price = tb.Price;
        }
        public TreatmentBookingDTO()
        {

        }

    }
}

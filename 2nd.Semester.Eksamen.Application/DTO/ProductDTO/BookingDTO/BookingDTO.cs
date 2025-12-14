using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class BookingDTO
    {
        public int CustomerId { get; set; }
        [Required]
        public CustomerDTO Customer { get; set; }
        [Required]
        public DateTime Start { get; set; } = DateTime.Now;
        [Required]
        public DateTime End { get; set; } = DateTime.Now;
        [Required]
        public List<TreatmentBookingDTO> TreatmentBookingDTOs { get; set; } = new List<TreatmentBookingDTO>(){new()};
        [Required]
        public bool CustomerNotification { get; set; } = false;
        [Required]
        public WaitListDTO WaitList { get; set; } = new();
        [Required]
        public TimeSpan Duration { get; set; } = new();
        public decimal Price => TreatmentBookingDTOs.Select(tb => tb.Price).Sum();
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class BookingDTO
    {
        [Required]
        public int CustomerId { get; set; } = new();
        [Required]
        public DateTime Start { get; set; } = new();
        [Required]
        public DateTime End { get; set; } = new();
        [Required]
        public List<TreatmentBookingDTO> TreatmentBookingDTOs { get; set; } = new List<TreatmentBookingDTO>(){new()};
        [Required]
        public bool CustomerNotification { get; set; } = false;
        [Required]
        public WaitListDTO WaitList { get; set; } = new();
        [Required]
        public TimeSpan Duration { get; set; } = new();
    }
}

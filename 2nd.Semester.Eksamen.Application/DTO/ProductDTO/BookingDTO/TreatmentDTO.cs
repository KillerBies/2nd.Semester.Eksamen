using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class TreatmentDTO
    {

        [Required]
        public string Category { get; set; } = string.Empty;

        public int TreatmentId { get; set; } = 0;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public List<string> RequiredSpecialties { get; set; } = new();
        [Required]
        public TimeSpan Duration { get; set; } = new();
        [Required]
        public decimal BasePrice { get; set; } = new();
        [Required]
        public string Description {  get; set; } = string.Empty;
    }
}

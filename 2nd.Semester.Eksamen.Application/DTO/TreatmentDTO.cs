using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class TreatmentDTO
    {
        [Required]
        public string Category { get; set; } 
        [Required]
        public int TreatmentId { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public List<string> RequiredSpecialties { get; set; } = new();
        [Required]
        public TimeSpan Duration { get; set; } = new();
        [Required]
        public decimal BasePrice { get; set; } = new();
    }
}

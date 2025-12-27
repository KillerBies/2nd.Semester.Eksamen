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

        [Required(ErrorMessage = "Kategori er påkrævet")]
        [StringLength(50, ErrorMessage = "Kategori må maks være 50 tegn")]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ\s]+$", ErrorMessage = "Kategori må kun indeholde bogstaver og mellemrum")]
        public string Category { get; set; } = string.Empty;

        public int TreatmentId { get; set; } = 0;


        [Required(ErrorMessage = "Produktnavn er påkrævet")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Produktnavn skal være mellem 2 og 100 tegn")]
        [RegularExpression(@"^[a-zA-Z0-9æøåÆØÅ\s]+$", ErrorMessage = "Produktnavn må kun indeholde bogstaver, tal og mellemrum")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public List<string> RequiredSpecialties { get; set; } = new();
        [Required]
        public TimeSpan Duration { get; set; } = new();

        [Required(ErrorMessage = "Pris er påkrævet")]
        [Range(0.01, 1000000, ErrorMessage = "Pris skal være større end 0")]
        public decimal BasePrice { get; set; } = new();
        [Required]
        public string Description {  get; set; } = string.Empty;
    }
}

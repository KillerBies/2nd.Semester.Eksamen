using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class NewProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Produktnavn er påkrævet")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Produktnavn skal være mellem 2 og 100 tegn")]
        [RegularExpression(@"^[a-zA-Z0-9æøåÆØÅ\s]+$", ErrorMessage = "Produktnavn må kun indeholde bogstaver, tal og mellemrum")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Pris er påkrævet")]
        [Range(0.01, 1000000, ErrorMessage = "Pris skal være større end 0")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Beskrivelse må maks være 500 tegn")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori er påkrævet")]
        [StringLength(50, ErrorMessage = "Kategori må maks være 50 tegn")]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ\s]+$", ErrorMessage = "Kategori må kun indeholde bogstaver og mellemrum")]
        public string Category { get; set; } = string.Empty;
    }
}

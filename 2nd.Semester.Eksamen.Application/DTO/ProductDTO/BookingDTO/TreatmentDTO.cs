using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
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
        public Guid TreatmentGuid { get; set; }

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

        public TreatmentDTO(Treatment tre)
        {
            Category = tre.Category;
            Name = tre.Name;
            TreatmentId = tre.Id;
            RequiredSpecialties = tre.RequiredSpecialties;
            Duration = tre.Duration;
            BasePrice = tre.Price;
            Description = tre.Description;
            TreatmentGuid = tre.Guid;
        }
        public TreatmentDTO(TreatmentDTO tre)
        {
            Category = tre.Category;
            Name = tre.Name;
            TreatmentId = tre.TreatmentId;
            RequiredSpecialties = tre.RequiredSpecialties;
            Duration = tre.Duration;
            BasePrice = tre.BasePrice;
            Description = tre.Description;
        }
        public TreatmentDTO(TreatmentSnapshot tre)
        {
            Category = tre.Category;
            Name = tre.Name;
            TreatmentId = tre.Id;
            Duration = tre.Duration;
            BasePrice = tre.PricePerUnit;
            TreatmentGuid = tre.Guid;
        }
        public TreatmentDTO() { }
    }
}

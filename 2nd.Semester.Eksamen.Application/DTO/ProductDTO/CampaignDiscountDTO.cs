using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CampaignDiscountDTO
    {
        [Required(ErrorMessage = "Navn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navn må maks være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 1, ErrorMessage = "Rabat på behandling skal være mellem 0 og 100%.")]
        public decimal TreatmentDiscount { get; set; }

        [Range(0, 1, ErrorMessage = "Rabat på produkter skal være mellem 0 og 100%.")]
        public decimal ProductDiscount { get; set; }

        public bool AppliesToTreatment { get; set; }
        public bool AppliesToProduct { get; set; }

        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateTime Start { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        public DateTime End { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "Beskrivelse må maks være 500 tegn.")]
        public string Description { get; set; } = string.Empty;

        // Instead of exposing Product entities in the DTO, use IDs
        [Required(ErrorMessage = "Der skal vælges mindst ét produkt.")]
        public List<int> ProductIds { get; set; } = new();
    }
}

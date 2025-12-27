using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class CampaignDiscountDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Navn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navn må maks være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 1, ErrorMessage = "Rabat på behandling skal være mellem 0 og 1.")]
        public decimal TreatmentDiscount { get; set; }

        [Range(0, 1, ErrorMessage = "Rabat på produkter skal være mellem 0 og 1.")]
        public decimal ProductDiscount { get; set; }

        public bool AppliesToTreatment { get; set; }
        public bool AppliesToProduct { get; set; }

        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateTime Start { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        public DateTime End { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "Beskrivelse må maks være 500 tegn.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Der skal vælges mindst ét produkt.")]
        public List<int> ProductIds { get; set; } = new();

        public CampaignDiscountDTO(Campaign camp)
        {
            Name = camp.Name;
            TreatmentDiscount = camp.TreatmentDiscount;
            ProductDiscount = camp.ProductDiscount;
            AppliesToProduct = camp.AppliesToProduct;
            AppliesToTreatment = camp.AppliesToTreatment;
            Start = camp.Start;
            End = camp.End;
            Description = camp.Description;
            ProductIds = camp.ProductsInCampaign.Select(p => p.Id).ToList();
            Id = camp.Id;
        }
        public CampaignDiscountDTO(DiscountOverviewDTO camp)
        {
            Name = camp.Name;
            TreatmentDiscount = camp.TreatmentDiscount;
            ProductDiscount = camp.ProductDiscount;
            AppliesToProduct = camp.IsActiveForProducts;
            AppliesToTreatment = camp.IsActiveForTreatments;
            Start = camp.Start;
            End = camp.End;
            Description = camp.Description;
            ProductIds = camp.AppliesToProducts.Select(p => p.ProductId).ToList();
            Id = camp.Id;
        }
        public CampaignDiscountDTO() { }
    }
}

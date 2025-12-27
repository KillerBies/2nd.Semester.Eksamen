using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class DiscountOverviewDTO
    {
        public int Id { get; set; } = 0;
        public string Type { get; set; } = "";
        public string LoyaltyDiscountType { get; set; } = "";
        public string Name { get; set; } = string.Empty;
        public decimal TreatmentDiscount { get; set; }
        public decimal ProductDiscount { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
        public List<ProductDTO> AppliesToProducts { get; set; }
        public string Description { get; set; } = string.Empty;
        public int NumberOfUses { get; set; } = 0;
        public int MinimumVisits { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsActiveForProducts { get; set; } = false;
        public bool IsActiveForTreatments { get; set; } = false;

        public DiscountOverviewDTO(Discount discount)
        {
            Id = discount.Id;
            Name = discount.Name;
            TreatmentDiscount = discount.TreatmentDiscount;
            ProductDiscount = discount.ProductDiscount;
            NumberOfUses = discount.NumberOfUses;
            IsActiveForProducts = discount.AppliesToProduct;
            IsActiveForTreatments = discount.AppliesToTreatment;
            if (discount.IsLoyalty)
            {
                var Discount = (LoyaltyDiscount)discount;
                Type = "Loyalitets Rabat";
                LoyaltyDiscountType = Discount.DiscountType;
                MinimumVisits = Discount.MinimumVisits;
            }
            else
            {
                var Discount = (Campaign)discount;
                Description = Discount.Description;
                Type = "Kampagne Rabat";
                Start = Discount.Start;
                End = Discount.End;
                AppliesToProducts = Discount.ProductsInCampaign == null ? new() : Discount.ProductsInCampaign.Select(p => new ProductDTO() { Name = p.Name, Price = p.Price, ProductId = p.Id }).ToList();
                IsActive = DateTime.Now >= Start && DateTime.Now <= End ?  true : false;
            }
        }
        public DiscountOverviewDTO()
        { }
    }
}

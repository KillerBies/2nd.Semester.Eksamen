using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class DiscountOverviewDTO
    {
        public Guid Guid { get; set; }
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
            TreatmentDiscount = discount.TreatmentDiscount*100;
            ProductDiscount = discount.ProductDiscount*100;
            NumberOfUses = discount.NumberOfUses;
            Guid = discount.Guid;
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
                AppliesToProducts = Discount.ProductsInCampaign == null ? new() : Discount.ProductsInCampaign.Select(p => new ProductDTO() { Name = p.Name, Price = p.Price, ProductId = p.Id, Guid=p.Guid}).ToList();
                IsActive = DateTime.Now >= Start && DateTime.Now <= End ?  true : false;
            }
        }
        public DiscountOverviewDTO(AppliedDiscountSnapshot discount)
        {
            Name = discount.Name;
            TreatmentDiscount = (discount.TreatmentDiscount != null ? (decimal) discount.TreatmentDiscount : 0);
            ProductDiscount = (discount.ProductDiscount != null ? (decimal)discount.ProductDiscount : 0);
            Guid = discount.Guid;
        }
        public DiscountOverviewDTO()
        { }
    }
}

using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IDiscountApplicationService
    {
        public Task CreateNewLoyaltyDiscountAsync(LoyaltyDiscountDTO discount);
        public Task DeleteLoyaltyDiscountAsync(LoyaltyDiscountDTO discount);
        public Task UpdateLoyaltyDiscountAsync(LoyaltyDiscountDTO discount);
        public Task CreateNewCampaignDiscountAsync(CampaignDiscountDTO discount);
        public Task DeleteCampaignDiscountAsync(CampaignDiscountDTO discount);
        public Task UpdateCampaignDiscountAsync(CampaignDiscountDTO discount);
        public Task<List<ProductDTO>> GetAllProductsAsync();
    }
}

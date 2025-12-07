using _2nd.Semester.Eksamen.Application.DTO.DiscountDTO.CampaignDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces;

public interface IDiscountService
{
    //Task<IEnumerable<Campaign?>> GetCampaignAsync();
    Task<CampaignDTO> GetCampaignAsync(int id);
    Task CreateNewCampaignAsync(Campaign campaign);
    Task UpdateCampaignAsync(CampaignDTO dto);
    Task DeleteCampaignAsync(int id);
    Task<IEnumerable<LoyaltyDiscount?>> GetLoyaltyDiscountAsync();
    Task UpdateLoyaltyDiscountAsync(LoyaltyDiscount loyaltyDiscount);
    Task DeleteLoyaltyDiscountAsync(LoyaltyDiscount loyaltyDiscount);
}
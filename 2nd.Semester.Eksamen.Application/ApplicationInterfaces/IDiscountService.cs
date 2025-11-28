using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces;

public interface IDiscountService
{
    public Task<IEnumerable<Campaign?>> GetCampaignAsync();
    public Task CreateNewCampaignAsync(Campaign Campaign);
    public Task UpdateCampaignAsync(Campaign campaign);
    public Task DeleteCampaignAsync(Campaign campaign);
    public Task<IEnumerable<LoyaltyDiscount?>> GetLoyaltyDiscountAsync();
    public Task UpdateLoyaltyDiscountAsync(LoyaltyDiscount loyaltyDiscount);
    public Task DeleteLoyaltyDiscountAsync(LoyaltyDiscount loyaltyDiscount);
}
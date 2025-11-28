using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;

namespace _2nd.Semester.Eksamen.Application.Services;

public class DiscountService : IDiscountService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly ILoyaltyDiscountRepository _loyaltyDiscountRepository;
    private readonly ICompanyCustomerRepository _companyCustomerRepository;
    private readonly IPrivateCustomerRepository _privatePrivateCustomerRepository;
    public DiscountService(ICampaignRepository campaignRepository, ILoyaltyDiscountRepository loyaltyDiscountRepository, ICompanyCustomerRepository companyCustomerRepository, IPrivateCustomerRepository privatePrivateCustomerRepository)
    {
        _campaignRepository = campaignRepository;
        _loyaltyDiscountRepository = loyaltyDiscountRepository;
        _companyCustomerRepository = companyCustomerRepository;
        _privatePrivateCustomerRepository = privatePrivateCustomerRepository;
    }

    public async Task<IEnumerable<Campaign?>> GetCampaignAsync()
    {
        return await _campaignRepository.GetAllAsync();
    }

    public async Task CreateNewCampaignAsync(Campaign Campaign)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCampaignAsync(Campaign campaign)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCampaignAsync(Campaign campaign)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<LoyaltyDiscount?>> GetLoyaltyDiscountAsync()
    {
        return await _loyaltyDiscountRepository.GetAllAsync();
    }

    public async Task UpdateLoyaltyDiscountAsync(LoyaltyDiscount loyaltyDiscount)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteLoyaltyDiscountAsync(LoyaltyDiscount loyaltyDiscount)
    {
        throw new NotImplementedException();
    }

}
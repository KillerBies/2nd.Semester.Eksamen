using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;

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


}
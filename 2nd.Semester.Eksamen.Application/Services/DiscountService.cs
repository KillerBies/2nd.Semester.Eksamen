using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
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

    //public async Task<IEnumerable<Campaign?>> GetCampaignAsync()
    //{
    //    return await _campaignRepository.GetAllAsync();
    //}
    public async Task<CampaignDTO> GetCampaignAsync(int id)
    {
        var cmp = await _campaignRepository.GetByIDAsync(id);

        if (cmp == null) return null;

        return new CampaignDTO
        {
            AppliesToProduct = cmp.AppliesToProduct,
            AppliesToTreatment = cmp.AppliesToTreatment,
            Description = cmp.Description,
            DiscountAmount = cmp.DiscountAmount,
            End = cmp.End,
            Id = cmp.Id,
            Name = cmp.Name,
            NumberOfUses = cmp.NumberOfUses,
            ProductsInCampaign = cmp.ProductsInCampaign,
            Start = cmp.Start
        };
    }

    public async Task CreateNewCampaignAsync(Campaign campaign)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCampaignAsync(CampaignDTO dto)
    {
        var cmp = await _campaignRepository.GetByIDAsync(dto.Id);
        if (cmp == null) return;


    }

    public async Task DeleteCampaignAsync(int id)
    {
        var cmp = await _campaignRepository.GetByIDAsync(id);
        if (cmp == null)
            return;

        var name = cmp.Name;

        await _campaignRepository.DeleteAsync(cmp);

        if (cmp.Name != null)
        {
            await _campaignRepository.DeleteAsync(name);
        }
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
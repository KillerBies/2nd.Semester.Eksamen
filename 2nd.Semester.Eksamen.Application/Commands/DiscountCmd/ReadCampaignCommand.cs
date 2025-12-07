using _2nd.Semester.Eksamen.Application.DTO.DiscountDTO.CampaignDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;

namespace _2nd.Semester.Eksamen.Application.Commands.DiscountCmd;

public class ReadCampaignCommand
{
    private readonly ICampaignRepository _repo;

    public ReadCampaignCommand(ICampaignRepository repo)
    {
        _repo = repo;
    }

    public async Task<CampaignDTO?> ExecuteAsync(int id)
    {
        var cmp = await _repo.GetByIDAsync(id);

        return new CampaignDTO
        {
            Id = cmp.Id,
            Name = cmp.Name,
            AppliesToProduct = cmp.AppliesToProduct,
            AppliesToTreatment = cmp.AppliesToTreatment,
            Description = cmp.Description,
            DiscountAmount = cmp.DiscountAmount,
            End = cmp.End,
            NumberOfUses = cmp.NumberOfUses,
            ProductsInCampaign = cmp.ProductsInCampaign,
            Start = cmp.Start
        };
    }
}
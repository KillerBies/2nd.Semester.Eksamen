using _2nd.Semester.Eksamen.Application.DTO.DiscountDTO.CampaignDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;

namespace _2nd.Semester.Eksamen.Application.Commands.DiscountCmd;

public class CreateCampaignCommand
{
    private readonly ICampaignRepository _repo;

    public CreateCampaignCommand(ICampaignRepository repo)
    {
        _repo = repo;
    }

    public async Task ExecuteAsync(CampaignInputDTO dto)
    {
        var campaign = new Campaign(
            name: dto.Name,
            discountAmount: dto.DiscountAmount,
            start: dto.Start,
            end: dto.End
        );
    }
}
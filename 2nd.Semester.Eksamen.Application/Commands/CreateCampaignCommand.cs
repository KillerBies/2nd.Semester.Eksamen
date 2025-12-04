using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;

namespace _2nd.Semester.Eksamen.Application.Commands;

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
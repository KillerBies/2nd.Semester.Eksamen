using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using _2nd.Semester.Eksamen.Infrastructure.Data;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly AppDbContext _context;

    public CampaignRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Campaign?> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Campaign?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Campaign?>> GetByFilterAsync(Filter filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateNewAsync(Campaign Campaign)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Campaign Campaign)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Campaign Campaign)
    {
        throw new NotImplementedException();
    }
}
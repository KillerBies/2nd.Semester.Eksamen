using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    public CampaignRepository(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }
    public async Task<Campaign?> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Campaign?>> GetAllAsync()
    {
        await using var _context = await _factory.CreateDbContextAsync();
        return await _context.Campaigns.ToListAsync();
    }

    public async Task<IEnumerable<Campaign?>> GetByFilterAsync(Filter filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateNewAsync(Campaign campaign)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        await _context.Campaigns.AddAsync(campaign);
    }

    public async Task UpdateAsync(Campaign campaign)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        _context.Campaigns.Update(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Campaign campaign)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();
    }
}
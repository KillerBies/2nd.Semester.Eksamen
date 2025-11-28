using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories;

public class LoyaltyDiscountRepository : ILoyaltyDiscountRepository
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    public LoyaltyDiscountRepository(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }
    public async Task<LoyaltyDiscount?> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<LoyaltyDiscount?>> GetAllAsync()
    {
        await using var _context = await _factory.CreateDbContextAsync();
        return await _context.LoyaltyDiscounts.ToListAsync();
    }

    public async Task<IEnumerable<LoyaltyDiscount?>> GetByFilterAsync(Filter filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateNewAsync(LoyaltyDiscount loyaltyDiscount)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(LoyaltyDiscount loyaltyDiscount)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        _context.LoyaltyDiscounts.Update(loyaltyDiscount);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LoyaltyDiscount loyaltyDiscount)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        _context.LoyaltyDiscounts.Remove(loyaltyDiscount);
        await _context.SaveChangesAsync();
    }
}
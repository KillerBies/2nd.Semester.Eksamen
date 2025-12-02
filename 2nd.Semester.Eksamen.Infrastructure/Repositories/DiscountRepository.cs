using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products; // <-- check if Discount class is here
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;     // <-- if Discount class is actually here
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class DiscountRepository : IDiscountRepository
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public DiscountRepository(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<Discount>> GetAllAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Discounts.ToListAsync();
    }

    public async Task<IEnumerable<LoyaltyDiscount>> GetLoyaltyDiscountsAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.LoyaltyDiscounts
            .OrderBy(ld => ld.MinimumVisits)
            .ToListAsync();
    }



    public async Task<List<Product>> GetByIdsAsync(List<int> ids)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Products
                            .Where(p => ids.Contains(p.Id))
                            .ToListAsync();
    }
}

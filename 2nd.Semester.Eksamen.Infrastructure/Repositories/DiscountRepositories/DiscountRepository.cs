using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
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
        List<Campaign> campaigns = await context.Campaigns.Include(c=>c.ProductsInCampaign).ToListAsync();
        List<Discount> discounts = await context.Discounts.Where(d=>!(d is Campaign)).ToListAsync();
        discounts.AddRange(campaigns);
        return discounts;
    }

    public async Task<IEnumerable<LoyaltyDiscount>> GetLoyaltyDiscountsAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.LoyaltyDiscounts
            .OrderBy(ld => ld.MinimumVisits)
            .ToListAsync();
    }
    public async Task <Discount> GetByIdAsync(int id)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Discounts.FirstOrDefaultAsync(d => d.Id == id);

    }


    public async Task<List<Product>> GetByIdsAsync(List<int> ids)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Products
                            .Where(p => ids.Contains(p.Id))
                            .ToListAsync();
    }

    public async Task<Campaign?> GetCampaignByDiscountIdAsync(int discountId)
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Campaigns
                             .FirstOrDefaultAsync(c => c.Id == discountId);
    }
    public async Task DeleteByIdAsync(int id)
    {
        var _context = await _factory.CreateDbContextAsync();
        using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        try
        {
            var discount = await _context.Discounts.FindAsync(id);
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}


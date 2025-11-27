using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using _2nd.Semester.Eksamen.Infrastructure.Data;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories;

public class LoyaltyDiscountRepository : ILoyaltyDiscountRepository
{
    private readonly AppDbContext _context;

    public LoyaltyDiscountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoyaltyDiscount?> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<LoyaltyDiscount?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoyaltyDiscount?>> GetByFilterAsync(Filter filter)
    {
        throw new NotImplementedException();
    }

    public Task CreateNewAsync(LoyaltyDiscount LoyaltyDiscount)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(LoyaltyDiscount LoyaltyDiscount)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(LoyaltyDiscount LoyaltyDiscount)
    {
        throw new NotImplementedException();
    }
}
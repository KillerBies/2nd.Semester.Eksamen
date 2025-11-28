using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface ILoyaltyDiscountRepository
    {
        //Repository for Loyalty Discounts. 
        public Task<LoyaltyDiscount?> GetByIDAsync(int id);
        public Task<IEnumerable<LoyaltyDiscount?>> GetAllAsync();
        public Task<IEnumerable<LoyaltyDiscount?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(LoyaltyDiscount loyaltyDiscount);
        public Task UpdateAsync(LoyaltyDiscount loyaltyDiscount);
        public Task DeleteAsync(LoyaltyDiscount loyaltyDiscount);
    }
}
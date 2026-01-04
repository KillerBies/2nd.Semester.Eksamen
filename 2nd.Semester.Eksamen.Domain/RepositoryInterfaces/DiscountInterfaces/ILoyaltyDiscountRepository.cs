using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces
{
    public interface ILoyaltyDiscountRepository
    {
        //Repository for Loyalty Discounts. 
        public Task<LoyaltyDiscount?> GetByIDAsync(int id);
        public Task<LoyaltyDiscount?> GetByGuidAsync(Guid guid);
        public Task<IEnumerable<LoyaltyDiscount?>> GetAllAsync();
        public Task<IEnumerable<LoyaltyDiscount?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(LoyaltyDiscount LoyaltyDiscount);
        public Task UpdateAsync(LoyaltyDiscount LoyaltyDiscount);
        public Task DeleteAsync(LoyaltyDiscount LoyaltyDiscount);
    }
}
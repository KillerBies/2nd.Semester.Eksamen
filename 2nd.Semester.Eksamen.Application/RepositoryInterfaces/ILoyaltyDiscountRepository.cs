using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface ILoyaltyDiscountRepository
    {
        //Repository for Loyalty Discounts. 
        public Task<LoyaltyDiscount?> GetByIDAsync(int id);
        public Task<IEnumerable<LoyaltyDiscount?>> GetAllAsync();
        public Task<IEnumerable<LoyaltyDiscount?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(LoyaltyDiscount LoyaltyDiscount);
        public Task UpdateAsync(LoyaltyDiscount LoyaltyDiscount);
        public Task DeleteAsync(LoyaltyDiscount LoyaltyDiscount);
    }
}
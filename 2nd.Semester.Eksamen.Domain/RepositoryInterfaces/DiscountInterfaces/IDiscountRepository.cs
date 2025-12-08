using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllAsync();
        Task<List<Product>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<LoyaltyDiscount>> GetLoyaltyDiscountsAsync();
        Task<Campaign?> GetCampaignByDiscountIdAsync(int discountId);
        public Task<Discount> GetByIdAsync(int id);
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllAsync();
        Task<List<Product>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<LoyaltyDiscount>> GetLoyaltyDiscountsAsync();
    }
}

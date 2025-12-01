using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        //Repository for PunchCards.
        public Task<Product?> GetByIDAsync(int id);
        public Task<IEnumerable<Product?>> GetAllAsync();
        public Task<IEnumerable<Product?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Product Product);
        public Task UpdateAsync(Product Product);
        public Task DeleteAsync(Product Product);
    }
}

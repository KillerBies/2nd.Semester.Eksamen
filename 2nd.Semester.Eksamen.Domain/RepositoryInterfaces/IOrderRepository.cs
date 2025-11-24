using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        //Repository for Order.
        public Task<Order?> GetByIDAsync(int id);
        public Task<Order?> GetByCustomerIdAsync(int customerId);
        public Task<IEnumerable<Order?>> GetAllAsync();
        public Task<IEnumerable<Order?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Order Order);
        public Task UpdateAsync(Order Order);
        public Task DeleteAsync(Order Order);
    }
}

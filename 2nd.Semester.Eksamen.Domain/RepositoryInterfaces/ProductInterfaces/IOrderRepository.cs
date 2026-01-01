using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces
{
    public interface IOrderRepository
    {
        //Repository for Order.
        public Task<Order?> GetByIDAsync(int id);
        public Task<IEnumerable<Order?>> GetByCustomerIdAsync(int customerId);
        public Task<IEnumerable<Order?>> GetAllAsync();
        public Task<IEnumerable<Order?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Order Order);
        public Task UpdateAsync(Order Order);
        public Task<Order?> GetByGuidAsync(Guid guid);
        public Task DeleteAsync(Order Order);
        public Task<List<Order>?> GetByCustomerGuidAsync(Guid guid);
        public Task<List<Order>?> GetByEmployeeGuidAsync(Guid guid);
        public Task<List<Order>?> GetByTreatmentGuidAsync(Guid guid);
        public Task<List<Order>?> GetByProductGuidAsync(Guid guid);
    }
}

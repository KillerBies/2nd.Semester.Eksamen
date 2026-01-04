using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces
{
    public interface IOrderLineRepository
    {
        Task AddOrderLineAsync(OrderLine orderLine);
        Task<List<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId);
        Task UpdateOrderLineAsync(OrderLine orderLine);
        Task DeleteOrderLineAsync(OrderLine orderLine);
        public Task<OrderLine?> GetByGuidAsync(Guid guid);
    }

}

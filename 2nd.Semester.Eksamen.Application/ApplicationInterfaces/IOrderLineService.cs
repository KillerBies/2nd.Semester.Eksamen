using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IOrderLineService
    {
        Task AddOrderLineAsync(OrderLine orderLine);
        Task<List<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId); // Not Implemented (very much a placeholder)
        Task UpdateOrderLineAsync(OrderLine orderLine); // Not Implemented (very much a placeholder)
        Task DeleteOrderLineAsync(OrderLine orderLine); // Not Implemented (very much a placeholder)
    }
}

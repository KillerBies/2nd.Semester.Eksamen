using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public OrderLineRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        public async Task AddOrderLineAsync(OrderLine orderLine)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.OrderLines.Add(orderLine);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderLines
                .Where(ol => ol.OrderID == orderId)
                .ToListAsync();
        }

        public async Task UpdateOrderLineAsync(OrderLine orderLine)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.OrderLines.Update(orderLine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderLineAsync(OrderLine orderLine)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.OrderLines.Remove(orderLine);
            await _context.SaveChangesAsync();
        }
    }
}

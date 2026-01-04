using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
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
        public async Task<OrderLine?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderLines.FirstOrDefaultAsync(ol => ol.Guid == guid);
        }
        public async Task AddOrderLineAsync(OrderLine orderLine)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.OrderLines.Add(orderLine);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
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

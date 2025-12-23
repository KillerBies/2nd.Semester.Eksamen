using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public OrderRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Order?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders.FindAsync(id);
        }
        public async Task<IEnumerable<Order?>> GetByCustomerIdAsync(int customerId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders.Where(o => o.Booking.CustomerId == customerId).ToListAsync();
        }
        public async Task<IEnumerable<Order?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders.ToListAsync();
        }
        public async Task<IEnumerable<Order?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(Order Order)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.Orders.AddAsync(Order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(Order Order)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Orders.Update(Order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(Order Order)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders
                .Include(o => o.Booking)
                .ThenInclude(b => b.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
        }


    }
}

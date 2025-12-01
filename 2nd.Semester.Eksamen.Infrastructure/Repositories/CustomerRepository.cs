using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Customer?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.ToListAsync();
        }
        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewCustomerAsync(Customer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.Customers.AddAsync(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateCustomerAsync(Customer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Customers.Update(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteCustomerAsync(Customer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Customers.Remove(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

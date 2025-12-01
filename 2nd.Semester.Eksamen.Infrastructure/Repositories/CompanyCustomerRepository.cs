using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using _2nd.Semester.Eksamen.Domain;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class CompanyCustomerRepository : ICompanyCustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CompanyCustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Customer?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.ToListAsync();
        }
        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }


        public async Task<Customer?> GetByIDAsync(int id)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await  _context.CompanyCustomers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CompanyCustomer?> GetByPhoneAsync(string phoneNumber)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }
        //public async Task<IEnumerable<Customer?>> GetAllAsync()
        //{

        //}
        //public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        //{

        //}

        public async Task CreateNewAsync(CompanyCustomer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.CompanyCustomers.AddAsync(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> PhoneAlreadyExistsAsync(string phone)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.AnyAsync(c => c.PhoneNumber == phone);
        }
        public async Task UpdateAsync(PrivateCustomer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.PrivateCustomers.Update(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(PrivateCustomer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.PrivateCustomers.Remove(Customer);
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

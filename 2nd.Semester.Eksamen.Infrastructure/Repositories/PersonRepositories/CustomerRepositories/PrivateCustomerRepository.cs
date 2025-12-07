using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories
{
    public class PrivateCustomerRepository : IPrivateCustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public PrivateCustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }


        public async Task<Customer?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.ToListAsync();
        }
        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }

        
        public async Task<PrivateCustomer?> GetByPhoneAsync(string phoneNumber)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task CreateNewAsync(PrivateCustomer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                //Checks if phonenumber already exists in database. If it doesn't already exist, it continues creating customer.
                if (await _context.PrivateCustomers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber)) throw new Exception("Telefonnummer findes allerede!"); ;
                await _context.PrivateCustomers.AddAsync(customer);
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
            return await _context.PrivateCustomers.AnyAsync(c => c.PhoneNumber == phone);
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

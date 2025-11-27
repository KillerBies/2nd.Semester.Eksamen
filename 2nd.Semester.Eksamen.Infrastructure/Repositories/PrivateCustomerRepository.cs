using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
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
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task<PrivateCustomer?> GetByPhoneAsync(string phoneNumber)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }
        //public async Task<IEnumerable<Customer?>> GetAllAsync()
        //{

        //}
        //public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        //{

        //}

        public async Task CreateNewAsync(PrivateCustomer customer)
        {

            await using var _context = await _factory.CreateDbContextAsync();
            //Adds Customer to PrivateCustomers table in Database.
            _context.PrivateCustomers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PhoneAlreadyExistsAsync(string phone)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.AnyAsync(c => c.PhoneNumber == phone);
        }
        //public async Task UpdateAsync(PrivateCustomer Customer)
        //{

        //}
        public async Task DeleteAsync(PrivateCustomer customer)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            _context.PrivateCustomers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PrivateCustomer customer)
        {
            await using var context = await _factory.CreateDbContextAsync();
            context.PrivateCustomers.Update(customer);
            await context.SaveChangesAsync();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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


            //Adds Customer to Customers table in Database.
            await using var _context = await _factory.CreateDbContextAsync();
            _context.CompanyCustomers.Add(customer);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> PhoneAlreadyExistsAsync(string phone)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.CompanyCustomers.AnyAsync(c => c.PhoneNumber == phone);
        }
        //public async Task UpdateAsync(PrivateCustomer Customer)
        //{

        //}
        //public async Task DeleteAsync(PrivateCustomer Customer)
        //{
        //}


    }
}

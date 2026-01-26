using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CustomerQueryRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        async Task<Customer?> ICustomerQueryRepository.GetByPhoneNumberAsync(string PhoneNumber)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.AsNoTracking().Include(c=>c.BookingHistory).Include(c=>c.Address).FirstOrDefaultAsync(c => c.PhoneNumber == PhoneNumber);
        }
        async Task<Customer?> ICustomerQueryRepository.GetByIDAsync(Guid id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.AsNoTracking().Include(c => c.BookingHistory).Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
        }
        async Task<IEnumerable<Customer>?> ICustomerQueryRepository.GetAllCustomersAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Customers.AsNoTracking().Include(c => c.BookingHistory).Include(c => c.Address).ToListAsync();
        }
    }
}

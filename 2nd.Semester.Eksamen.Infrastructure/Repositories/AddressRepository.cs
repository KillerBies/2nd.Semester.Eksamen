using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public AddressRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        public async Task CreateNewAsync(Address address)
        {
            await using var context = await _factory.CreateDbContextAsync();
            context.Adresses.Add(address);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Address address)
        {
            await using var context = await _factory.CreateDbContextAsync();
            context.Adresses.Remove(address);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address?>> GetAllAsync()
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Adresses.ToListAsync();
        }

        public async Task<Address?> GetByIDAsync(int id)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Adresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Address address)
        {
            await using var context = await _factory.CreateDbContextAsync();
            context.Adresses.Update(address);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address?>> GetByFilterAsync(Filter filter)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Adresses.ToListAsync();
        }
    }
}

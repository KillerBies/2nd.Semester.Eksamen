using _2nd.Semester.Eksamen.Application.DTO;
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
    public class EmployeeRepository : IEmployeeRepository
    {
        // Midlertidlig liste til test af skabelse af employee
        private readonly List<Employee> _employees = new();

        private readonly IDbContextFactory<AppDbContext> _factory;
        public EmployeeRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task CreateNewAsync(Employee employee, Address address)
        {
            await using var _context = await _factory.CreateDbContextAsync();

            //Save address 
            _context.Adresses.Add(address);
            await _context.SaveChangesAsync(); 

            // Link employee to the new address
            employee.AddressId = address.Id;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Employee?>> GetAllAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.ToListAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Employee?>> GetByFilterAsync(Filter filter)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            // Example filtering – adjust as needed
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty("filter.Name")) // Implement filter feature to actually make this work
                query = query.Where(e => e.Name.Contains("filter.Name"));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Employee?>> GetBySpecialtyAsync(string Category)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees
                .Where(e => e.Specialty == Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllUserCardsAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();

            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIDAsync(int id)
        {
            await using var ctx = await _factory.CreateDbContextAsync();

            return await ctx.Employees
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task UpdateAsync(Employee employee)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            ctx.Employees.Update(employee); 
            await ctx.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            await _context.SaveChangesAsync();
        }

    }
}

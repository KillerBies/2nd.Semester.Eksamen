using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public EmployeeRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task CreateNewAsync(Employee Employee)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Employee Employee)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Employee?>> GetAllAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.ToListAsync();
        }
        public async Task<Employee?> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Employee?>> GetByFilterAsync(Domain.Filter filter)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Employee?>> GetBySpecialtyAsync(string Category)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees
                .Where(e => e.Specialty == Category)
                .ToListAsync();
        }
        public async Task UpdateAsync(Employee Employee)
        {
            throw new NotImplementedException();
        }
    }
}

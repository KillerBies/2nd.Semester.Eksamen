using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public TreatmentRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Treatment> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Treatment>> GetAllAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Treatments.ToListAsync();
        }
        public async Task<IEnumerable<Treatment>> GetByFilterAsync(Filter filter)
        {
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(Treatment treatment)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Treatment treatment)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Treatment treatment)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Employee>> GetByCategory(string category)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees
                .Where(e => e.Specialty == category)
                .ToListAsync();
        }
    }
}

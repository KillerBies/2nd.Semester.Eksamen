using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public EmployeeRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task CreateNewAsync(Employee employee)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<IEnumerable<Employee?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.ToListAsync();
        }
        public async Task UpdateAsync(Employee employee)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(Employee employee)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<IEnumerable<Employee?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            // Example filtering – adjust as needed
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty("filter.Name")) // Implement filter feature to actually make this work
                query = query.Where(e => e.Name.Contains("filter.Name"));

            return await query.ToListAsync();
        }
        public async Task<Employee?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            var result = await _context.Employees
                                       .Include(e => e.Address)
                                       .FirstOrDefaultAsync(e => e.Id == id);
            return result;
        }
        public async Task<IEnumerable<Employee?>> GetByTreatmentSpecialtiesAsync(List<string> specialties)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.Where(e => specialties.All(s=>e.Specialties.Contains(s))).ToListAsync();
        }

    }
}

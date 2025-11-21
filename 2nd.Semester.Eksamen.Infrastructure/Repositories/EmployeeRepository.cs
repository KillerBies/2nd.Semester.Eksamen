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

        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateNewAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Employee?>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Employee?>> GetByFilterAsync(Filter filter)
        {
            // Example filtering – adjust as needed
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty("filter.Name")) // Implement filter feature to actually make this work
                query = query.Where(e => e.Name.Contains("filter.Name"));

            return await query.ToListAsync();
        }
        public async Task<Employee?> GetByIDAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<EmployeeUserCardModel>> GetAllUserCards()
        {
            return await _context.Employees
                .Select(e => new EmployeeUserCardModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    PhoneNumber = e.PhoneNumber
                })
                .ToListAsync();
        }

    }
}


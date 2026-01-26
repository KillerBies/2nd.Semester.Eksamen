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
    public class EmployeeQueryRepository : IEmployeeQueryRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public EmployeeQueryRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        async Task<Employee?> IEmployeeQueryRepository.GetEmployeeByPhoneNumberAsync(string phone)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.AsNoTracking().Include(c => c.Address).Include(e => e.Appointments).FirstOrDefaultAsync(e=>e.PhoneNumber == phone);
        }
        async Task<IEnumerable<Employee>?> IEmployeeQueryRepository.GetAllEmployeesAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.AsNoTracking().Include(c => c.Address).Include(e=>e.Appointments).ToListAsync();
        }
        async Task<Employee?> IEmployeeQueryRepository.GetEmployeeByIDAsync(Guid id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.AsNoTracking().Include(e => e.Address).Include(e => e.Appointments).FirstOrDefaultAsync(e => e.Id == id);
        }
        async Task<List<string>> IEmployeeQueryRepository.GetAllDistinctEmployeeSpecialtiesAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Employees.AsNoTracking().SelectMany(e => e.Specialties).Distinct().ToListAsync();
        }
    }
}

using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
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
        public async Task CreateNewEmployeeAsync(Employee employee)
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
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var _context = await _factory.CreateDbContextAsync();

            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var employeeToUpdate = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
                if (employeeToUpdate == null)
                    throw new ArgumentNullException("Denne medarbejder kan ikke findes");
                employeeToUpdate.WorkEnd = employee.WorkEnd;
                employeeToUpdate.WorkStart = employee.WorkStart;
                employeeToUpdate.Specialties = employee.Specialties;
                employeeToUpdate.BasePriceMultiplier = employee.BasePriceMultiplier;
                employeeToUpdate.TrySetName(employee.Name);
                employeeToUpdate.TrySetLastName(employee.Name, employee.LastName);
                employeeToUpdate.TrySetGender(employee.Gender);
                employeeToUpdate.TrySetEmail(employee.Email);
                employeeToUpdate.TrySetAddress(employee.Address.City, employee.Address.PostalCode, employee.Address.StreetName, employee.Address.HouseNumber);
                employeeToUpdate.TrySetExperience(employee.ExperienceLevel);
                employeeToUpdate.PhoneNumber = employee.PhoneNumber;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                if (await _context.BookedTreatments.AnyAsync(t => t.EmployeeId == employee.Id))
                    throw new Exception("Medarbejder kan ikke slettes med pending behandlinger");
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
    }
}

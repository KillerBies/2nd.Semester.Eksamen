using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.EmployeeRepositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public ScheduleRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<EmployeeSchedule> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.EmployeeSchedules.FindAsync(id);
        }
        public async Task<EmployeeSchedule> GetByEmployeeIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.EmployeeSchedules.FirstAsync(e=>e.EmployeeId == id);
        }
        public async Task<IEnumerable<EmployeeSchedule>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.EmployeeSchedules.ToListAsync();
        }
        public async Task<IEnumerable<EmployeeSchedule>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(EmployeeSchedule EmployeeSchedule)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                // check if schedule exists
                var existing = await _context.EmployeeSchedules
                    .FirstOrDefaultAsync(s => s.EmployeeId == EmployeeSchedule.EmployeeId);

                if (existing != null)
                {
                    // update existing schedule
                    _context.Entry(existing).CurrentValues.SetValues(EmployeeSchedule);
                }
                else
                {
                    await _context.EmployeeSchedules.AddAsync(EmployeeSchedule);
                }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(EmployeeSchedule EmployeeSchedule)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.EmployeeSchedules.Update(EmployeeSchedule);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(EmployeeSchedule EmployeeSchedule)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.EmployeeSchedules.Remove(EmployeeSchedule);
                await _context.SaveChangesAsync();
                await transaction.RollbackAsync();
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task BookScheduleAsync(TreatmentBooking TreatmentBooking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var schedule = await _context.EmployeeSchedules.FirstAsync(t => t.EmployeeId == TreatmentBooking.Employee.Id);
                if (!schedule.BookTreatmentOnDate(TreatmentBooking.Start, TreatmentBooking.End)) throw new Exception();
                _context.EmployeeSchedules.Update(schedule);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(Exception)
            {
                await transaction.RollbackAsync(); throw;
            }
        }
    }
}

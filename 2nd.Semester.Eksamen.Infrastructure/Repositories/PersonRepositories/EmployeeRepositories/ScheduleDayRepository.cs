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

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.EmployeeRepositories
{
    public class ScheduleDayRepository : IScheduleDayRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public ScheduleDayRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<ScheduleDay> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.ScheduleDays.Include(sd => sd.TimeRanges).FirstOrDefaultAsync(sd=>sd.Id==id);
        }
        public async Task<IEnumerable<ScheduleDay>> GetByEmployeeIDAsync(int employeeid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.ScheduleDays.Include(sd => sd.TimeRanges).Where(sd => sd.EmployeeId == employeeid).ToListAsync();
        }
        public async Task<IEnumerable<ScheduleDay>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.ScheduleDays.ToListAsync();
        }
        public async Task<IEnumerable<ScheduleDay>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(ScheduleDay ScheduleDay)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.ScheduleDays.AddAsync(ScheduleDay);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(ScheduleDay ScheduleDay)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.ScheduleDays.Update(ScheduleDay);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(ScheduleDay ScheduleDay)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.ScheduleDays.Remove(ScheduleDay);
                await _context.SaveChangesAsync();
                await transaction.RollbackAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task BookScheduleAsync(TreatmentBooking TreatmentBooking)
        {
            //var _context = await _factory.CreateDbContextAsync();
            //using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            //try
            //{
            //    var employee = await _context.ScheduleDays.FindAsync(ScheduleDay.EmployeeId);
            //    var schedule = await _context.ScheduleDays.FirstAsync(t => t.EmployeeId == TreatmentBooking.Employee.Id);
            //    if (!schedule.BookTreatmentOnDate(TreatmentBooking.Start, TreatmentBooking.End, TreatmentBooking.Treatment.Name, employee.WorkStart, employee.WorkEnd, TreatmentBooking.BookingID)) throw new Exception();
            //    _context.EmployeeSchedules.Update(schedule);
            //    await _context.SaveChangesAsync();
            //    await transaction.CommitAsync();
            //}
            //catch (Exception)
            //{
            //    await transaction.RollbackAsync(); throw;
            //}
            throw new NotImplementedException();
        }
    }
}

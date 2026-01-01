using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
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
        public async Task<ScheduleDay?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.ScheduleDays.FirstOrDefaultAsync(e => e.Guid == guid);
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
                ScheduleDay.Guid = Guid.NewGuid();
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
        public async Task BookVacation(DateOnly start, DateOnly end, int employeeId)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                if (await _context.ScheduleDays.AnyAsync(sd => sd.EmployeeId == employeeId && sd.Date > start && sd.Date < end && sd.TimeRanges.Any(tr=>tr.Type != "Freetime" || tr.Type != "Unavailable"))) throw new Exception("The Vacation Overlaps with pre-existing Plans");
                var days = new List<ScheduleDay>();
                Guid activityId = Guid.NewGuid();
                var employee = await _context.Employees.FindAsync(employeeId);
                for (var date = start; date <= end; date = date.AddDays(1))
                {
                    var day = await _context.ScheduleDays.Include(sd => sd.TimeRanges).FirstOrDefaultAsync(es => es.EmployeeId == employeeId && es.Date == date);
                    if (day == null)
                    {
                        day = new ScheduleDay(date, employee.WorkStart, employee.WorkEnd) { EmployeeId=employee.Id};
                    }
                    if(!day.BookDayForVacation(activityId,employee.WorkStart,employee.WorkEnd)) throw new Exception("The Vacation Overlaps with pre-existing Plans");
                    _context.ScheduleDays.Update(day);
                    await _context.SaveChangesAsync();
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
    }
}

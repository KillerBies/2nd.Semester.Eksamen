using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories.BookingRepositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public BookingRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task CreateNewBookingAsync(Booking booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                if (await _context.Bookings.AnyAsync(b => b.CustomerId == booking.CustomerId && b.Start < booking.End && b.End > booking.Start)) throw new Exception("The booking Overlaps");
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                foreach (var treatment in booking.Treatments)
                {
                    string treatmentName = (await _context.Treatments.FindAsync(treatment.TreatmentId)).Name;
                    var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                    var day = await _context.ScheduleDays.Include(sd=>sd.TimeRanges).FirstOrDefaultAsync(es => es.EmployeeId == treatment.EmployeeId && es.Date== DateOnly.FromDateTime(treatment.Start));
                    if(day==null)
                    {
                        day = new ScheduleDay(DateOnly.FromDateTime(treatment.Start), employee.WorkStart, employee.WorkEnd);
                    }
                    Console.WriteLine(treatment.EmployeeId);
                    day.EmployeeId = treatment.EmployeeId;
                    day.AddBooking(treatment);
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
        public async Task CancelBookingAsync(Booking booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                foreach (var treatment in booking.Treatments)
                {
                    string treatmentName = (await _context.Treatments.FindAsync(treatment.TreatmentId)).Name;
                    var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                    var day = await _context.ScheduleDays.FirstOrDefaultAsync(es => es.EmployeeId == treatment.EmployeeId && es.Date == DateOnly.FromDateTime(treatment.Start));
                    if (day == null)
                    {
                        break;
                    }
                    day.CancelBooking(treatment, booking.Id);
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
        public async Task<Booking?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.FindAsync(id);
        }
        public async Task<IEnumerable<Booking?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.ToListAsync();
        }
        public async Task<IEnumerable<Booking?>> GetByFilterAsync(Domain.Filter filter)
        {
            await using var _context = await _factory.CreateDbContextAsync();
           return await _context.Bookings.Where(c => c.Status == filter.Status).OrderBy(c => c.Start).Include(c => c.Customer).ToListAsync();
        }
        public async Task UpdateAsync(Booking booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();
                foreach (var treatment in booking.Treatments)
                {
                    string treatmentName = (await _context.Treatments.FindAsync(treatment.TreatmentId)).Name;
                    var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                    var day = await _context.ScheduleDays.FirstOrDefaultAsync(es => es.EmployeeId == treatment.EmployeeId && es.Date == DateOnly.FromDateTime(treatment.Start));
                    if (day == null)
                    {
                        break;
                    }
                    day.UpdateDaySchedule(treatment, booking.Id);
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
        public async Task<IEnumerable<Booking>> GetByCustomerId(int CustomerId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.Where(b => b.CustomerId == CustomerId).ToListAsync();
        }
        public async Task<bool> BookingOverlapsAsync(Booking Booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.AnyAsync(b=>b.CustomerId == Booking.CustomerId && b.Overlaps(Booking.Start, Booking.End));
        }
    }
}

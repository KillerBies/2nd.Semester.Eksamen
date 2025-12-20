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
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;

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
                Guid ActivityId = Guid.NewGuid();
                foreach (var treatment in booking.Treatments)
                {
                    var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                    var day = await _context.ScheduleDays.Include(sd=>sd.TimeRanges).FirstOrDefaultAsync(es => es.EmployeeId == treatment.EmployeeId && es.Date== DateOnly.FromDateTime(treatment.Start));
                    string treatmentName = (await _context.Treatments.FindAsync(treatment.TreatmentId)).Name;
                    if(day==null)
                    {
                        day = new ScheduleDay(DateOnly.FromDateTime(treatment.Start), employee.WorkStart, employee.WorkEnd);
                    }
                    day.EmployeeId = treatment.EmployeeId;
                    day.AddBooking(treatment, ActivityId, treatmentName);
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
                var trackedBooking = await _context.Bookings.Include(b => b.Treatments).FirstOrDefaultAsync(b => b.Id == booking.Id);
                if (trackedBooking == null)
                    throw new Exception("Booking not found");
                foreach (var treatment in trackedBooking.Treatments.ToList())
                {
                    var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                    var day = await _context.ScheduleDays.FirstOrDefaultAsync(es => es.EmployeeId == treatment.EmployeeId && es.Date == DateOnly.FromDateTime(treatment.Start));
                    if (day == null) continue;
                    day.CancelBooking(treatment);
                    _context.ScheduleDays.Update(day);
                    await _context.SaveChangesAsync();
                    _context.BookedTreatments.Remove(treatment);
                    await _context.SaveChangesAsync();
                }
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                _context.Bookings.Remove(trackedBooking);
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
            return await _context.Bookings
        .Include(b => b.Customer)
            .ThenInclude(c => c.Address)

        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Treatment)

        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.TreatmentBookingProducts)
                .ThenInclude(tbp => tbp.Product)

        .FirstOrDefaultAsync(b => b.Id == id);
        }






        public async Task<IEnumerable<Booking?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.Include(b=>b.Customer).ThenInclude(c=>c.Address).Include(b=>b.Treatments).ThenInclude(b=>b.Treatment).Include(b=>b.Treatments).ThenInclude(b=> b.Employee).ThenInclude(e=>e.Address).ToListAsync();
        }






        public async Task<IEnumerable<Booking?>> GetByFilterAsync(Domain.Filter filter)
        {
           var _context = await _factory.CreateDbContextAsync();
           return await _context.Bookings.Where(c => c.Status == filter.Status).OrderBy(c => c.Start).Include(c => c.Customer).Include(c => c.Treatments).ThenInclude(t => t.Treatment).Include(c=>c.Treatments).ThenInclude(t=>t.Employee).ToListAsync();
        }
        










        public async Task UpdateAsync(Booking booking)
        {
            await using var context = await _factory.CreateDbContextAsync();
            await using var transaction =
                await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                //Load existing booking WITH treatments
                var existingBooking = await context.Bookings
                    .Include(b => b.Treatments)
                    .FirstOrDefaultAsync(b => b.Id == booking.Id);

                if (existingBooking == null)
                    throw new InvalidOperationException("Booking not found");

                //Cancel old treatments in schedules
                foreach (var oldTreatment in existingBooking.Treatments)
                {
                    var scheduleDay = await context.ScheduleDays
                        .Include(sd => sd.TimeRanges)
                        .FirstOrDefaultAsync(sd =>
                            sd.EmployeeId == oldTreatment.EmployeeId &&
                            sd.Date == DateOnly.FromDateTime(oldTreatment.Start));

                    scheduleDay?.CancelBooking(oldTreatment);
                }

                //Remove old treatment bookings
                context.BookedTreatments.RemoveRange(existingBooking.Treatments);

                //Add new treatments + update schedules
                foreach (var treatment in booking.Treatments)
                {
                    var employee = await context.Employees.FindAsync(treatment.EmployeeId)
                        ?? throw new InvalidOperationException("Employee not found");

                    var treatmentEntity = await context.Treatments.FindAsync(treatment.TreatmentId)
                        ?? throw new InvalidOperationException("Treatment not found");

                    var dayDate = DateOnly.FromDateTime(treatment.Start);

                    var scheduleDay = await context.ScheduleDays
                        .Include(sd => sd.TimeRanges)
                        .FirstOrDefaultAsync(sd =>
                            sd.EmployeeId == treatment.EmployeeId &&
                            sd.Date == dayDate);

                    if (scheduleDay == null)
                    {
                        scheduleDay = new ScheduleDay(dayDate, employee.WorkStart, employee.WorkEnd)
                        {
                            EmployeeId = treatment.EmployeeId
                        };

                        context.ScheduleDays.Add(scheduleDay);
                    }

                    var activityId = Guid.NewGuid();

                    // Domain logic (modifies TimeRanges)
                    scheduleDay.AddBooking(treatment, activityId, treatmentEntity.Name);

                    // Create new booked treatment
                    context.BookedTreatments.Add(new TreatmentBooking(
                        treatment.TreatmentId,
                        treatment.EmployeeId,
                        treatment.Start,
                        treatment.End)
                    {
                        Price = treatment.Price,
                        BookingID = booking.Id
                    });
                }

                //Update scalar properties of booking
                context.Entry(existingBooking).CurrentValues.SetValues(booking);

                //Persist everything once
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
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
        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
    }
}

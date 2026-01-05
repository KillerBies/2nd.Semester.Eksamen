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
using _2nd.Semester.Eksamen.Domain.Entities.History;

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
                booking.Guid = Guid.NewGuid();
                foreach(var t in booking.Treatments)
                {
                    t.Guid = Guid.NewGuid();
                }
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                foreach (var treatment in booking.Treatments)
                {
                    var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                    var day = await _context.ScheduleDays.Include(sd => sd.TimeRanges).FirstOrDefaultAsync(es => es.EmployeeId == treatment.EmployeeId && es.Date == DateOnly.FromDateTime(treatment.Start));
                    string treatmentName = (await _context.Treatments.FindAsync(treatment.TreatmentId)).Name;
                    if (day == null)
                    {
                        day = new ScheduleDay(DateOnly.FromDateTime(treatment.Start), employee.WorkStart, employee.WorkEnd);
                    }
                    day.EmployeeId = treatment.EmployeeId;
                    day.AddBooking(treatment, booking.Guid, treatmentName);
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
                //Load existing booking with treatments
                var bookingToCancel = await _context.Bookings.Include(b => b.Treatments).FirstOrDefaultAsync(b => b.Id == booking.Id);

                if (bookingToCancel == null)
                    throw new InvalidOperationException("Booking not found");

                //Cancel old treatments in schedules
                foreach (var treatmentToCancel in bookingToCancel.Treatments)
                {
                    var scheduleDay = await _context.ScheduleDays.Include(sd => sd.TimeRanges).FirstOrDefaultAsync(sd => sd.EmployeeId == treatmentToCancel.EmployeeId && sd.Date == DateOnly.FromDateTime(treatmentToCancel.Start));
                    if (scheduleDay != null)
                    {
                        if (scheduleDay.CancelBooking(treatmentToCancel)) ;
                        {
                            var timeranges = await _context.TimeRanges.Where(t => t.ScheduleDayId == scheduleDay.Id).ToListAsync();
                            _context.TimeRanges.RemoveRange(timeranges);
                            _context.ScheduleDays.Remove(scheduleDay);
                        }
                    }
                }

                //Remove old treatment bookings
                _context.BookedTreatments.RemoveRange(bookingToCancel.Treatments);

                //Remove customer if they don't want their data saved and have no other bookings
                Customer customer = await _context.Customers.FindAsync(booking.CustomerId);
                bool hasBookings = !(_context.Bookings.Where(b => b.CustomerId == customer.Id && b.Id != booking.Id && b.Status != BookingStatus.Pending).ToList().Any());
                _context.Bookings.Remove(bookingToCancel);
                await _context.SaveChangesAsync();
                if (customer.SaveAsCustomer == false && hasBookings)
                {
                    //maybe remove all the bookings a customer has (since none are pending)
                    _context.Customers.Remove(customer);
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
        public async Task CancelBookingByIdAsync(int BookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                //Load existing booking with treatments
                var bookingToCancel = await _context.Bookings.Include(b => b.Treatments).FirstOrDefaultAsync(b => b.Id == BookingId);

                if (bookingToCancel == null)
                    throw new InvalidOperationException("Booking not found");

                //Cancel old treatments in schedules
                foreach (var treatmentToCancel in bookingToCancel.Treatments)
                {
                    var scheduleDay = await _context.ScheduleDays.Include(sd => sd.TimeRanges).FirstOrDefaultAsync(sd => sd.EmployeeId == treatmentToCancel.EmployeeId && sd.Date == DateOnly.FromDateTime(treatmentToCancel.Start));
                    if (scheduleDay != null)
                    {
                        if (scheduleDay.CancelBooking(treatmentToCancel)) ;
                        {
                            var timeranges = await _context.TimeRanges.Where(t => t.ScheduleDayId == scheduleDay.Id).ToListAsync();
                            _context.TimeRanges.RemoveRange(timeranges);
                            _context.ScheduleDays.Remove(scheduleDay);
                        }
                    }
                }

                //Remove old treatment bookings
                _context.BookedTreatments.RemoveRange(bookingToCancel.Treatments);

                //Remove customer if they don't want their data saved and have no other bookings
                Customer customer = await _context.Customers.FindAsync(bookingToCancel.CustomerId);
                bool hasBookings = !(_context.Bookings.Where(b => b.CustomerId == customer.Id && b.Id != BookingId && b.Status != BookingStatus.Pending).ToList().Any());
                _context.Bookings.Remove(bookingToCancel);
                if (customer.SaveAsCustomer == false && hasBookings)
                {
                    _context.Customers.Remove(customer);
                }
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.BookingId == bookingToCancel.Id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
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
            return await _context.Bookings
        .Include(b => b.Customer)
            .ThenInclude(c => c.Address)

        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Treatment)
           .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Employee)
        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.TreatmentBookingProducts)
                .ThenInclude(tbp => tbp.Product)

        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Employee)

        .FirstOrDefaultAsync(b => b.Id == id);
        }



        public async Task TryDeleteBookingAtPayment(Booking booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                //Load existing booking with treatments
                var bookingToDelete = await _context.Bookings.Include(b => b.Treatments).FirstOrDefaultAsync(b => b.Id == booking.Id);

                if (bookingToDelete == null)
                    throw new InvalidOperationException("Booking not found");

                //Remove old treatment bookings
                _context.BookedTreatments.RemoveRange(bookingToDelete.Treatments);

                //Remove customer if they don't want their data saved and have no other bookings
                Customer customer = await _context.Customers.FindAsync(booking.CustomerId);
                bool hasBookings = _context.Bookings.Where(b => b.CustomerId == customer.Id && b.Id != booking.Id && b.Status != BookingStatus.Pending).ToList().Any();
                if (customer.SaveAsCustomer == false && !hasBookings)
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(o => o.BookingId == bookingToDelete.Id);
                    if (order != null)
                    {
                        _context.Orders.Remove(order);
                    }
                    _context.Bookings.Remove(bookingToDelete);
                    //remove customers bookings
                    //None issue maybe? Everytime a booking is payed it deletes it so why would there be any bookings in the database?
                    _context.Customers.Remove(customer);
                }
                else
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(o => o.BookingId == bookingToDelete.Id);
                    if (order != null)
                    {
                        _context.Orders.Remove(order);
                    }
                    _context.Bookings.Remove(bookingToDelete);
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


        public async Task<IEnumerable<Booking?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.Include(b => b.Customer).ThenInclude(c => c.Address).Include(b => b.Treatments).ThenInclude(b => b.Treatment).Include(b => b.Treatments).ThenInclude(b => b.Employee).ThenInclude(e => e.Address).ToListAsync();
        }






        public async Task<IEnumerable<Booking?>> GetByFilterAsync(Domain.Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.Where(c => c.Status == filter.Status).OrderBy(c => c.Start).Include(c => c.Customer).Include(c => c.Treatments).ThenInclude(t => t.Treatment).Include(c => c.Treatments).ThenInclude(t => t.Employee).ToListAsync();
        }











        public async Task UpdateAsync(Booking booking)
        {
            await using var context = await _factory.CreateDbContextAsync();
            await using var transaction =
                await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                //Load existing booking with treatments
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

                    if(scheduleDay != null)
                    {
                        if (scheduleDay.CancelBooking(oldTreatment));
                        {
                            var timeranges = await context.TimeRanges.Where(t => t.ScheduleDayId == scheduleDay.Id).ToListAsync();
                            context.TimeRanges.RemoveRange(timeranges);
                            context.ScheduleDays.Remove(scheduleDay);
                        }
                    }
                    await context.SaveChangesAsync();
                }

                //Remove old treatment bookings
                context.BookedTreatments.RemoveRange(existingBooking.Treatments);

                //Add new treatments + update schedules
                foreach (var treatment in booking.Treatments)
                {
                    var employee = await context.Employees.FindAsync(treatment.EmployeeId)
                        ?? throw new Exception("Employee not found");

                    var treatmentEntity = await context.Treatments.FindAsync(treatment.TreatmentId)
                        ?? throw new Exception("Treatment not found");

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
                        await context.SaveChangesAsync();
                        //Needed and important (if the changes arent saved and the employee has another treatment in this booking then the next treatment will see that no shcedule day exists and make another one (copies))
                    }

                    // Create new booked treatment
                    var NewTreatment = new TreatmentBooking(treatment.TreatmentId, treatment.EmployeeId, treatment.Start, treatment.End) { Price = treatment.Price, BookingID = booking.Id, Guid = Guid.NewGuid()};
                    // TimeRanges
                    scheduleDay.AddBooking(NewTreatment, booking.Guid, treatmentEntity.Name);
                    context.BookedTreatments.Add(NewTreatment);
                }

                //Update scalar properties of booking
                context.Entry(existingBooking).CurrentValues.SetValues(booking);

                //Update and save everything
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
        public async Task<Booking?> GetByGuidAsync(Guid guid)
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
        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Employee)
        .FirstOrDefaultAsync(b => b.Guid == guid);
        }

        public async Task<bool> BookingOverlapsAsync(Booking Booking)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings.AnyAsync(b => b.CustomerId == Booking.CustomerId && b.Overlaps(Booking.Start, Booking.End));
        }
        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<List<Booking>?> GetByCustomerGuidAsync(Guid guid)
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
        .Include(b => b.Treatments)
        .ThenInclude(t => t.Employee)
        .ThenInclude(e => e.Address)
        .Where(b => b.Customer.Guid == guid).ToListAsync();
        }
        public async Task<List<Booking>?> GetByEmployeeGuidAsync(Guid guid)
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
        .Where(b => b.Treatments.Any(t => t.Employee.Guid == guid)).ToListAsync();
        }
        public async Task<List<Booking>?> GetByTreatmentGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
        .Include(b => b.Customer)
            .ThenInclude(c => c.Address)

        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Treatment)
                    .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Employee)
            .ThenInclude(e=>e.Address)
        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.TreatmentBookingProducts)
                .ThenInclude(tbp => tbp.Product)
        .Where(b => b.Treatments.Any(t => t.Treatment.Guid == guid)).ToListAsync();
        }

        public async Task<Booking?> GetByTreatmentBookingGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return (await _context.Bookings.Include(b => b.Customer)
            .ThenInclude(c => c.Address)
        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Treatment)
                    .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Employee)
        .Include(b => b.Treatments)
            .ThenInclude(tb => tb.TreatmentBookingProducts)
                .ThenInclude(tbp => tbp.Product).FirstOrDefaultAsync(b=>b.Treatments.Any(t=>t.Guid == guid)));
        }

        public async Task<OrderSnapshot?> GetSnapShotByTreatmentBookingGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return (await _context.OrderSnapshots.Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                    .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot).FirstOrDefaultAsync(o=> o.BookingSnapshot.TreatmentSnapshot.Any(t => t.TreatmentBookingGuid == guid)));
        }

    }
}

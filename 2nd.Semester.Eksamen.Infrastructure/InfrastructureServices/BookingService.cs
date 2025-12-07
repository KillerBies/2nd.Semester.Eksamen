using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class BookingService : IBookingService
    {


        private readonly IBookingRepository _bookingRepository;
        private readonly IPrivateCustomerRepository _privateCustomerRepository;
        private readonly ICompanyCustomerRepository _companyCustomerRepository;
        private readonly AppDbContext _context;
        public BookingService(AppDbContext context,IBookingRepository bookingRepository, IPrivateCustomerRepository privateCustomerRepository, ICompanyCustomerRepository companyCustomerRepository)
        {
            _bookingRepository = bookingRepository;
            _privateCustomerRepository = privateCustomerRepository;
            _companyCustomerRepository = companyCustomerRepository;
            _context = context;
        }




        public async Task<IEnumerable<Booking>> GetBookingsByFilterAsync(Filter filter)
        {

            return await _bookingRepository.GetByFilterAsync(filter);

        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            
                var customer = booking.Customer;
                //Checks if Customer has chosen to be deleted from database. If false sendes to infrastructure layer and deletes customer.
                if (booking.Customer.SaveAsCustomer == false && customer is PrivateCustomer privateCustomer)
                {
                    await _privateCustomerRepository.DeleteAsync(privateCustomer);
                }
                if (booking.Customer.SaveAsCustomer == false && customer is CompanyCustomer companyCustomer)
                {
                    await _companyCustomerRepository.DeleteAsync(companyCustomer);
                }
                //Deletes customer
                await _bookingRepository.CancelBookingAsync(booking);
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Check overlapping bookings
                if (await _context.Bookings.AnyAsync(b =>
                    b.CustomerId == booking.CustomerId &&
                    b.Start < booking.End && b.End > booking.Start))
                {
                    throw new Exception("Booking overlaps existing booking.");
                }

                // Persist the booking
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                // Add treatments & update schedules
                Guid ActivityId = Guid.NewGuid();
                foreach (var treatment in booking.Treatments)
                {
                    await AddTreatmentToEmployeeSchedule(treatment, ActivityId);
                }

                await transaction.CommitAsync();
                return booking;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task AddTreatmentToEmployeeSchedule(TreatmentBooking treatment, Guid bookingId)
        {
            var scheduleDay = await _context.ScheduleDays
                .Include(d => d.TimeRanges)
                .FirstOrDefaultAsync(d =>
                    d.EmployeeId == treatment.EmployeeId &&
                    d.Date == DateOnly.FromDateTime(treatment.Start));

            if (scheduleDay == null)
            {
                var employee = await _context.Employees.FindAsync(treatment.EmployeeId);
                scheduleDay = new ScheduleDay(DateOnly.FromDateTime(treatment.Start), employee.WorkStart, employee.WorkEnd)
                {
                    EmployeeId = employee.Id
                };
                await _context.ScheduleDays.AddAsync(scheduleDay);
            }

            var timerange = new TimeRange
            {
                Start = TimeOnly.FromDateTime(treatment.Start),
                End = TimeOnly.FromDateTime(treatment.End),
                Type = "Booked",
                ActivityId = bookingId
            };

            scheduleDay.TimeRanges.Add(timerange);
            await _context.SaveChangesAsync();
        }
 
    }
}

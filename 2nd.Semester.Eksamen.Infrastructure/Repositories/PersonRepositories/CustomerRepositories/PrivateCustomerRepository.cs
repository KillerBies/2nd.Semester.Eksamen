using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories
{
    public class PrivateCustomerRepository : IPrivateCustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public PrivateCustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<PrivateCustomer?> GetCustomerByIdAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers
            .Include(c => c.BookingHistory)
            .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Booking?> GetBookingWithTreatmentsAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
            .Include(b => b.Customer)
            .Include(b => b.Treatments)
            .ThenInclude(tb => tb.Treatment)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
        }


        public async Task<Order?> GetOrderByBookingIdAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Orders.FirstOrDefaultAsync(o => o.BookingId == bookingId);
        }


        public async Task AddOrderAsync(Order order)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateOrderAsync(Order order)
        {
            var _context = await _factory.CreateDbContextAsync();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<PrivateCustomer?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.ToListAsync();
        }
        public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }


        public async Task<PrivateCustomer?> GetByPhoneAsync(string phoneNumber)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task CreateNewAsync(PrivateCustomer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                //Checks if phonenumber already exists in database. If it doesn't already exist, it continues creating customer.
                if (await _context.PrivateCustomers.AnyAsync(c => c.PhoneNumber == customer.PhoneNumber)) throw new Exception("Telefonnummer findes allerede!"); ;
                await _context.PrivateCustomers.AddAsync(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> PhoneAlreadyExistsAsync(string phone)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PrivateCustomers.AnyAsync(c => c.PhoneNumber == phone);
        }
        public async Task UpdateAsync(PrivateCustomer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.PrivateCustomers.Update(Customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(PrivateCustomer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                PrivateCustomer trackedcustomer = await _context.PrivateCustomers.FirstOrDefaultAsync(c => c.Id == Customer.Id);
                _context.PrivateCustomers.Remove(trackedcustomer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Booking?> GetBookingWithTreatmentsAndTreatmentAsync(int bookingId)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public Task<IEnumerable<Booking?>> GetBookingsForCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetNextPendingBookingAsync(int customerId)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Treatments)
                    .ThenInclude(tb => tb.Treatment) // ensures Treatment is loaded
                .Where(b => b.CustomerId == customerId && b.Status == BookingStatus.Pending)
                .OrderBy(b => b.Start) // earliest pending booking
                .FirstOrDefaultAsync();
        }
        public async Task UpdateDiscountAsync(Discount discount)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

    }
}

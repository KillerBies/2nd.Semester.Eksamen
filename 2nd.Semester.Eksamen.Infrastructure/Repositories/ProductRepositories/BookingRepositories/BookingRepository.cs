using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
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
            //DEBUG
            var tracked = _context.ChangeTracker.Entries()
    .Select(e => new { Entity = e.Entity.GetType().Name, e.State })
    .ToList();
            //DEBUG END
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                if (await _context.Bookings.AnyAsync(b => b.CustomerId == booking.CustomerId && b.Start < booking.End && b.End > booking.Start)) throw new Exception("The booking Overlaps");
                await _context.Bookings.AddAsync(booking);
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
        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Bookings
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
    }
}

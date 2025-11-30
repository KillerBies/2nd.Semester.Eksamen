using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
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
                if (await BookingOverlapsAsync(booking)) throw new Exception();
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
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
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
            return await _context.Bookings.AnyAsync(b=>b.CustomerId == Booking.CustomerId && !(b.Overlaps(Booking.Start, Booking.End)));
        }
    }
}

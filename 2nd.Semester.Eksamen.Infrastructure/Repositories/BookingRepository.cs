using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public BookingRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task CreateNewAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Booking booking)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
        public async Task<Booking> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Booking>> GetByFilterAsync(Domain.Filter filter)
        {
            await using var _context = await _factory.CreateDbContextAsync();
           return await _context.Bookings.Where(c => c.Status == filter.Status).OrderBy(c => c.Start).Include(c => c.Customer).ToListAsync();
        }
        public async Task UpdateAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}

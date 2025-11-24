using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}

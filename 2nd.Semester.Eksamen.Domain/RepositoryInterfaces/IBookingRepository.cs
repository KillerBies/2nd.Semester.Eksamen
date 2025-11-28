using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface IBookingRepository
    {
        //Repository for Bookings. 
        public Task<Booking?> GetByIDAsync(int id);
        public Task<IEnumerable<Booking?>> GetAllAsync();
        public Task<IEnumerable<Booking?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Booking Booking);
        public Task UpdateAsync(Booking Booking);
        public Task DeleteAsync(Booking Booking);
        public Task<IEnumerable<Booking>> GetByCustomerId(int CustomerId);
    }
}

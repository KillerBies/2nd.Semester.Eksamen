using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces
{
    public interface IBookingRepository
    {
        //Repository for Bookings. 
        public Task<Booking?> GetByIDAsync(int id);
        public Task<IEnumerable<Booking?>> GetAllAsync();
        public Task<IEnumerable<Booking?>> GetByFilterAsync(Filter filter);
        public Task CreateNewBookingAsync(Booking Booking);
        public Task UpdateAsync(Booking Booking);
        public Task CancelBookingAsync(Booking Booking);
        public Task CancelBookingByIdAsync(int BookingId);
        public Task<IEnumerable<Booking>> GetByCustomerId(int CustomerId);
        public Task<bool> BookingOverlapsAsync(Booking Booking);
        public Task<Booking?> GetByGuidAsync(Guid guid);
        public Task<List<Booking>?> GetByCustomerGuidAsync(Guid guid);
        public Task<List<Booking>?> GetByEmployeeGuidAsync(Guid guid);
        public Task<List<Booking>?> GetByTreatmentGuidAsync(Guid guid);

    }
}

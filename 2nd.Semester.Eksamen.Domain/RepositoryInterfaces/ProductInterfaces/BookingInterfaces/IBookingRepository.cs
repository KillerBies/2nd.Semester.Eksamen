using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces
{
    public interface IBookingRepository
    {
        //Repository for Bookings C.U.D. Operations. 
        public Task CreateNewBookingAsync(Booking Booking);
        public Task UpdateAsync(Booking Booking);
        public Task CancelBookingAsync(Guid BookingId);
        public Task TryDeleteBookingAtPayment(Booking booking);

    }
}

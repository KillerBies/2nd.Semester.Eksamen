using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces
{
    public interface IBookingCancellationService
    {
        public Task CancelBooking(Booking booking);
    }
}

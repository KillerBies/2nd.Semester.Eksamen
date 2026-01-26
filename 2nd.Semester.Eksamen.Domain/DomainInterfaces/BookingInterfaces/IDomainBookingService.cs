using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces
{
    public interface IDomainBookingService
    {

        public Task CreateBooking(Guid CustomerId, DateTime start, DateTime end, List<TreatmentBooking> treatments);
        public Task CancelBooking(Booking booking);
    }
}

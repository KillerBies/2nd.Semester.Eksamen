using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IBookingService
    {
        public Task<IEnumerable<Booking>> GetBookingsByFilterAsync(Filter filter);
        public Task DeleteBookingAsync(Booking booking);
    }
}

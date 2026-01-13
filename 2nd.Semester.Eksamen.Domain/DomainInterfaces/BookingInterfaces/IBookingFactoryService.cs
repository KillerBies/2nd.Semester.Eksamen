using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces
{
    public interface IBookingFactoryService
    {
        public Task CreateBookingAsync(Guid customerId, DateTime start, DateTime end, IReadOnlyCollection<TreatmentBooking> treatments);
    }
}

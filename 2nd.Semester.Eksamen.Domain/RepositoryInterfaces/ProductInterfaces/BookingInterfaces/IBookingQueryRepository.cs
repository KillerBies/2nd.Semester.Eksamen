using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces
{
    public interface IBookingQueryRepository
    {
        public Task<ICollection<Booking>> GetForCustomerAsync(Guid customerId, DateTime from, DateTime to);
        public Task<ICollection<Booking>> GetForEmployee(Guid employeeId, DateTime from, DateTime to);
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.History;
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
        public Task<List<Booking>> GetForCustomerAsync(Guid customerId, DateTime from, DateTime to);
        public Task<List<Booking>> GetForEmployeeAsync(Guid employeeId, DateTime from, DateTime to);
        public Task<List<Booking>> GetAllAsync();
        public Task<Booking?> GetByBookingAsync(Guid Bookingid);
        public Task<List<Booking?>> GetByFilterAsync(Filter filter);

        public Task<List<Booking>?> GetByCustomerGuidAsync(Guid guid);
        public Task<List<Booking>?> GetByEmployeeGuidAsync(Guid guid);
        public Task<List<Booking>?> GetByTreatmentGuidAsync(Guid guid);

        public Task<Booking?> GetByTreatmentBookingGuidAsync(Guid guid);
        public Task<OrderSnapshot?> GetSnapShotByTreatmentBookingGuidAsync(Guid guid);

    }
}

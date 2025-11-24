using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;

namespace _2nd.Semester.Eksamen.Domain.DomainServices
{
    public class BookingDomainService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingDomainService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<bool> IsBookingOverlappingAsync(int EmployeeId, DateTime startDate, DateTime endDate)
        {
            var bookings = await _bookingRepository.GetByEmployeeId(EmployeeId);
            return bookings.Any(b => b.Overlaps(endDate, startDate));
        }
    }
}

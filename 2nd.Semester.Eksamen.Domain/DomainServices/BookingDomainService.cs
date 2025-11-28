using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;

namespace _2nd.Semester.Eksamen.Domain.DomainServices
{
    public class BookingDomainService : IBookingDomainService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITreatmentBookingRepository _treatmentBookingRepository;
        public BookingDomainService(IBookingRepository bookingRepository, ITreatmentBookingRepository treatmentBookingRepository)
        {
            _treatmentBookingRepository = treatmentBookingRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<bool> IsBookingOverlappingAsync(Booking booking)
        {
            var bookings = await _bookingRepository.GetByCustomerId(booking.CustomerId);
            if (!(bookings.Any(b => b.Overlaps(booking.Start, booking.End)))) return true;
            foreach(var treatmentbooking in booking.Treatments)
            {
                var treatmentBookings = await _treatmentBookingRepository.GetByEmployeeIDAsync(treatmentbooking.Employee.Id);
                if (treatmentBookings.Any(tb => treatmentbooking.Employee.Appointments.Any(a => a.Overlaps(tb.Start, tb.End)))) return true;
            }
            return false;
        }
        private 
    }
}

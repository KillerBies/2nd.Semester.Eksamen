using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using _2nd.Semester.Eksamen.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Schedules
{
    public class Schedule
    {
        private readonly List<Booking> _bookings = new List<Booking>();
        public void AddBooking(Booking booking)
        {
            if (_bookings.Any(b => b.Overlaps(booking.Start, booking.End)))
                throw new BookingOverlapException("Denne booking overlapper");
            _bookings.Add(booking);
        }
        public void CancelBooking(Guid BookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.Guid == BookingId);
            if (booking == null)
                throw new BookingNotFoundException("Denne booking kan ikke findes");
            if (booking.Start < DateTime.UtcNow) 
                throw new BookingPastException("Denne booking kan ikke aflyses da den er i fortiden");
            _bookings.Remove(booking);
        }
        public void RemoveFinishedBooking(Guid BookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.Guid == BookingId);
            if (booking == null)
                throw new BookingNotFoundException("Denne booking kan ikke findes");
            if (booking.Status == BookingStatus.Completed)
                throw new GeneralBookingException("Denne booking kan ikke slettes. Færdiggør den eller aflys den");
            _bookings.Remove(booking);
        }
    }
}

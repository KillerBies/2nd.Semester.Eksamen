using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts
{
    public class Treatment : Product
    {
        //Elements of a treatment. Its info is stored in the database.
        //Treatment details
        public List<TreatmentBooking> Bookings { get; set; } = new();
        public List<string>? RequiredSpecialties { get; set; } = new();
        public TimeSpan Duration { get; private set; }

        public Treatment(string name, decimal price, string discription, string category, TimeSpan duration, List<string>? requiredSpecialties = null) : base(name, price, discription)
        {
            Category = category;
            Duration = duration;
            RequiredSpecialties = requiredSpecialties == null ? new() : requiredSpecialties;
        }
        public Treatment() { }

        //method to set duration
        public bool TrySetDuration(TimeSpan duration)
        {
            if (duration.TotalMinutes > 0)
            {
                Duration = duration;
                return true;
            }
            return false;
        }
        public void Delete()
        {
            if (!(Bookings.Any(b => b.Start >= DateTime.UtcNow || b.Booking.Status == BookingStatus.Pending)))
                throw new DomainException("Behandling kan ikke slettes da den har planlagt bookinger");
        }
    }
}
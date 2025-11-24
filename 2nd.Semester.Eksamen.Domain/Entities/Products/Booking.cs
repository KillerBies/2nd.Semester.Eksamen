using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class Booking : BaseEntity
    {
        //Elements of a booking


        //Customer details
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        //Booking details
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration {  get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        //Treatment details
        public List<TreatmentBooking> Treatments { get; set; } = new List<TreatmentBooking>();




        public Booking() { }
        public Booking(Customer customer, DateTime start, DateTime end)
        {
            Customer = customer;
            Start = start;
            End = end;
            Duration = ComputeDuration(start, end);
            Treatments = new List<TreatmentBooking>();
            Status = BookingStatus.Pending;
        }



        //method to change booking status
        public bool TryChangeStatus(BookingStatus newStatus)
        {
            Status = newStatus;
            return true;
        }

        //method to add treatment to booking
        public bool TryAddTreatment(TreatmentBooking treatment)
        {
            if (treatment == null) return false;
            Treatments.Add(treatment);
            return true;
        }

        //method to finish booking
        public void FinishBooking()
        {
            Status = BookingStatus.Completed;
        }

        public bool Overlaps(DateTime start, DateTime end)
        {
            return Start < end && End > start;
        }

        private TimeSpan ComputeDuration(DateTime start, DateTime end)
        {
            return end - start;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record BookingSnapshot : BaseSnapshot
    {
        public List<TreatmentSnapshot> TreatmentSnapshot { get; protected set; }
        public int CustomerSnapshotId { get; protected set; }
        public TimeSpan Duration { get; protected set; }
        public decimal Price { get; protected set; }
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }

        public CustomerSnapshot CustomerSnapshot { get; protected set; }
       
        public OrderSnapshot OrderSnapshot { get; protected set; }
        


        private BookingSnapshot() { }

        public BookingSnapshot(Booking booking) : base(booking.RefrenceId)
        {
            Duration = booking.Duration;
            Start = booking.Start;
            End = booking.End;
            CustomerSnapshot = CustomerSnapshot.CreateCustomerSnapshot(booking.Customer);
            TreatmentSnapshot = new List<TreatmentSnapshot>();
            foreach (var treatmentBooking in booking.Treatments)
            {
                TreatmentSnapshot.Add(new TreatmentSnapshot(treatmentBooking, this));
            }
        }

    }

}

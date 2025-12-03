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
        
        public List<TreatmentSnapshot> TreatmentSnapshot { get; init; }
        public CustomerSnapshot CustomerSnapshot { get; init; }

        


        public BookingSnapshot() { }

        public BookingSnapshot(Booking booking)
        {
            TreatmentSnapshot = booking.Treatments.Select(t => new TreatmentSnapshot(t.Treatment)).ToList();
            CustomerSnapshot = CustomerSnapshot.CreateCustomerSnapshot(booking.Customer);

        }

    }

}

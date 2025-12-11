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
        public List<TreatmentSnapshot> TreatmentSnapshot { get;  set; }
        public int? CustomerSnapshotId { get; set; }
        
        public CustomerSnapshot CustomerSnapshot { get;  set; }
       
        public OrderSnapshot OrderSnapshot { get; set; }
        


        private BookingSnapshot() { }

        public BookingSnapshot(Booking booking)
        {
            TreatmentSnapshot = new List<TreatmentSnapshot>();

          
            foreach (var t in booking.Treatments)
            {
                var snapshot = new TreatmentSnapshot(t.Treatment, this, t.Employee.BasePriceMultiplier);
                TreatmentSnapshot.Add(snapshot);
            }
            CustomerSnapshot = CustomerSnapshot.CreateCustomerSnapshot(booking.Customer);

            
            CustomerSnapshot.BookingSnapshot = this;
        }

    }

}

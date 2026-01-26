using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record TreatmentSnapshot : ProductSnapshot
    {
        //Snapshot of a treatment that had been booked
        //Snapshot is made at time of payment so no need to change anything here when its made.
        public TimeSpan Duration { get; protected set; }
        public decimal Price { get; protected set; }

        //Booking
        public int BookingSnapshotId { get; protected set; }
        public BookingSnapshot? BookingSnapshot { get; protected set; }




        //Employee
        public int EmployeeSnapshotId { get; protected set; }
        public EmployeeSnapshot EmployeeSnapshot { get; protected set; }

        private TreatmentSnapshot() { }

        public TreatmentSnapshot(TreatmentBooking treatment, BookingSnapshot bookingSnapshot) : base(treatment.Treatment)
        {
            BookingSnapshot = bookingSnapshot;
            Duration = treatment.Treatment.Duration;
            Price = treatment.Price;

            BookingSnapshot = bookingSnapshot;
            BookingSnapshotId = bookingSnapshot.Id;

            EmployeeSnapshot = new EmployeeSnapshot(treatment.Employee);
            EmployeeSnapshotId = EmployeeSnapshot.Id;
        }


    }

}

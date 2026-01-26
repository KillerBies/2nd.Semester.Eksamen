using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//There is no buisniss logic in bookings that requires the information of other bookings when canceling. 
//As such a CancelBooking() method in booking that checks if the booking has yet to start should be fine.
//The factory service is fine.
namespace _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts
{
    public class Booking : BaseEntity
    {
        //Elements of a booking


        //Customer details
        public int CustomerId { get; protected set; }
        public Customer Customer { get; protected set; }

        //Booking details
        public Order? Order { get; protected set; }
        public int? OrderId { get; protected set; }
        public DateTime Start { get; protected set; }
        public DateTime End { get; protected set; }
        public TimeSpan Duration {  get; protected set; }
        public BookingStatus Status { get; protected set; } = BookingStatus.Pending;

        //Treatment details
        public List<TreatmentBooking> Treatments { get; set; } = new List<TreatmentBooking>();




        public Booking() { }
        public Booking(Customer customer, DateTime start, DateTime end, List<TreatmentBooking> treatments)
        {
            CustomerId = customer.Id;
            Start = start;
            End = end;
            Duration = ComputeDuration(start, end);
            Treatments = treatments;
            Status = BookingStatus.Pending;
        }

        public Booking(int customerId, DateTime start, DateTime end, List<TreatmentBooking> treatments)
        {
            CustomerId = customerId;
            Start = start;
            End = end;
            Duration = ComputeDuration(start, end);
            Treatments = treatments;
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
            if (treatment == null) 
                return false;
            treatment.AddToBooking(Id);
            Treatments.Add(treatment);
            return true;
        }

        public bool AddToOrder(int orderId)
        {
            if (orderId <= 0)
                return false;
            OrderId = orderId;
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
        public void Delete()
        {
            if (!(DateTime.UtcNow > Start && Status == BookingStatus.Completed))
                throw new DomainException("Denne booking kan ikke slettes");
        }
        public void Cancel()
        {
            if (!(DateTime.UtcNow < Start && Status == BookingStatus.Pending))
                throw new DomainException("Denne booking kan ikke aflyses");
        }
        public void Update()
        {
            if (!(Status == BookingStatus.Pending))
                throw new DomainException("Denne booking kan ikke ændres");
        }
    }
}

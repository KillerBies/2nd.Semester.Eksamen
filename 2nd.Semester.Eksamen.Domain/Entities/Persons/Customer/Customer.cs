using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer
{
    public class Customer : Person
    {
        // Base class for customers
        public List<Booking> BookingHistory { get;  set; } = new();
        public int NumberOfVisists { get; set; }
        public decimal PointBalance { get; set; }
        public List<PunchCard> PunchCards { get;  set; } = new();
        public string? Notes { get; set; } = string.Empty;
        public bool SaveAsCustomer { get;  set; }

        public Customer() { }

        public Customer(
            string name,
            Address address,
            string phoneNumber,
            string email,
            string notes,
            bool saveAsCustomer
        ) : base(name, address, phoneNumber, email)
        {
            BookingHistory = new List<Booking>();
            PointBalance = 0;
            PunchCards = new List<PunchCard>();
            Notes = notes;
            SaveAsCustomer = saveAsCustomer;
        }

        // Method to change point balance
        public bool TryAddToPointBalance(decimal points)
        {
            if (points < 0) return false;

            PointBalance += points;
            return true;
        }

        // Method to redeem points
        public bool TryRedeemPoints(decimal points)
        {
            if (points <= PointBalance)
            {
                PointBalance -= points;
                return true;
            }

            return false;
        }

        // Add completed booking to history
        public bool TryAddToBookingHistory(Booking booking)
        {
            if (booking.Status == BookingStatus.Completed)
            {
                BookingHistory.Add(booking);
                return true;
            }

            return false;
        }

        // Add punchcard if customer doesn't have one for this treatment
        public bool TryAddPunchCard(Treatment treatment)
        {
            if (!PunchCards.Any(card => card.Treatment == treatment))
            {
                PunchCards.Add(new PunchCard(this, 1, treatment));
                return true;
            }

            return false;
        }

        public bool AddVisit()
        {
            // TODO: add check to see if payment went through
            NumberOfVisists++;
            return true;
        }
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer
{
    public class Customer : Person
    {
        //Base class for customers
        public List<Booking> BookingHistory { get; protected set; } = new List<Booking>();
        public int NumberOfVisists { get; protected set; }
        public decimal PointBalance { get; protected set; }
        public List<PunchCard> PunchCards { get; protected set; } = new List<PunchCard>();
        public string? Notes { get; protected set; } = string.Empty;
        public bool SaveAsCustomer { get; protected set; }
        public Customer() { }
        public Customer(string name, Address address, string phoneNumber, string email, string notes, bool saveAsCustomer) : base(name, address, phoneNumber, email)
        {
            BookingHistory = new List<Booking>();
            PointBalance = 0;
            PunchCards = new List<PunchCard>();
            Notes = notes;
            SaveAsCustomer = saveAsCustomer;
        }





        //method to change point balance
        public bool TryAddToPointBalance(decimal points)
        {
            if (points < 0) return false;
            PointBalance += points;
            return true;
        }



        //method to redeem points from point balance
        public bool TryRedeemPoints(decimal points)
        {
            if (points <= PointBalance)
            {
                PointBalance -= points;
                return true;
            }
            return false;
        }



        //method to add to booking history
        public bool TryAddToBookingHistory(Booking booking)
        {
            if (booking.Status == BookingStatus.Completed)
            {
                BookingHistory.Add(booking);
                return true;
            }
            return false;
        }



        //method to add a punchcard
        public bool TryAddPunchCard(Treatment treatment)
        {
            if (!PunchCards.Any(Card => Card.Treatment == treatment))
            {
                PunchCards.Add(new PunchCard(this, 1, treatment));
                return true;
            }
            return false;
        }
    }


}

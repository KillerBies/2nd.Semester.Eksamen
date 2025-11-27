using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    [NotMapped]
    public abstract class Customer : Person
    {
        //Base class for customers
        public List<Booking> BookingHistory { get; private set; } = new List<Booking>();
        public int NumberOfVisists { get; private set; }
        public decimal PointBalance { get; private set; }
        public List<PunchCard> PunchCards { get; private set; } = new List<PunchCard>();
        public string Notes { get; set; } = string.Empty;
        public bool SaveAsCustomer { get; set; }
        public Customer() { }
        public Customer(string name, Address address, string phoneNumber, string email, bool saveAsCustomer) : base(name, address, phoneNumber, email) 
        {
            BookingHistory = new List<Booking>();
            PointBalance = 0;
            PunchCards = new List<PunchCard>();
            Notes = string.Empty;
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


        //method to add to booking history.
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
        public bool AddVisit() //TODO: add check to see if the payment went through or/and if there was a valid booking
        {
            NumberOfVisists++;
            return true;
        }
    }


}

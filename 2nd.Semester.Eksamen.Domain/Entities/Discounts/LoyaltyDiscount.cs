using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2nd.Semester.Eksamen.Domain.Entities.Discounts
{
    public class LoyaltyDiscount : Discount
    {
        public string DiscountType { get; set; } = string.Empty;
        public int MinimumVisits { get; set; }

        public LoyaltyDiscount(int minimumVisits, string discountType, string name, decimal discountamount) : base(name, discountamount)
        {
            MinimumVisits = minimumVisits;
            DiscountType = discountType;
        }
        public LoyaltyDiscount() { }

        public bool CheckCustomer(Customer customer)
        {
            if (customer.BookingHistory.Count() >= MinimumVisits)
                return true;
            return false;
        }
    }
}

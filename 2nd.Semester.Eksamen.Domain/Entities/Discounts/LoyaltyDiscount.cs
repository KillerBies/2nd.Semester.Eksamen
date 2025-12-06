using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2nd.Semester.Eksamen.Domain.Entities.Discounts
{
    public class LoyaltyDiscount : Discount
    {
        public string DiscountType { get; set; } = string.Empty;
        public int MinimumVisits { get; set; }

        public LoyaltyDiscount(
            int minimumVisits, string discountType, string name, decimal treatmentDiscount, decimal productDiscount)
            : base(name, treatmentDiscount, productDiscount)
        {
            MinimumVisits = minimumVisits;
            DiscountType = discountType;
            IsLoyalty = true;
        }
        public LoyaltyDiscount() { }

        public bool CheckCustomer(Customer customer)
        {
            return customer.BookingHistory.Count() >= MinimumVisits;
        }
    }
}

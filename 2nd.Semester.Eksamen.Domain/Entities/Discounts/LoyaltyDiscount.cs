using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Produkter;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class LoyaltyDiscount : Discount
    {
        public string? DiscountType { get; set; }
        public int? MinimumVisits { get; set; }

        public LoyaltyDiscount()
        {

        }

        public bool CheckCustomer(Customer customer)
        {
            if (customer.BookingHistory.Count() >= MinimumVisits)
                return true;
            return false;
        }
    }
}

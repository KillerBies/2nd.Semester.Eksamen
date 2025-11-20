using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class LoyaltyDiscount: Discount
    {
        public string DiscountType { get; set; } = string.Empty;
        public int MinimumVisits { get; set; }

        public LoyaltyDiscount(int minimumVisits, string discountType, string name, decimal discountamount) : base(name,discountamount)
        {
            MinimumVisits = minimumVisits;
            DiscountType = discountType;
        }
        public LoyaltyDiscount() { }

    }
}

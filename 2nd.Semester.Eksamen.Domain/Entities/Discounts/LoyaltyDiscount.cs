using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class LoyaltyDiscount: Discount
    {
        public string? DiscountType { get; private set; }
        public int? MinimumVisits { get; private set; }
    }
}

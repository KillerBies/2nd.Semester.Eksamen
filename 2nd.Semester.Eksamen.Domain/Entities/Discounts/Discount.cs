using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Tilbud
{
    public class Discount: BaseDiscount
    {
        public LoyaltyDiscountType DiscountType { get; private set; }
        public int MinimumVisits { get; private set; }
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Produkter;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class Order: BaseEntity
    {
        public Booking? Booking { get; private set; }
        public List<ProductSnapshot>? Products { get; private set; }
        public decimal? Total { get; private set; }
        public decimal? DiscountedTotal { get; private set; }
        public Discount? AppliedDiscount { get; private set; }

        public Order() { }
    }
}

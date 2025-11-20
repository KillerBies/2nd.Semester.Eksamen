using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class Order: BaseEntity
    {
        public Booking Booking { get; private set; } = null!;
        public List<OrderLine> Products { get; private set; } = null!;
        public decimal Total { get; private set; } = 0;
        public decimal DiscountedTotal { get; private set; } = 0;
        public Discount AppliedDiscount { get; private set; } = null!;

        public Order() { }
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class Order : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get;  set; } = null!;
        public decimal Total { get; private set; }
        public decimal DiscountedTotal { get; private set; }
        public int AppliedDiscountId { get; private set; }
        public List<OrderLine> Products { get; set; } = new List<OrderLine>();


        public Order(int bookingId, decimal total, decimal discountedTotal, int appliedDiscountId)
        {
            BookingId = bookingId;
            Total = total;
            DiscountedTotal = discountedTotal;
            AppliedDiscountId = appliedDiscountId;
        }


        public void UpdateTotals(decimal total, decimal discountedTotal, int? appliedDiscountId)
        {
            Total = total;
            DiscountedTotal = discountedTotal;
            AppliedDiscountId = appliedDiscountId ?? 0;
        }


        public void AddOrderLine(OrderLine line)
        {
            if (!Products.Contains(line))
                Products.Add(line);
        }
    }

}

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
        public int BookingId { get; protected set; }
        public Booking Booking { get; protected set; } = null!;
        public decimal Total { get; protected set; }
        public decimal DiscountedTotal { get; protected set; }
        public int AppliedDiscountId { get; protected set; }
        public decimal VAT {  get; protected set; }
        public List<OrderLine> Products { get; protected set; } = new List<OrderLine>();

        public Order() { }
        public Order(int bookingId, decimal total, decimal discountedTotal, decimal vat, int appliedDiscountId)
        {
            BookingId = bookingId;
            Total = total;
            DiscountedTotal = discountedTotal;
            AppliedDiscountId = appliedDiscountId;
            VAT = vat;
        }


        public void UpdateTotals(decimal total, decimal discountedTotal, int? appliedDiscountId)
        {
            Total = total;
            DiscountedTotal = discountedTotal;
            AppliedDiscountId = appliedDiscountId ?? 0;
        }

        public void AddBooking(Booking booking)
        {
            Booking = booking;
            BookingId = booking.Id;
        }
        public void AddOrderLine(OrderLine line)
        {
            if (!Products.Contains(line))
                Products.Add(line);
        }
    }

}

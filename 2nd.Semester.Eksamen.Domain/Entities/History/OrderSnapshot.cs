using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record OrderSnapshot : BaseSnapshot
    {
        public BookingSnapshot BookingSnapshot { get; init; }
        
        public decimal CustomDiscount { get; init; }
        public DateOnly DateOfPayment { get; init; }
        public decimal TotalAfterDiscount { get; init; }
        public int? PdfID { get; init; }
        public List<OrderLineSnapshot> OrderLinesSnapshot { get; init; }
        public AppliedDiscountSnapshot AppliedDiscountSnapshot { get; init; }
        public OrderSnapshot() { }
        public OrderSnapshot(Order order, int customDiscount)
        {
            BookingSnapshot = new BookingSnapshot(order.Booking);
            CustomDiscount = customDiscount;
            OrderLinesSnapshot = order.Products.Select(oL => new OrderLineSnapshot(oL)).ToList();
            DateOfPayment = DateOnly.FromDateTime(DateTime.Now);
            AppliedDiscountSnapshot = new AppliedDiscountSnapshot(order.AppliedDiscount);
        }
    }

}

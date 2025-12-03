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
        public BookingSnapshot BookingSnapshot { get; private set; }
        
        public decimal? CustomDiscount { get; private set; }
        public DateOnly DateOfPayment { get; private set; }
        public decimal? TotalAfterDiscount { get; private set; }
        public int? PdfID { get; set; }
        public List<OrderLineSnapshot>? OrderLinesSnapshot { get; private set; }
        public AppliedDiscountSnapshot? AppliedDiscountSnapshot { get; private set; }
        private OrderSnapshot() { }
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

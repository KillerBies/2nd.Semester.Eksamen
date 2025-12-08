using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record OrderSnapshot : BaseSnapshot
    {
       public int? BookingSnapshotId { get; set; }
        public BookingSnapshot BookingSnapshot { get; private set; }

        public decimal? CustomDiscount { get; private set; }
        public DateOnly DateOfPayment { get; private set; }
        public decimal? TotalAfterDiscount { get; private set; }
        public byte[]? PdfInvoice { get; set; }
       
        public List<OrderLineSnapshot>? OrderLinesSnapshot { get; private set; } = new();
        public int? AppliedSnapshotId { get; set; }
        public AppliedDiscountSnapshot? AppliedDiscountSnapshot { get; private set; }
        private OrderSnapshot() { }
        public OrderSnapshot(Order order, Discount discount, Booking booking)
        {
            BookingSnapshot = new BookingSnapshot(booking);
            
            OrderLinesSnapshot = order.Products.Select(oL => new OrderLineSnapshot(oL)).ToList();
            DateOfPayment = DateOnly.FromDateTime(DateTime.Now);
            AppliedDiscountSnapshot = new AppliedDiscountSnapshot(discount);
        }
    }

}

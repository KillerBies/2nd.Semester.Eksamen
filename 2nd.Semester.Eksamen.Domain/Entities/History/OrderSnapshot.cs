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
       public int BookingSnapshotId { get; protected set; }
        public BookingSnapshot BookingSnapshot { get; protected set; }

        public decimal? CustomDiscount { get; protected set; }
        public DateOnly DateOfPayment { get; protected set; }
        public decimal? TotalAfterDiscount { get; protected set; }
        public byte[]? PdfInvoice { get; protected set; }
        public decimal VAT {  get; protected set; }
        public List<OrderLineSnapshot>? OrderLinesSnapshot { get; protected set; } = new();
        public int? AppliedSnapshotId { get; protected set; }
        public AppliedDiscountSnapshot? AppliedDiscountSnapshot { get; protected set; }
        private OrderSnapshot() { }
        public OrderSnapshot(Order order, Discount discount, Booking booking) : base(order.RefrenceId)
        {
            BookingSnapshot = new BookingSnapshot(booking);
            TotalAfterDiscount = order.DiscountedTotal;
            OrderLinesSnapshot = order.Products.Select(oL => new OrderLineSnapshot(oL)).ToList();
            DateOfPayment = DateOnly.FromDateTime(DateTime.Now);
            AppliedDiscountSnapshot = new AppliedDiscountSnapshot(discount);
            VAT = order.VAT;
        }
    }

}

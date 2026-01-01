using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.History;
namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class OrderSnapshotDTO
    {
        public int Id { get; private set; }
        public DateOnly PaymentDate { get; private set; }
        public string Name { get; private set; }
        public decimal? TotalPaid { get; private set; }
        public byte[]? PdfFile { get; private set; }

        public OrderSnapshotDTO(OrderSnapshot orderSnapshot)
        {
            Id = orderSnapshot.Id;
            PaymentDate = orderSnapshot.DateOfPayment;
            if (orderSnapshot.BookingSnapshot.CustomerSnapshot is PrivateCustomerSnapshot cust)
            { Name = orderSnapshot.BookingSnapshot.CustomerSnapshot.Name + " " + cust.LastName; }
            else { Name = orderSnapshot.BookingSnapshot.CustomerSnapshot.Name; }
            TotalPaid = orderSnapshot.TotalAfterDiscount;
            PdfFile = orderSnapshot.PdfInvoice;
        }




    }
}

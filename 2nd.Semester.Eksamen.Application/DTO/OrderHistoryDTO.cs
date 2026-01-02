using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class OrderHistoryDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateOnly DateOfPayment { get; set; }
        public List<TreatmentHistoryDTO> Treatments { get; set; }
        public string Discount { get; set; }
        public Guid DiscountGuid { get; set; }
        public List<ProductHistoryDTO> Products { get; set; }
        public OrderSnapshotDTO OrderSnapshotDTO { get; set; }
        public string CustomerName { get; set; }
        public Guid? CustomerGuid { get; set; }
        public decimal CustomDiscount { get; set; } = 0;
        public decimal? TotalPrice { get; set; } = 0;
        public Guid BookingGuid { get; set; }
        public TimeSpan BookingDuration { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public OrderHistoryDTO(OrderSnapshot os)
        {
            OrderSnapshotDTO = new OrderSnapshotDTO(os);
            CustomerName = os.BookingSnapshot.CustomerSnapshot.Name;
            CustomerGuid = os.BookingSnapshot.CustomerSnapshot.Guid;
            TotalPrice = os.TotalAfterDiscount;
            DateOfPayment = os.DateOfPayment;
            BookingStart = os.BookingSnapshot.Start;
            BookingEnd = os.BookingSnapshot.End;
            BookingDuration = os.BookingSnapshot.Duration;
            BookingGuid = os.BookingSnapshot.Guid;
            if (os.AppliedDiscountSnapshot != null)
            {
                DiscountGuid = os.AppliedDiscountSnapshot.Guid;
                Discount = os.AppliedDiscountSnapshot.Name;
            }
            if(os.CustomDiscount != null)
            {
                CustomDiscount=(decimal)os.CustomDiscount;
            }
            Id = os.Id;
            Guid = os.Guid;
            TotalPrice = os.TotalAfterDiscount;
            Treatments = os.BookingSnapshot.TreatmentSnapshot
                .Select(t => new TreatmentHistoryDTO(t, os))
                .ToList();
            Products = os.OrderLinesSnapshot
                .Select(ol => new ProductHistoryDTO(os,ol))
                .ToList();
        }
        public OrderHistoryDTO() { }
    }
}

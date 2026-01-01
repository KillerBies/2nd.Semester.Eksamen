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
        public List<TreatmentHistoryDTO> Treatments { get; set; }
        public List<ProductHistoryDTO> Products { get; set; }
        public string CustomerName { get; set; }
        public Guid? CustomerGuid { get; set; }
        public decimal? TotalPrice { get; set; } = 0;
        public OrderHistoryDTO(OrderSnapshot os)
        {
            CustomerName = os.BookingSnapshot.CustomerSnapshot.Name;
            CustomerGuid = os.BookingSnapshot.CustomerSnapshot.Guid;
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

using _2nd.Semester.Eksamen.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class ProductHistoryDTO
    {
        public int OrderId { get; set; }
        public Decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NumberSold { get; set; }
        public Guid ProductGuid { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid CustomerGuid { get; set; }
        public int ProductSnapShotId { get; set; }
        public ProductHistoryDTO(OrderSnapshot os, OrderLineSnapshot ps)
        {
            CustomerName = os.BookingSnapshot.CustomerSnapshot.Name;
            CustomerGuid = os.BookingSnapshot.CustomerSnapshot.Guid;
            Price = ps.ProductSnapshot.PricePerUnit;
            Name = ps.ProductSnapshot.Name;
            NumberSold = ps.NumberOfProducts;
            ProductGuid = ps.ProductSnapshot.Guid;
            ProductSnapShotId = ps.ProductSnapshot.Id;
            OrderId = os.Id;
        }
    }
}

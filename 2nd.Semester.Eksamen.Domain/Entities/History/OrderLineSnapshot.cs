using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record OrderLineSnapshot : BaseSnapshot
    {
        public int? OrderSnapshotId { get; set; }
        public OrderSnapshot OrderSnapshot { get; set; } = null!;
        public int ProductSnapshotId { get; set; }
        public  ProductSnapshot? ProductSnapshot { get; set; }
        public int NumberOfProducts { get; private set; } = 0;

        
        private OrderLineSnapshot() { }
        public OrderLineSnapshot(OrderLine orderLine)
        {
            Guid = orderLine.Guid;
            if (orderLine.LineProduct != null)
            {
            ProductSnapshot = new ProductSnapshot(orderLine.LineProduct);
            NumberOfProducts = orderLine.NumberOfProducts;
            }
        }
    }
}

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
        public int OrderSnapshotId { get; protected set; }
        public OrderSnapshot OrderSnapshot { get; protected set; } = null!;
        public int ProductSnapshotId { get; protected set; }
        public  ProductSnapshot ProductSnapshot { get; protected set; }
        public int NumberOfProducts { get; protected set; }

        
        private OrderLineSnapshot() { }
        public OrderLineSnapshot(OrderLine orderLine) : base(orderLine.RefrenceId)
        {
            if (orderLine.LineProduct != null)
            {
                ProductSnapshot = new ProductSnapshot(orderLine.LineProduct);
                NumberOfProducts = orderLine.NumberOfProducts;
            }
        }
    }
}

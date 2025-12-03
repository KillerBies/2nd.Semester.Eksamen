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
        public int OrderSnapshotID { get; init; }
        public OrderSnapshot OrderSnapshot { get; init; } = null!;
        public ProductSnapshot ProductSnapshot { get; init; }
        public int NumberOfProducts { get; init; }

        
        
        public OrderLineSnapshot(OrderLine orderLine)
        {

            ProductSnapshot = new ProductSnapshot(orderLine.LineProduct);
    
            NumberOfProducts = orderLine.NumberOfProducts;
        }
    }
}

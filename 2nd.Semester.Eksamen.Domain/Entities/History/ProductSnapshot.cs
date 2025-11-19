using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public class ProductSnapshot : BaseEntity
    {
        //Products sold or used
        public string Name { get; private set; } = string.Empty;
        public decimal PricePerUnit { get; private set; }
        public int? NumberSold { get; set; }

        public ProductSnapshot() { }
        public ProductSnapshot(Product product, int numberSold)
        {
            Name = product.Name;
            PricePerUnit = product.Price;
            NumberSold = numberSold;
        }

    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record ProductSnapshot : BaseSnapshot
    {
        //Products sold or used
        public string Name { get;   set; } = string.Empty;
        public decimal PricePerUnit { get;   set; }
        public decimal? DiscountedPrice { get;   set; }
        public List<OrderLineSnapshot> OrderLines { get; set; } = new();
        
        
        


        protected ProductSnapshot() { }
        public ProductSnapshot(Product product)
        {
            Name = product.Name;
            PricePerUnit = product.Price;
            DiscountedPrice = product.DiscountedPrice;
            Guid = product.Guid;
        }

    }

}

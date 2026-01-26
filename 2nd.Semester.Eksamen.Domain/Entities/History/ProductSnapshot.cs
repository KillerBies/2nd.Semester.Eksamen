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
        public string Name { get; protected set; } = string.Empty;
        public decimal PricePerUnit { get; protected set; }
        public decimal? DiscountedPrice { get; protected set; }
        public string Category { get; protected set; }
        public List<OrderLineSnapshot> OrderLines { get; protected set; } = new();
        
        
        


        protected ProductSnapshot() { }
        public ProductSnapshot(Product product) : base(product.RefrenceId)
        {
            Name = product.Name;
            PricePerUnit = product.Price;
            DiscountedPrice = product.DiscountedPrice;
            Category = product.Category;
        }

    }

}

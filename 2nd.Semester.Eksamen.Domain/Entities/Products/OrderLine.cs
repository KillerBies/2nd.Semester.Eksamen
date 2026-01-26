using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class OrderLine : BaseEntity
    {
        public Guid OrderID { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product LineProduct { get; set; } = null!;
        public int NumberOfProducts { get; set; } = 1;

        public OrderLine() { }

        public OrderLine(Order order, Product product, int quantity = 1)
        {
            Order = order;
            OrderID = order.Id;
            LineProduct = product;
            ProductId = product.Id;
            NumberOfProducts = quantity;
        }
    }
}

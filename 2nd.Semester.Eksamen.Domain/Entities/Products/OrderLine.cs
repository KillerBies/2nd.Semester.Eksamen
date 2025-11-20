using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class OrderLine : BaseEntity
    {
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;
        public Product LineProduct { get; set; } = null!;
        public int NumberOfProducts { get; set; }
        public OrderLine() { }
    }
}

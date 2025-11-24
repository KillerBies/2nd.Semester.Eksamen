using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; } = new();
        public string Name { get; set; } = null!;
        public decimal Price { get; set; } = new();
    }
}

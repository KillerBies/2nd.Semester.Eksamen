using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class ProductOverviewDTO
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set;  } = string.Empty;

        public ProductOverviewDTO()
        {
        }
        public ProductOverviewDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Category = product.Category;
            Guid = product.Guid;
        }
        public ProductOverviewDTO(ProductSnapshot product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.PricePerUnit;
            Guid = product.Guid;
        }
    }
}

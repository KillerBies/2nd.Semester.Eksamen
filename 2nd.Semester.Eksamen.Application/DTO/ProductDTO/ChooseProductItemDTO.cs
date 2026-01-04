using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class ChooseProductItemDTO
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public bool Status { get; set; } = false;

        public ChooseProductItemDTO(Product product)
        {
            ProductName = product.Name;
            ProductId = product.Id;
        }
    }
}

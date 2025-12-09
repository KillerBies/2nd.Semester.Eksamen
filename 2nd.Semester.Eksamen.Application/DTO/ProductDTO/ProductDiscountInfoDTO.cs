using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO
{
    public class ProductDiscountInfoDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? DiscountName { get; set; }
        public bool IsLoyalty { get; set; }
    }

}

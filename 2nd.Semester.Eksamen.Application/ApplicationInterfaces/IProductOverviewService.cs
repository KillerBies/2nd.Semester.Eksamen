using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IProductOverviewService
    {
        public Task<List<ProductOverviewDTO>> GetAllProductOverviewsAsync();
        public Task<List<OrderSnapshotDTO>> GetProductSalesHistoryAsync(string ProductName);
        public Task<List<string>> GetAllCategoriesAsync();
        public Task DeleteProductAsync(ProductOverviewDTO product);
    }
}

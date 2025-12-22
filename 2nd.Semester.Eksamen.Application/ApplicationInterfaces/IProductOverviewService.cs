using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IProductOverviewService
    {
        public Task<List<DTO.ProductDTO.ProductOverviewDTO>> GetAllProductOverviewsAsync();
        //public Task<List<OrderSnapshotOverviewDTO>> GetProductSalesHistoryAsync(string ProductName);
    }
}

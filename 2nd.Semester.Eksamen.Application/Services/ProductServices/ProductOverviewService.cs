using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.ProductServices
{
    public class ProductOverviewService : IProductOverviewService
    {
        IProductRepository _productRepository;
        ISnapshotRepository _snapshotRepository;
        public ProductOverviewService(IProductRepository productRepository, ISnapshotRepository snapshotRepository)
        {
            _snapshotRepository = snapshotRepository;
            _productRepository = productRepository;
        }
        public async Task<List<ProductOverviewDTO>> GetAllProductOverviewsAsync()
        {
            return (await _productRepository.GetAllAsync()).Select(p => new ProductOverviewDTO() {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category
            }).ToList();
        }
        public async Task<List<OrderSnapshotDTO>> GetProductSalesHistoryAsync(string ProductName)
        {
            return (await _snapshotRepository.GetByProduct(ProductName)).Select(os => new OrderSnapshotDTO(os)).ToList();
        }
        public async Task<List<string>> GetAllCategoriesAsync()
        {
            return await _productRepository.GetAllProductCategoriesAsync();
        }
        public async Task DeleteProductAsync(ProductOverviewDTO product)
        {
            try
            {
                await _productRepository.DeleteAsync(product.Id);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<ProductOverviewDTO> GetProductByIdAsync(int id)
        {
            return new ProductOverviewDTO((await _productRepository.GetByIDAsync(id)));
        }
    }
}

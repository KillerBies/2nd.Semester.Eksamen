using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
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
        public ProductOverviewService(IProductRepository productRepository) 
        { 
            _productRepository = productRepository;
        }
        public async Task<List<DTO.ProductDTO.ProductOverviewDTO>> GetAllProductOverviewsAsync()
        {
            return (await _productRepository.GetAllAsync()).Select(p=>new ProductOverviewDTO() { 
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.GetType().Name
            }).ToList();
        }
    }
}

using _2nd.Semester.Eksamen.Application.Adapters;
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
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;
        private DTO_to_Domain _domainAdapter;
        public ProductApplicationService(IProductRepository productRepo, DTO_to_Domain dTO_To_Domain) 
        { 
            _productRepository = productRepo;
            _domainAdapter = dTO_To_Domain;
        }
        public async Task CreateNewProductAsync(NewProductDTO productDTO)
        {
            try
            {
                await _productRepository.CreateNewAsync(_domainAdapter.DTOProductToNewDomain(productDTO));
            }catch (Exception ex)
            {
                throw new Exception("Noget gik galt, produktet blev ikke oprettet");
            }
        }
    }
}

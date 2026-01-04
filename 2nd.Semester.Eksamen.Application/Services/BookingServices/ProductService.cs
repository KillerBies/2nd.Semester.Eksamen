using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
    {
        return await _repo.GetByIdsAsync(ids);
    }
    public async Task<List<ChooseProductItemDTO>> GetAllProductItemsAsync()
    {
        return (await _repo.GetAllProductsNoMatterTypeAsync()).Select(p => new ChooseProductItemDTO(p)).ToList();
    }
}


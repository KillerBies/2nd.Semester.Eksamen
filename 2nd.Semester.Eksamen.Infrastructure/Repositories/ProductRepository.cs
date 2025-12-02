using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    public ProductRepository(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task CreateNewAsync(Product product)
    {
        using var _Context = _factory.CreateDbContext();
        _Context.Products.Add(product);
        await _Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        using var _context = _factory.CreateDbContext();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product?>> GetAllAsync()
    {
        using var _context = _factory.CreateDbContext();
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetByFilterAsync(Filter filter)
    {
        using var _context = _factory.CreateDbContext();
        // adjust depending on Filter properties
        return await _context.Products
            .Where(p => true)
            .ToListAsync();
    }

    public async Task<Product?> GetByIDAsync(int id)
    {
        using var _context = _factory.CreateDbContext();
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetByIdsAsync(List<int> ids)
    {
        using var _context = _factory.CreateDbContext();
        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        using var _context = _factory.CreateDbContext();
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}

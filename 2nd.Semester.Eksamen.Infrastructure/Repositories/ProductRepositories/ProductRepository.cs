using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public ProductRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Product?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Products.Where(p => p.GetType() == typeof(Product)).ToListAsync();
        }
        public async Task<IEnumerable<Product?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(Product Product)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.Products.AddAsync(Product);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(Product Product)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var ProductToUpdate = await _context.Products.FindAsync(Product.Id);
                ProductToUpdate.Name = Product.Name;
                ProductToUpdate.Price = Product.Price;
                ProductToUpdate.Description = Product.Description;
                ProductToUpdate.Category = Product.Category;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Product>> GetByIdsAsync(List<int> ids)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<List<string>> GetAllProductCategoriesAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Products.Select(p => p.Category).ToListAsync();
        }
    }
}

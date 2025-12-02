using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.DiscountRepositories
{
    public class LoyaltyDiscountRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public LoyaltyDiscountRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<LoyaltyDiscount?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.LoyaltyDiscounts.FindAsync(id);
        }
        public async Task<IEnumerable<LoyaltyDiscount?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.LoyaltyDiscounts.ToListAsync();
        }
        public async Task<IEnumerable<LoyaltyDiscount?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(LoyaltyDiscount LoyaltyDiscount)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.LoyaltyDiscounts.AddAsync(LoyaltyDiscount);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
        public async Task UpdateAsync(LoyaltyDiscount LoyaltyDiscount)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.LoyaltyDiscounts.Update(LoyaltyDiscount);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(LoyaltyDiscount LoyaltyDiscount)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.LoyaltyDiscounts.Remove(LoyaltyDiscount);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

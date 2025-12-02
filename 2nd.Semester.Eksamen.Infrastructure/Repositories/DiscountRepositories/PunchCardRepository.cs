using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.DiscountRepositories
{
    public class PunchCardRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public PunchCardRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        //Repository for PunchCards.
        public async Task<PunchCard> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PunchCards.FindAsync(id);
        }
        public async Task<IEnumerable<PunchCard>> GetByCustomerIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PunchCards.Where(pc=>pc.Customer.Id==id).ToListAsync();
        }
        public async Task<IEnumerable<PunchCard>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.PunchCards.ToListAsync();
        }
        public async Task<IEnumerable<PunchCard>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(PunchCard PunchCard)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.PunchCards.AddAsync(PunchCard);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(PunchCard PunchCard)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.PunchCards.Update(PunchCard);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(Exception)
            {
                await transaction.RollbackAsync(); 
                throw;
            }
        }
        public async Task DeleteAsync(PunchCard PunchCard)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.PunchCards.Remove(PunchCard);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

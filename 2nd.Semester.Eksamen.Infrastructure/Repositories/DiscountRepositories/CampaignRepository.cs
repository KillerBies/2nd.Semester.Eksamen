using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.DiscountRepositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public CampaignRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Campaign?> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Campaigns.FindAsync(id);
        }
        public async Task<Campaign?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Campaigns.FirstOrDefaultAsync(c=>c.Guid == guid);
        }
        public async Task<IEnumerable<Campaign?>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Campaigns.ToListAsync();
        }
        public async Task<IEnumerable<Campaign?>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(Campaign campaign)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                campaign.Guid = Guid.NewGuid();
                await _context.Campaigns.AddAsync(campaign);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(Campaign Campaign)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var DiscountToUpdate = await _context.Campaigns.FindAsync(Campaign.Id);
                DiscountToUpdate.ProductDiscount = Campaign.ProductDiscount;
                DiscountToUpdate.TreatmentDiscount = Campaign.TreatmentDiscount;
                DiscountToUpdate.AppliesToTreatment = Campaign.AppliesToTreatment;
                DiscountToUpdate.AppliesToProduct = Campaign.AppliesToProduct;
                DiscountToUpdate.Description = Campaign.Description;
                DiscountToUpdate.Start = Campaign.Start;
                DiscountToUpdate.End = Campaign.End;
                DiscountToUpdate.ProductsInCampaign = Campaign.ProductsInCampaign;
                DiscountToUpdate.Name = Campaign.Name;
                if (Campaign.NumberOfUses != null && Campaign.NumberOfUses != 0)
                {
                    DiscountToUpdate.NumberOfUses = Campaign.NumberOfUses;
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(Campaign Campaign)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Campaigns.Remove(Campaign);
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

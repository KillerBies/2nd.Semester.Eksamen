using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories.BookingRepositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public TreatmentRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<Treatment?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Treatments.FirstOrDefaultAsync(t => t.Guid == guid);
        }
        public async Task<Treatment> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Treatments.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<IEnumerable<Treatment>> GetAllAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.Treatments.ToListAsync();
        }
        public async Task<IEnumerable<Treatment>> GetByFilterAsync(Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task CreateNewAsync(Treatment treatment)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                treatment.Guid = Guid.NewGuid();
                await _context.Treatments.AddAsync(treatment);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task UpdateAsync(Treatment treatment)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var treatmentToUpdate = await _context.Treatments.FindAsync(treatment.Id);
                treatmentToUpdate.TrySetDuration(treatment.Duration);
                treatmentToUpdate.TryChangePrice(treatment.Price);
                treatmentToUpdate.TryChangeName(treatment.Name);
                treatmentToUpdate.Description = treatment.Description;
                treatmentToUpdate.RequiredSpecialties = treatment.RequiredSpecialties;
                treatmentToUpdate.Category = treatment.Category;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task DeleteAsync(Treatment treatment)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.Treatments.Remove(treatment);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<IEnumerable<Treatment>> GetByCategory(string category)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Treatments.Where(t => t.Category == category).ToListAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                if(await _context.BookedTreatments.AnyAsync(tb=>tb.TreatmentId == id && tb.Booking.Status == Domain.Entities.Products.BookingProducts.BookingStatus.Pending))
                {
                    throw new Exception("Cannot delete Treatment connected to pending bookings");
                }
                var treatment = await _context.Treatments.FirstOrDefaultAsync(c => c.Id == id);

                if (treatment != null)
                {
                    _context.Treatments.Remove(treatment);
                    await _context.SaveChangesAsync();
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }


        }

        public async Task<List<string>> GetAllSpecialtiesAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.Treatments.Select(t => string.Join(',',t.RequiredSpecialties)).ToListAsync();
        }

        public async Task<int> GetNumberOfPending()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.CountAsync();
        }
        public async Task<int> GetNumberOfComplete()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.TreatmentSnapshots.CountAsync();
        }
    }
}

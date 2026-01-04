using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories.BookingRepositories
{
    public class TreatmentBookingRepository : ITreatmentBookingRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public TreatmentBookingRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<TreatmentBooking?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.FirstOrDefaultAsync(b => b.Guid == guid);
        }
        public async Task<List<TreatmentBooking>?> GetByTreatmentGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.Where(b => b.Treatment.Guid == guid).ToListAsync();
        }
        public async Task<List<TreatmentBooking>?> GetByEmployeeGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments
                .Where(bt => bt.Employee.Guid == guid)
                .Include(o => o.Treatment)
                .Include(o => o.Employee)
                .Include(o=>o.Booking)
                .ToListAsync();
        }
        public async Task BookTreatmentAsync(TreatmentBooking treatmentBooking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                treatmentBooking.Guid = Guid.NewGuid();
                if (await TreatmentBookingOverlapsAsync(treatmentBooking)) throw new Exception();
                await _context.BookedTreatments.AddAsync(treatmentBooking);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task CancleBookedTreatmentAsync(TreatmentBooking treatmentBooking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.BookedTreatments.Remove(treatmentBooking);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<TreatmentBooking> GetByIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.FindAsync(id);
        }
        public async Task<IEnumerable<TreatmentBooking>> GetAllAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.ToListAsync();
        }
        public async Task<IEnumerable<TreatmentBooking>> GetByFilterAsync(Domain.Filter filter)
        {
            var _context = await _factory.CreateDbContextAsync();
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(TreatmentBooking treatmentBooking)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                _context.BookedTreatments.Update(treatmentBooking);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<IEnumerable<TreatmentBooking>> GetByEmployeeIDAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.Where(b => b.Employee.Id == id).ToListAsync();
        }
        public async Task<bool> TreatmentBookingOverlapsAsync(TreatmentBooking TreatmentBooking)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.AnyAsync(bt=>bt.Employee.Id == TreatmentBooking.Employee.Id && bt.Overlaps(TreatmentBooking.Start,TreatmentBooking.End));
        }
        public async Task DeleteAsync(TreatmentBooking treatmentBooking)
        {
            throw new NotImplementedException();
        }


    }
}

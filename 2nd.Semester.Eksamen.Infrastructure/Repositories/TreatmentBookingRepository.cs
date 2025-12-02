using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class TreatmentBookingRepository : ITreatmentBookingRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public TreatmentBookingRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task CreateNewAsync(TreatmentBooking treatmentBooking)
        {
            await using var _context = await _factory.CreateDbContextAsync();
            await _context.BookedTreatments.AddAsync(treatmentBooking);
        }
        public async Task DeleteAsync(TreatmentBooking treatmentBooking)
        {
            
            await using var _context = await _factory.CreateDbContextAsync();
            _context.BookedTreatments.Remove(treatmentBooking);
            await _context.SaveChangesAsync();
        }
        public async Task<TreatmentBooking> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TreatmentBooking>> GetAllAsync()
        {
            await using var _context = await _factory.CreateDbContextAsync();
            return await _context.BookedTreatments.ToListAsync();
        }
        public async Task<IEnumerable<TreatmentBooking>> GetByFilterAsync(Domain.Filter filter)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(TreatmentBooking treatmentBooking)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TreatmentBooking>> GetByEmployeeIDAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

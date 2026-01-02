using _2nd.Semester.Eksamen.Domain.Entities.History;
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
    public class HistorySnapShotRepository : IHistorySnapShotRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public HistorySnapShotRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }
        public async Task<ProductSnapshot?> GetProductSnapshotByIdAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.ProductSnapshots
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<TreatmentSnapshot?> GetTreatmentSnapShotByIdAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.TreatmentSnapshots
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<CustomerSnapshot?> GetCustomerSnapShotByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.CustomerSnapshots.FirstOrDefaultAsync(c => c.Guid == guid);
        }
        public async Task<BookingSnapshot?> GetBookingSnapShotByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.BookingsSnapshots.FirstOrDefaultAsync(c => c.Guid == guid);
        }
        public async Task<TreatmentSnapshot?> GetTreatmentSnapShotByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.TreatmentSnapshots.FirstOrDefaultAsync(c => c.Guid == guid);
        }
        public async Task<AppliedDiscountSnapshot?> GetDiscountSnapShotByGuid(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.AppliedDiscountSnapshots.FirstOrDefaultAsync(c => c.Guid == guid);
        }
        public async Task<TreatmentSnapshot?> GetEmployeeSnapShotByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.TreatmentSnapshots.FirstOrDefaultAsync(ts => ts.EmployeeGuid == guid);
        }
        public async Task<ProductSnapshot?> GetProductSnapshotByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.ProductSnapshots.FirstOrDefaultAsync(ps => ps.Guid == guid);
        }
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.InvoiceRepositories
{
    public class SnapshotRepository : ISnapshotRepository
    {

        private readonly IDbContextFactory<AppDbContext> _factory;

        public SnapshotRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }


        public async Task CreateNewAsync(OrderSnapshot orderSnapshot)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.OrderSnapshots.AddAsync(orderSnapshot);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    
        public async Task <List<OrderSnapshot>> GetAllOrderSnapshotsAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot).ThenInclude(tb => tb.EmployeeSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                    .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot).ToListAsync();

        }
        public async Task<OrderSnapshot?> GetByIdAsync(int id)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot) 
                .Include(o => o.OrderLinesSnapshot)
                    .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<OrderSnapshot?> GetByGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                    .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .FirstOrDefaultAsync(o => o.Guid == guid);
        }
        public async Task<IEnumerable<OrderSnapshot>> GetByProduct(string ProductName)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Where(o => o.OrderLinesSnapshot.Any(p => p.ProductSnapshot.Name == ProductName))
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderSnapshot?>> GetAllBookingSnapShotsAsync()
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Include(o=>o.BookingSnapshot).ThenInclude(b => b.CustomerSnapshot).ThenInclude(c => c.AddressSnapshot).Include(o => o.BookingSnapshot).ThenInclude(b => b.TreatmentSnapshot).ToListAsync();
        }

        public async Task<IEnumerable<OrderSnapshot>?> GetByProductGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Where(o => o.OrderLinesSnapshot.Any(p => p.ProductSnapshot.Guid == guid))
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderSnapshot>?> GetByCustomerGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            string phonenumber = "";
            var customer = await _context.CustomerSnapshots.FirstOrDefaultAsync(c => c.Guid == guid);
            if (customer != null)
            {
                phonenumber = customer.PhoneNumber;
            }
            return await _context.OrderSnapshots.Where(o => o.BookingSnapshot.CustomerSnapshot.Guid == guid ||  o.BookingSnapshot.CustomerSnapshot.PhoneNumber == phonenumber)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderSnapshot>?> GetByTreatmentGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Where(o => o.BookingSnapshot.TreatmentSnapshot.Any(tb => tb.Guid == guid))
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderSnapshot>?> GetByDiscountGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Where(o => o.AppliedDiscountSnapshot.Guid == guid)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderSnapshot>?> GetByEmployeeGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots.Where(o => o.BookingSnapshot.TreatmentSnapshot.Any(t=>t.EmployeeGuid == guid))
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .ToListAsync();
        }
        public async Task<OrderSnapshot?> GetByBookingGuidAsync(Guid guid)
        {
            var _context = await _factory.CreateDbContextAsync();
            return await _context.OrderSnapshots
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.CustomerSnapshot)
                        .ThenInclude(c => c.AddressSnapshot)
                .Include(o => o.BookingSnapshot)
                    .ThenInclude(b => b.TreatmentSnapshot)
                .Include(o => o.OrderLinesSnapshot)
                .ThenInclude(ol => ol.ProductSnapshot)
                .Include(o => o.AppliedDiscountSnapshot)
                .FirstOrDefaultAsync(o => o.BookingSnapshot.Guid == guid);
        }
    }
}

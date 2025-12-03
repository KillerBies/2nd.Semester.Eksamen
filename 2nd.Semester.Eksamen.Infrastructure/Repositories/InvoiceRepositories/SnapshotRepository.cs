using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.InvoiceRepositories
{
    public class SnapshotRepository : ISnapshotRepository
    {

        private readonly IDbContextFactory<AppDbContext> _factory;

        public SnapshotRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }


        public async Task CreateNewAsync(Order order)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.//.AddAsync(order);
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

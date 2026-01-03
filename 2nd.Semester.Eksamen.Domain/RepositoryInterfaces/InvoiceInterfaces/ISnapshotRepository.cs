using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces
{
    public interface ISnapshotRepository
    {
        public  Task CreateNewAsync(OrderSnapshot orderSnapshot);

        public Task<List<OrderSnapshot>>? GetAllOrderSnapshotsAsync();
        public Task<OrderSnapshot?> GetByIdAsync(int id);
        public Task<OrderSnapshot?> GetByGuidAsync(Guid guid);
        public Task<IEnumerable<OrderSnapshot>> GetByProduct(string ProductName);
        public Task<IEnumerable<OrderSnapshot?>> GetAllBookingSnapShotsAsync();
        public Task<IEnumerable<OrderSnapshot>?> GetByProductGuidAsync(Guid guid);
        public Task<IEnumerable<OrderSnapshot>?> GetByCustomerGuidAsync(Guid guid);
        public Task<IEnumerable<OrderSnapshot>?> GetByTreatmentGuidAsync(Guid guid);
        public Task<IEnumerable<OrderSnapshot>?> GetByDiscountGuidAsync(Guid guid);
        public Task<IEnumerable<OrderSnapshot>?> GetByEmployeeGuidAsync(Guid guid);
        public Task<OrderSnapshot?> GetByBookingGuidAsync(Guid guid);
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces
{
    public interface IHistorySnapShotRepository
    {
        public Task<ProductSnapshot?> GetProductSnapshotByIdAsync(int id);
        public Task<TreatmentSnapshot?> GetTreatmentSnapShotByIdAsync(int id);
        public Task<CustomerSnapshot?> GetCustomerSnapShotByGuidAsync(Guid guid);
        public Task<BookingSnapshot?> GetBookingSnapShotByGuidAsync(Guid guid);
        public Task<TreatmentSnapshot?> GetTreatmentSnapShotByGuidAsync(Guid guid);
    }
}

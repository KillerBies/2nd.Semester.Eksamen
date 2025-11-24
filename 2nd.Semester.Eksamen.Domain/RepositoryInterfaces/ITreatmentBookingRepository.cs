using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface ITreatmentBookingRepository
    {
        //Repo for booked treatments. Used to get info about booked treatments (not treatment data)
        public Task<TreatmentBooking> GetByIDAsync(int id);
        public Task<IEnumerable<TreatmentBooking>> GetByEmployeeIDAsync(int id);
        public Task<IEnumerable<TreatmentBooking>> GetAllAsync();
        public Task<IEnumerable<TreatmentBooking>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(TreatmentBooking TreatmentBooking);
        public Task UpdateAsync(TreatmentBooking TreatmentBooking);
        public Task DeleteAsync(TreatmentBooking TreatmentBooking);
    }
}

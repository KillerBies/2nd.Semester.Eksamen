using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces
{
    public interface ITreatmentBookingRepository
    {
        //Repo for booked treatments. Used to get info about booked treatments (not treatment data)
        public Task<TreatmentBooking> GetByIDAsync(int id);
        public Task<IEnumerable<TreatmentBooking>> GetByEmployeeIDAsync(int id);
        public Task<IEnumerable<TreatmentBooking>> GetAllAsync();
        public Task<IEnumerable<TreatmentBooking>> GetByFilterAsync(Filter filter);
        public Task BookTreatmentAsync(TreatmentBooking TreatmentBooking);
        public Task UpdateAsync(TreatmentBooking TreatmentBooking);
        public Task CancleBookedTreatmentAsync(TreatmentBooking TreatmentBooking);
        public Task<bool> TreatmentBookingOverlapsAsync(TreatmentBooking TreatmentBooking);
        public Task DeleteAsync(TreatmentBooking treatmentBooking);
    }
}

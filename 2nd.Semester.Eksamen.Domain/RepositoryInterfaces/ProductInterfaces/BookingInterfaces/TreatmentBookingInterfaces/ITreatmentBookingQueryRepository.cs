using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces.TreatmentBookingInterfaces
{
    public interface ITreatmentBookingQueryRepository
    {
        public Task<ICollection<TreatmentBooking>> GetForEmployeeAsync(Guid employeeId, DateTime from, DateTime to);
    }
}

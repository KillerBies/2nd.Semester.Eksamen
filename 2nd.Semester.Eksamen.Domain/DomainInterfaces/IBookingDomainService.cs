using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces
{
    public interface IBookingDomainService
    {
        public Task<bool> IsBookingOverlappingAsync(Booking booking);
    }
}

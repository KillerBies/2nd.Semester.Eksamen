using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces
{
    public interface IBookingDomainService
    {
        public Task<bool> IsCustomerBookingOverlappingAsync(int customerId, DateTime bookingStart, DateTime bookingEnd);
        public Task<bool> IsEmployeeBookingOverlapping(int employeeId, DateTime start, DateTime end);
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface IBookingOverviewService
    {
        public Task<List<BookingSnapshot>> GetAllCompletedBookings();
    }
}

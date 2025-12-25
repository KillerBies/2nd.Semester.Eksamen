using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class BookingOverviewService : IBookingOverviewService
    {
        private ISnapshotRepository _snapshotRepository { get; set; }
        public BookingOverviewService(ISnapshotRepository snapshotRepository)
        {
            _snapshotRepository = snapshotRepository;
        }
        public async Task<List<BookingSnapshot>> GetAllCompletedBookings()
        {
            return (await _snapshotRepository.GetAllBookingSnapShotsAsync()).ToList();
        }
    }
}

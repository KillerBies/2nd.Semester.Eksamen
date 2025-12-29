using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
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
        private IBookingRepository _bookingRepository { get; set; }
        public BookingOverviewService(ISnapshotRepository snapshotRepository, IBookingRepository bookingRepository)
        {
            _snapshotRepository = snapshotRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<List<BookingSnapshot>> GetAllCompletedBookings()
        {
            return (await _snapshotRepository.GetAllBookingSnapShotsAsync()).ToList();
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            return new BookingDTO(await _bookingRepository.GetByIDAsync(id));
        }

        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            return (await _bookingRepository.GetAllAsync()).Select(b => new BookingDTO(b)).ToList();
        }
        public async Task CreateBookingAsync(BookingDTO booking)
        {
            try
            {
                List<TreatmentBooking> treatments = booking.TreatmentBookingDTOs.Select(tb => new TreatmentBooking(new Treatment() { Id = tb.Treatment.TreatmentId }, new Employee() { Id = tb.Employee.EmployeeId }, booking.Start, booking.End)).ToList();
                await _bookingRepository.CreateNewBookingAsync(new Booking() { CustomerId = booking.CustomerId, Treatments = treatments, Start = booking.Start, End = booking.End, Duration = booking.Duration });
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task UpdateBookingAsync(BookingDTO booking)
        {
            try
            {
                List<TreatmentBooking> treatments = booking.TreatmentBookingDTOs.Select(tb => new TreatmentBooking(new Treatment() { Id = tb.Treatment.TreatmentId }, new Employee() { Id = tb.Employee.EmployeeId }, booking.Start, booking.End)).ToList();
                await _bookingRepository.UpdateAsync(new Booking() { CustomerId = booking.CustomerId, Treatments = treatments, Start = booking.Start, End = booking.End, Duration = booking.Duration,Id=booking.BookingId});
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task DeleteBookingAsync(BookingDTO booking)
        {
            try
            {
                await _bookingRepository.CancelBookingByIdAsync(booking.BookingId);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}

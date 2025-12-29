using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
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
        public Task<BookingDTO> GetBookingByIdAsync(int id);
        public Task<List<BookingDTO>> GetAllBookingsAsync();
        public Task CreateBookingAsync(BookingDTO booking);
        public Task UpdateBookingAsync(BookingDTO booking);
        public Task DeleteBookingAsync(BookingDTO booking);
    }
}

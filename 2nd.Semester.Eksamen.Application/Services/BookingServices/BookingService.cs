using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class BookingService : IBookingService
    {


        private readonly IBookingRepository _bookingRepository;
        private readonly IPrivateCustomerRepository _privateCustomerRepository;
        private readonly ICompanyCustomerRepository _companyCustomerRepository;
        public BookingService(IBookingRepository bookingRepository, IPrivateCustomerRepository privateCustomerRepository, ICompanyCustomerRepository companyCustomerRepository)
        {
            _bookingRepository = bookingRepository;
            _privateCustomerRepository = privateCustomerRepository;
            _companyCustomerRepository = companyCustomerRepository;
        }




        public async Task<IEnumerable<Booking>> GetBookingsByFilterAsync(Filter filter)
        {

            return await _bookingRepository.GetByFilterAsync(filter);

        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            
                var customer = booking.Customer;
                //Checks if Customer has chosen to be deleted from database. If false sendes to infrastructure layer and deletes customer.
                if (booking.Customer.SaveAsCustomer == false && customer is PrivateCustomer privateCustomer)
                {
                    await _privateCustomerRepository.DeleteAsync(privateCustomer);
                }
                if (booking.Customer.SaveAsCustomer == false && customer is CompanyCustomer companyCustomer)
                {
                    await _companyCustomerRepository.DeleteAsync(companyCustomer);
                }
                //Deletes customer
                await _bookingRepository.CancelBookingAsync(booking);
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _bookingRepository.GetByIDAsync(id);
        }

        public async Task UpdateAsync(Booking booking)
        {
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task SetBookingStatusAsync(int bookingId, BookingStatus newStatus)
        {
            var booking = await _bookingRepository.GetByIDAsync(bookingId);

            if (booking == null)
                throw new KeyNotFoundException($"Booking with id {bookingId} not found");

            booking.Status = newStatus;
            await _bookingRepository.UpdateAsync(booking);
        }

    }
}

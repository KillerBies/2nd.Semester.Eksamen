using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Exceptions;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces.TreatmentBookingInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService
{
    public class BookingFactoryService : IBookingFactoryService
    {
        private readonly IBookingQueryRepository _bookingQueryRepository;
        private readonly ITreatmentBookingQueryRepository _treatmentBookingRepository;
        private readonly IBookingRepository _bookingRepository;
        public BookingFactoryService(IBookingQueryRepository bookingQueryRepository, ITreatmentBookingQueryRepository treatmentBookingRepository, IBookingRepository bookingRepository)
        {
            _treatmentBookingRepository = treatmentBookingRepository;
            _bookingQueryRepository = bookingQueryRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task CreateBookingAsync(Guid customerId, DateTime start, DateTime end, List<TreatmentBooking> treatments)
        {
            var booking = new Booking(customerId, start, end, treatments);
            //Check customer double booking
            if(await CustomerIsDoubleBooked(booking))
                throw new BookingOverlapException("Denne booking overlapper");
            //check employee double booking
            if(await EmployeeIsDoubleBooked(treatments))
                throw new BookingOverlapException("Denne booking overlapper");
            //insert booking
            await _bookingRepository.CreateNewBookingAsync(booking);
        }
        private async Task<bool> CustomerIsDoubleBooked(Booking booking)
        {
            return (await _bookingQueryRepository.GetForCustomerAsync(booking.Id, booking.Start, booking.End)).Any();
        }
        private async Task<bool> EmployeeIsDoubleBooked(IReadOnlyCollection<TreatmentBooking> treatments)
        {
            foreach(var treatment in treatments)
            {
                if ((await _bookingQueryRepository.GetForEmployeeAsync(treatment.Employee.Id, treatment.Start, treatment.End)).Any())
                    return true;
            }
            return false;
        }
    }
}

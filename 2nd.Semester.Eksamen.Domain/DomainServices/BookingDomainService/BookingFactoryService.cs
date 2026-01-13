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
        private readonly IBookingQueryRepository _bookingRepository;
        private readonly ITreatmentBookingQueryRepository _treatmentBookingRepository;
        public BookingFactoryService(IBookingQueryRepository bookingRepository, ITreatmentBookingQueryRepository treatmentBookingRepository)
        {
            _treatmentBookingRepository = treatmentBookingRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task CreateBookingAsync(Guid customerId, DateTime start, DateTime end, IReadOnlyCollection<TreatmentBooking> treatments)
        {
            var booking = new Booking(customerId,start,end,(List<TreatmentBooking>)treatments)
            if(await CustomerIsDoubleBooked(Booking booking))
                throw new BookingOverlapException("Denne booking overlapper");
            if(await EmployeeIsDoubleBooked(treatments))
                throw new BookingOverlapException("Denne booking overlapper");

        }
        private async Task<bool> CustomerIsDoubleBooked(Booking booking)
        {
            return (await _bookingRepository.GetForCustomerAsync(booking.Guid, booking.Start, booking.End)).Any();
        }
        private async Task<bool> EmployeeIsDoubleBooked(IReadOnlyCollection<TreatmentBooking> treatments)
        {
            foreach(var treatment in treatments)
            {
                if ((await _treatmentBookingRepository.GetForEmployeeAsync(treatment.Employee.Guid, treatment.Start, treatment.End)).Any())
                    return true;
            }
            return false;
        }
    }
}

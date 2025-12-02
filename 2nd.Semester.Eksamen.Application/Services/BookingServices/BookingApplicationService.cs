using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class BookingApplicationService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingDomainService _bookingDomainService;
        private readonly ScheduleService _scheduleService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly DTO_to_Domain _toDomainAdapter;
        public BookingApplicationService(DTO_to_Domain toDomainAdapter, IScheduleRepository scheduleRepository,ScheduleService scheduleService, IBookingRepository bookingRepository, IBookingDomainService bookingDomainService, ITreatmentBookingRepository treatmentBookingRepository, ICompanyCustomerRepository companyCustomerRepo)
        {
            _bookingRepository = bookingRepository;
            _bookingDomainService = bookingDomainService;
            _scheduleService = scheduleService;
            _scheduleRepository = scheduleRepository;
            _toDomainAdapter = toDomainAdapter;
        }

        //use the same dbcontext for overlap checks
        public async Task CreateBookingAsync(BookingDTO booking)
        {
            if (booking.TreatmentBookingDTOs == null || !booking.TreatmentBookingDTOs.Any())
                throw new ArgumentException("Booking must contain at least one treatment.");
            Booking Booking = await _toDomainAdapter.DTOBookingToDomain(booking);
            try
            {
                await _bookingRepository.CreateNewBookingAsync(Booking);
                await Task.WhenAll(Booking.Treatments.Select(t => _scheduleRepository.BookScheduleAsync(t)));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Something went wrong while creating booking", ex);
            }
        }
        public async Task CancelBookingAsync()
        {
            throw new NotImplementedException();
        }
        public async Task RescheduleBookingAsync()
        {
            throw new NotImplementedException();
        }
    }
}

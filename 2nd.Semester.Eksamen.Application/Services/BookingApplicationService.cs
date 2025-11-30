using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.DomainServices;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Services
{
    public class BookingApplicationService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingDomainService _bookingDomainService;
        private readonly ScheduleService _scheduleService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly DTO_to_Domain _toDomainAdapter = new DTO_to_Domain();
        public BookingApplicationService(IScheduleRepository scheduleRepository,ScheduleService scheduleService, IBookingRepository bookingRepository, IBookingDomainService bookingDomainService, ITreatmentBookingRepository treatmentBookingRepository, ICompanyCustomerRepository companyCustomerRepo)
        {
            _bookingRepository = bookingRepository;
            _bookingDomainService = bookingDomainService;
            _scheduleService = scheduleService;
            _scheduleRepository = scheduleRepository;
        }
        public async Task CreateBookingAsync(BookingDTO booking)
        {
            if (booking.TreatmentBookingDTOs == null || !booking.TreatmentBookingDTOs.Any())
                throw new ArgumentException("Booking must contain at least one treatment.");
            Booking Booking = await _toDomainAdapter.DTOBookingToDomain(booking);
            try
            {
                await _bookingRepository.CreateNewBookingAsync(Booking);
                Booking.Treatments.ForEach(t => _scheduleRepository.BookScheduleAsync(t));
            }
            catch (Exception)
            {
                throw new ArgumentException("Something went wrong");
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

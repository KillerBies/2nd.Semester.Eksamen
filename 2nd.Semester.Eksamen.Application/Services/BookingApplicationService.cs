using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
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
        private readonly DTO_to_Domain _toDomainAdapter = new DTO_to_Domain();
        public BookingApplicationService(ScheduleService scheduleService, IBookingRepository bookingRepository, IBookingDomainService bookingDomainService, ITreatmentBookingRepository treatmentBookingRepository, ICompanyCustomerRepository companyCustomerRepo)
        {
            _bookingRepository = bookingRepository;
            _bookingDomainService = bookingDomainService;
            _scheduleService = scheduleService;
        }
        public async Task CreateBookingAsync(BookingDTO booking)
        {
            if (booking.TreatmentBookingDTOs == null || !booking.TreatmentBookingDTOs.Any())
                throw new ArgumentException("Booking must contain at least one treatment.");
            if ((await IsOverlapping(booking)))
                throw new ArgumentException("Booking overlaps with others");
            var tasks = booking.TreatmentBookingDTOs.Select(t => _scheduleService.BookSchedule(t.Employee.EmployeeId, t.Start, t.End));
            await Task.WhenAll(tasks);
            await _bookingRepository.CreateNewAsync(_toDomainAdapter.DTOBookingToDomain(booking));
        }
        private async Task<bool> IsOverlapping(BookingDTO booking)
        {
            var tasks = booking.TreatmentBookingDTOs.Select(tb => _bookingDomainService.IsEmployeeBookingOverlapping(tb.Employee.EmployeeId,tb.Start,tb.End));
            bool[] results = await Task.WhenAll(tasks);
            return results.Any(r => r);
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

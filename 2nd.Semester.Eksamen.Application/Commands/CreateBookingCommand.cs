using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands
{
    public class CreateBookingCommand
    {
        private readonly IBookingDomainService _bookingDomainService;
        private readonly DTO_to_Domain ToDomainAdapter = new();
        private readonly IBookingRepository _bookingRepository
        public CreateBookingCommand(IBookingDomainService bookingDomainService, IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingDomainService = bookingDomainService;
        }
        public async Task<bool> CreateBooking(BookingDTO booking)
        {
            Booking DomainBooking = ToDomainAdapter.DTOBookingToDomain(booking);
            if (await IsOverlapping(DomainBooking)) return false;
            DomainBooking.Treatments.Select(t => t.Employee.Schedule.BookTreatmentOnDate(t));
            _bookingRepository.CreateNewAsync(DomainBooking);
            return true;
            //book schedual
            //Creat booking in db

        }
        private async Task<bool> IsOverlapping(Booking booking)
        {
            var tasks = booking.Treatments.Select(tb => _bookingDomainService.IsBookingOverlappingAsync(booking));
            bool[] results = await Task.WhenAll(tasks);
            return results.Any(r => r);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;

namespace _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService
{
    public class BookingDomainService : IBookingDomainService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITreatmentBookingRepository _treatmentBookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public BookingDomainService(IBookingRepository bookingRepository, ITreatmentBookingRepository treatmentBookingRepository, IEmployeeRepository employeeRepository)
        {
            _treatmentBookingRepository = treatmentBookingRepository;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<bool> IsCustomerBookingOverlappingAsync(int customerId, DateTime bookingStart, DateTime bookingEnd)
        {
            var bookings = await _bookingRepository.GetByCustomerId(customerId);
            if (!bookings.Any(b => b.Overlaps(bookingStart, bookingEnd))) return true;
            return false;
        }
        public async Task<bool> IsEmployeeBookingOverlapping(int employeeId, DateTime start, DateTime end)
        {
            var Employee = await _employeeRepository.GetByIDAsync(employeeId);
            if (Employee.Appointments.Any(tb => tb.Overlaps(start, end))) return true;
            return false;
        }
    }
}

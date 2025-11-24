using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO;
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
        public BookingApplicationService(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, ITreatmentBookingRepository treatmentBookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task CreateBookingAsync(CreateBookingCommand cmd)
        {
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

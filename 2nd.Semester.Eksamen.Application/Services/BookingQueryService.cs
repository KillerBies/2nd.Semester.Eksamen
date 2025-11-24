using _2nd.Semester.Eksamen.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;

namespace _2nd.Semester.Eksamen.Application.Services
{
    public class BookingQueryService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITreatmentBookingRepository _treatmentBookingRepository;
        public BookingQueryService(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, ITreatmentBookingRepository treatmentBookingRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _treatmentBookingRepository = treatmentBookingRepository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var Employees = await _employeeRepository.GetAllAsync();
            var employeeDTOs = Employees.Select(e => new EmployeeDTO
            {
                EmployeeId = e.Id,
                Name = e.Name,
                ExperienceLevel = e.ExperienceLevel,
                BasePriceMultiplier = e.BasePriceMultiplier,
                Specialization = e.Specialty
            });
            return employeeDTOs;
        }
        public async Task<IEnumerable<TreatmentDTO>> GetAllTreatmentsAsync()
        {
            var treatments = await _treatmentRepository.GetAllAsync();
            var treatmentDTOs = treatments.Select(t => new TreatmentDTO
            {
                TreatmentId = t.Id,
                Name = t.Name,
                Category = t.Category,
                Duration = t.Duration,
                BasePrice = t.Price,
            });
            return treatmentDTOs;
        }


    }
}

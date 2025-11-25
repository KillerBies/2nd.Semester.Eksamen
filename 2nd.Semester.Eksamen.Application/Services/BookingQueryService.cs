using _2nd.Semester.Eksamen.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<IEnumerable<AvailableBookingSpotDTO>> GetAvailableSpotsAsync(BookingDTO Booking,DateTime MinDate=default)
        {
            if (MinDate == default) MinDate = DateTime.Now;
            List<AvailableBookingSpotDTO> spots = new();
            EmployeeDTO RootEmp = Booking.TreatmentBookingDTOs.Select(t=>t.Employee).FirstOrDefault(e => e.EmployeeId != 0);
            if(RootEmp==null)
            {
                while(RootEmp == null)
                {

                }
                /*
                 find an employee available tomorrow and start from there
                if there are non then try again
                 */
            }

            return await;
            throw new NotImplementedException();
        }

        /*
        Input minimum datetime (normalt sat til dagen efter datetime now)
        Gør følgende indtil booking spot listen har en count på 30
        BookingDTO = new();
        Hvis en medarbejder er valgt
            Find deres første åbne spot efter kl08:00
            Udregn hvornår behandlingen slutter og se om hele bookingen kan passe ind og slutte før kl:20:00
            Hvis den ikke kan søg efter deres næste tomme spot og check igen
            Når et spot er fundet
            Foreach(treatment in otherTreatments)
                found=false
                While(found==false)
                    Brug start og end til at tjekke alle medarbejderes apointments for et spot available()
                    Hvis et spot findes add dem til treatment booking dtoen i booking dto objektet
                        found=true;
            hvis et spot findes til alle treatments
            Add booking spottet til listen af spots.

            
                
         */



    }
}

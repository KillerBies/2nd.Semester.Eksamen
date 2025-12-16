using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.BookingSchedules;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class BookingQueryService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITreatmentBookingRepository _treatmentBookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingDomainService _bookingDomainService;
        private readonly DTO_to_Domain ToDomainAdapter;
        private readonly ISuggestionService _suggestionService;
        private readonly Domain_to_DTO ToDTOAdapter;
        public BookingQueryService(Domain_to_DTO domain_To_DTO ,ICustomerRepository customerRepository,DTO_to_Domain dtoToDomain, IBookingDomainService bookingDomainService, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, ITreatmentBookingRepository treatmentBookingRepository, ISuggestionService suggestionService)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _treatmentBookingRepository = treatmentBookingRepository;
            _suggestionService = suggestionService;
            ToDomainAdapter = dtoToDomain;
            _bookingDomainService = bookingDomainService;
            _customerRepository = customerRepository;
            ToDTOAdapter = domain_To_DTO;
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
                Specialties = e.Specialties
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
        public async Task<List<BookingDTO>> GetBookingSuggestionsAsync(List<TreatmentBookingDTO> treatments, DateOnly startdate, int numberOfDaysToCheck, int numberOfSuggestions, int interval)
        {

            List<TreatmentBooking> TreatmentBookings = new();

            if (treatments != null)
            {
                foreach (var treatment in treatments)
                {
                    if (treatment != null)
                    {
                        var domainTreatment = await ToDomainAdapter.DTOTreatmentBookingToDomain(treatment);
                        if (domainTreatment != null)
                            TreatmentBookings.Add(domainTreatment);
                    }
                }
            }
            List<BookingSuggestion> suggestions = await _suggestionService.GetBookingSugestions(TreatmentBookings, startdate, numberOfDaysToCheck, numberOfSuggestions, interval);
            List<BookingDTO> bookingDTOs = suggestions.Select(s => new BookingDTO
            {
                Start = s.Start,
                End = s.End,
                TreatmentBookingDTOs = s.Items.Select(i => new TreatmentBookingDTO
                {
                    Treatment = new TreatmentDTO
                    {
                        TreatmentId = i.Treatment.Id,
                        Name = i.Treatment.Name,
                        Category = i.Treatment.Category,
                        Duration = i.Treatment.Duration,
                        BasePrice = i.Treatment.Price
                    },
                    Employee = new EmployeeDTO
                    {
                        EmployeeId = i.Employee.Id,
                        Name = i.Employee.Name,
                        ExperienceLevel = i.Employee.ExperienceLevel,
                        BasePriceMultiplier = i.Employee.BasePriceMultiplier,
                        Specialties = i.Employee.Specialties
                    },
                    Start = i.Start,
                    End = i.End,
                }).ToList()
            }).ToList();
            return bookingDTOs;
        }
        public async Task<List<TreatmentBookingDTO>> ArangeTreatments(BookingDTO booking)
        {
            var arranged = new List<TreatmentBookingDTO>();
            var remaining = booking.TreatmentBookingDTOs.ToList();
            var start = booking.Start;

            while (remaining.Any())
            {
                bool scheduledAny = false;

                for (int i = 0; i < remaining.Count; i++)
                {
                    var treatment = remaining[i];
                    bool isOverlapping = await _bookingDomainService.IsEmployeeBookingOverlapping(
                        treatment.Employee.EmployeeId, start, start.Add(treatment.Treatment.Duration));

                    if (!isOverlapping)
                    {
                        treatment.Start = start;
                        treatment.End = start.Add(treatment.Treatment.Duration);
                        arranged.Add(treatment);
                        start = treatment.End;
                        remaining.RemoveAt(i);
                        scheduledAny = true;
                        break; // exit for-loop and start from updated start time
                    }
                }
            }
            return arranged;
        }
        public async Task<CustomerDTO?> SearchPhoneNumber(string phoneNumber)
        {
            var customer = await _customerRepository.GetByPhoneNumberAsync(phoneNumber);
            if (customer != null) return ToDTOAdapter.CustomerToDTO(customer);
            return null;

        }
        public async Task<CustomerDTO?> GetCustomerByIDAsync(int id)
        {
            return ToDTOAdapter.CustomerToDTO(await _customerRepository.GetByIDAsync(id));
        }


        public async Task<List<BookingDTO>> GetUpcomingBookingsAsync()
        {
            return (await _bookingRepository.GetAllAsync()).Take(10).Select(b => ToDTOAdapter.BookingToDTO(b)).ToList();
        }
    }
}

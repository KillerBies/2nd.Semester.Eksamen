using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using System.Runtime.CompilerServices;
using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class BookingFormService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITreatmentBookingRepository _treatmentBookingRepository;
        private readonly Domain_to_DTO ToDtoAdapter;
        private readonly ICustomerRepository _customerRepository;
        public BookingFormService(ICustomerRepository customerRepository, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, ITreatmentBookingRepository treatmentBookingRepository, Domain_to_DTO _To_DTO)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _treatmentBookingRepository = treatmentBookingRepository;
            ToDtoAdapter = _To_DTO;
            _customerRepository = customerRepository;
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
        //public async Task CreateBookingAsync(BookingDTO bookingDTO)
        //{
        //}
        //public async Task<IEnumerable<TreatmentBokingDTO>> GetAllTreatmentBookingsAsync()
        //{
        //    var bookings = await _bookingRepository.GetAllAsync();
        //    var bookingDTOs = bookings.Select(b => new BookingDTO
        //    {

        //    });
        //    return bookingDTOs;
        //}


        // Searches through customers using phonenumber. Returns customer with that phonenumber. If none found, returns null.
        //public async Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber)
        //{
        //    var privateCustomer = await _priCustomerRepository.GetByPhoneAsync(phoneNumber);
        //    if (privateCustomer != null)
        //    {
        //        return privateCustomer;
        //    }
        //    var companyCustomer = await _comCustomerRepository.GetByPhoneAsync(phoneNumber);
        //    if (companyCustomer != null)
        //    {
        //        return companyCustomer;
        //    }
        //    return null;
        //}
        public async Task<CustomerDTO?> GetCustomerByIDAsync(int id)
        {
            return ToDtoAdapter.CustomerToDTO(await _customerRepository.GetByIDAsync(id));
        }
    }
}
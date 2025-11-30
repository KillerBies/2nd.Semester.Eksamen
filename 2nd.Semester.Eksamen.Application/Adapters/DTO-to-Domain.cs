using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Adapters
{
    public class DTO_to_Domain
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPrivateCustomerRepository _privateCustomerRepository;
        private readonly ICompanyCustomerRepository _companyCustomerRepository;
        private readonly ICustomerRepository _customerRepository;
        public DTO_to_Domain(ICustomerRepository customerRepository,IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, IPrivateCustomerRepository privateCustomerRepository, ICompanyCustomerRepository companyCustomerRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _companyCustomerRepository = companyCustomerRepository;
            _privateCustomerRepository = privateCustomerRepository;
            _customerRepository = customerRepository;

        }
        public async Task<Treatment> DTOTreatmentToDomain(TreatmentDTO treatmentDTO)
        {
            return await _treatmentRepository.GetByIDAsync(treatmentDTO.TreatmentId);
        }
        public async Task<Employee> DTOEmployeeToDomain(EmployeeDTO employeeDTO)
        {
            return await _employeeRepository.GetByIDAsync(employeeDTO.EmployeeId);
        }
        public async Task<Booking> DTOBookingToDomain(BookingDTO booking)
        {
            List<TreatmentBooking> treatments = new();
            foreach(var treatment in booking.TreatmentBookingDTOs)
            {
                treatments.Add(await DTOTreatmentBookingToDomain(treatment));
            }
            var customer = await DTOCustomerToDomain(booking.CustomerId);
            return new Booking(customer, booking.Start, booking.End, treatments);
        }
        public async Task<TreatmentBooking> DTOTreatmentBookingToDomain(TreatmentBookingDTO treatmentBookingDTO)
        {
            var treatment = await DTOTreatmentToDomain(treatmentBookingDTO.Treatment);
            var employee = await DTOEmployeeToDomain(treatmentBookingDTO.Employee);
            return new TreatmentBooking(treatment, employee, treatmentBookingDTO.Start, treatmentBookingDTO.End);
        }
        public async Task<Customer> DTOCustomerToDomain(int CustomerId)
        {
            return await _customerRepository.GetByIDAsync(CustomerId);
        }
        public DTO_to_Domain() { }
    }
}

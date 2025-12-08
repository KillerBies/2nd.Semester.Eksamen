using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;

namespace _2nd.Semester.Eksamen.Application.Adapters
{
    public class DTO_to_Domain
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        public DTO_to_Domain(ICustomerRepository customerRepository,IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _customerRepository = customerRepository;
        }
        public async Task<Treatment> DTOTreatmentToDomain(TreatmentDTO treatmentDTO)
        {
            var result = await _treatmentRepository.GetByIDAsync(treatmentDTO.TreatmentId);
            return (Treatment)result;
        }
        public async Task<Employee> DTOEmployeeToDomain(EmployeeDTO employeeDTO)
        {
            var result = await _employeeRepository.GetByIDAsync(employeeDTO.EmployeeId);
            return result;
        }
        public async Task<Booking> DTOBookingToDomain(BookingDTO booking)
        {
            List<TreatmentBooking> treatments = new();
            foreach(var treatment in booking.TreatmentBookingDTOs)
            {
                treatments.Add(await DTOTreatmentBookingToDomainToDb(treatment));
            }
            var customer = await DTOCustomerToDomain(booking.Customer.id);
            //BEFORE SENT FULL CUSTOMER IN
            return new Booking(booking.Customer.id, booking.Start, booking.End, treatments);
        }
        public async Task<TreatmentBooking> DTOTreatmentBookingToDomain(TreatmentBookingDTO treatmentBookingDTO)
        {
            var treatment = await DTOTreatmentToDomain(treatmentBookingDTO.Treatment);
            var employee = await DTOEmployeeToDomain(treatmentBookingDTO.Employee);
            var result = new TreatmentBooking(treatment, employee, treatmentBookingDTO.Start, treatmentBookingDTO.End);
            return result;
        }
        public async Task<TreatmentBooking> DTOTreatmentBookingToDomainToDb(TreatmentBookingDTO treatmentBookingDTO)
        {
            var treatment = await DTOTreatmentToDomain(treatmentBookingDTO.Treatment);
            var employee = await DTOEmployeeToDomain(treatmentBookingDTO.Employee);
            var result = new TreatmentBooking(treatment.Id, employee.Id, treatmentBookingDTO.Start, treatmentBookingDTO.End);
            return result;
        }



        public async Task<Customer> DTOCustomerToDomain(int CustomerId)
        {
            var customer = await _customerRepository.GetByIDAsync(CustomerId);
            if (customer == null) throw new NullReferenceException("Customer is null");
            return customer;
            throw new InvalidOperationException($"Customer with ID {CustomerId} is not a valid derived type.");
        }
    }
}

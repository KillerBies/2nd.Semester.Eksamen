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
        public DTO_to_Domain(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, IPrivateCustomerRepository privateCustomerRepository, ICompanyCustomerRepository companyCustomerRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _companyCustomerRepository = companyCustomerRepository;
            _privateCustomerRepository = privateCustomerRepository;

        }
        public Treatment DTOTreatmentToDomain(TreatmentDTO treatmentDTO)
        {
            return _treatmentRepository.GetByIDAsync(treatmentDTO.TreatmentId).Result;
        }
        public Employee DTOEmployeeToDomain(EmployeeDTO employeeDTO)
        {
            return _employeeRepository.GetByIDAsync(employeeDTO.EmployeeId).Result;
        }
        public Booking DTOBookingToDomain(BookingDTO booking)
        {
            var treatments = booking.TreatmentBookingDTOs.Select(tb=>DTOTreatmentBookingToDomain(tb)).ToList();
            var customer = DTOCustomerToDomain(booking.Customer);
            return new Booking(customer, booking.Start, booking.End, treatments);
        }
        public TreatmentBooking DTOTreatmentBookingToDomain(TreatmentBookingDTO treatmentBookingDTO)
        {
            var treatment = DTOTreatmentToDomain(treatmentBookingDTO.Treatment);
            var employee = DTOEmployeeToDomain(treatmentBookingDTO.Employee);
            return new TreatmentBooking(treatment, employee, treatmentBookingDTO.Start, treatmentBookingDTO.End);
        }
        public Customer DTOCustomerToDomain(CustomerDTO customer)
        {
            if(customer.GetType()==typeof(PrivateCustomerDTO))
            {
                return _privateCustomerRepository.GetByIDAsync(customer.id).Result;
            }
            return _companyCustomerRepository.GetByIDAsync(customer.id).Result;

        }
        public DTO_to_Domain() { }
    }
}

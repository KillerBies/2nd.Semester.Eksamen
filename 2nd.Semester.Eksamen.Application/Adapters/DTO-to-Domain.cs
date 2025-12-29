using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
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
        public Product DTOProductToNewDomain(NewProductDTO productDTO)
        {
            var result = new Product(productDTO.Name, productDTO.Price, productDTO.Description) { Category=productDTO.Category};
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
            var newBooking = new Booking(booking.Customer.id, booking.Start, booking.End, treatments);
            if (booking.BookingId != null)
                newBooking.Id = (int)booking.BookingId;
            return newBooking;
        }
        public async Task<Booking> DTOBookingToDomainEdit(BookingDTO booking)
        {
            List<TreatmentBooking> treatments = new();
            foreach (var treatment in booking.TreatmentBookingDTOs)
            {
                treatments.Add(await DTOTreatmentBookingToDomain(treatment));
            }
            var customer = await DTOCustomerToDomain(booking.Customer.id);
            //BEFORE SENT FULL CUSTOMER IN
            var newBooking = new Booking(booking.Customer.id, booking.Start, booking.End, treatments);
            if (booking.BookingId != null)
                newBooking.Id = (int)booking.BookingId;
            return newBooking;
        }
        public async Task<TreatmentBooking> DTOTreatmentBookingToDomain(TreatmentBookingDTO treatmentBookingDTO)
        {
            var treatment = await DTOTreatmentToDomain(treatmentBookingDTO.Treatment);
            var employee = await DTOEmployeeToDomain(treatmentBookingDTO.Employee);
            var result = new TreatmentBooking(treatment, employee, treatmentBookingDTO.Start, treatmentBookingDTO.End) { Price = treatmentBookingDTO.Price};
            return result;
        }
        public async Task<TreatmentBooking> DTOTreatmentBookingToDomainToDb(TreatmentBookingDTO treatmentBookingDTO)
        {
            var treatment = await DTOTreatmentToDomain(treatmentBookingDTO.Treatment);
            var employee = await DTOEmployeeToDomain(treatmentBookingDTO.Employee);
            var result = new TreatmentBooking(treatment.Id, employee.Id, treatmentBookingDTO.Start, treatmentBookingDTO.End) { Price = treatmentBookingDTO.Price };
            return result;
        }



        public async Task<Customer> DTOCustomerToDomain(int CustomerId)
        {
            var customer = await _customerRepository.GetByIDAsync(CustomerId);
            if (customer == null) throw new NullReferenceException("Customer is null");
            return customer;
            throw new InvalidOperationException($"Customer with ID {CustomerId} is not a valid derived type.");
        }

        public LoyaltyDiscount DTOLoyaltyDiscountToDomain(LoyaltyDiscountDTO discount)
        {
            return new LoyaltyDiscount(discount.MinimumVisits, discount.DiscountType, discount.Name, discount.TreatmentDiscount/100, discount.ProductDiscount/100);
        }
        public Campaign DTOCampaignDiscountToDomain(CampaignDiscountDTO discount)
        {
            return new Campaign(discount.Name, discount.TreatmentDiscount/100, discount.ProductDiscount/100, discount.Start,discount.End);
        }

        public async Task<Employee> DTOEmployeeInputToDomain(EmployeeInputDTO dto)
        {
            var specialtyString = string.Join(", ", dto.Specialties.Select(s => s.Value));
            var address = new Address(dto.Address.City, dto.Address.PostalCode, dto.Address.StreetName, dto.Address.HouseNumber);

            var employee = new Employee(
                firstname: dto.FirstName,
                lastname: dto.LastName,
                type: dto.Type.GetDescription(),
                specialties: specialtyString,
                address: address,
                experience: dto.ExperienceLevel.GetDescription(),
                gender: dto.Gender.GetDescription(),
                email: dto.Email,
                phoneNumber: dto.PhoneNumber,
                basePriceMultiplier: dto.BasePriceMultiplier,
                workEnd: dto.WorkEnd != null ? TimeOnly.FromTimeSpan(dto.WorkEnd) : new(08, 0, 0),
                workStart: dto.WorkStart != null ? TimeOnly.FromTimeSpan(dto.WorkStart) : new(18, 0, 0)
            );

            return employee;
        }
        public async Task<Employee> DTOEmployeeUpdateToDomain(int employeeId, EmployeeUpdateDTO dto)
        {
            // Fetch existing employee from repo
            var employee = await _employeeRepository.GetByIDAsync(employeeId);
            if (employee == null) throw new NullReferenceException($"Employee {employeeId} not found");

            // Map the updated values from DTO
            employee.TrySetName(dto.FirstName);
            employee.TrySetLastName(dto.FirstName, dto.LastName);
            employee.TrySetEmail(dto.Email);
            employee.TrySetPhoneNumber(dto.PhoneNumber);
            employee.TrySetSpecialties(string.Join(", ", dto.Specialties.Select(s => s.Value)));
            employee.TrySetGender(dto.Gender.GetDescription());
            employee.TrySetBasePriceMultiplier(dto.BasePriceMultiplier);
            employee.TrySetExperience(dto.ExperienceLevel.GetDescription());
            employee.TrySetType(dto.Type.GetDescription());
            employee.TrySetAddress(dto.Address.City, dto.Address.PostalCode, dto.Address.StreetName, dto.Address.HouseNumber);

            return employee;
        }

    }
}

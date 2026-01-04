using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Application.Adapters
{
    public class Domain_to_DTO
    {
        public TreatmentDTO TreatmentToDTO(Treatment treatment)
        {
            return new TreatmentDTO
            {
                TreatmentId = treatment.Id,
                Name = treatment.Name,
                Category = treatment.Category,
                Duration = treatment.Duration,
                BasePrice = treatment.Price,
                RequiredSpecialties = treatment.RequiredSpecialties,
                Description = treatment.Description
            };
        }

        public ProductDTO ProductToDTO(Product product)
        {
            return new ProductDTO() {ProductId=product.Id,Name=product.Name,Price=product.Price,Guid=product.Guid};
        }
        public EmployeeDTO EmployeeToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                ExperienceLevel = employee.ExperienceLevel,
                BasePriceMultiplier = employee.BasePriceMultiplier,
                Specialties = employee.Specialties
            };
        }
        public TreatmentBookingDTO TreatmentBookingToDTO(TreatmentBooking treatmentBooking)
        {
            return new TreatmentBookingDTO
            {
                Treatment = TreatmentToDTO(treatmentBooking.Treatment),
                Employee = EmployeeToDTO(treatmentBooking.Employee),
                Start = treatmentBooking.Start,
                End = treatmentBooking.End,
                Price = treatmentBooking.Price
            };
        }
        public CustomerDTO CustomerToDTO(Customer customer)
        {
            return new CustomerDTO(customer);
        }
        public PrivateCustomerDTO PrivateCustomerToDTO(PrivateCustomer PrivateCustomer)
        {
            var dto = CustomerToDTO(PrivateCustomer);
            return new PrivateCustomerDTO(PrivateCustomer);
        }
        public CompanyCustomerDTO BusinessCustomerToDTO(CompanyCustomer CompanyCustomer)
        {
            var dto = CustomerToDTO(CompanyCustomer);
            return new CompanyCustomerDTO(CompanyCustomer);
        }
        public BookingDTO BookingToDTO(Booking booking)
        {
            return new BookingDTO
            {
                TreatmentBookingDTOs = booking.Treatments.Select(tb => TreatmentBookingToDTO(tb)).ToList(),
                Customer = CustomerToDTO(booking.Customer),
                Start = booking.Start,
                End = booking.End,
                Duration = booking.Duration,
                BookingId = booking.Id,
                CustomerId = booking.CustomerId
            };
        }
        public BookingDTO PartBookingToDTO(Booking booking)
        {
            return new BookingDTO
            {
                TreatmentBookingDTOs = booking.Treatments.Select(tb => TreatmentBookingToDTO(tb)).ToList(),
                CustomerId = booking.CustomerId,
                Start = booking.Start,
                End = booking.End,
                Duration = booking.Duration
            };
        }
        public EmployeeDetailsDTO EmployeeToDetailsDTO(Employee employee)
        {
            if (employee == null) return null;

            return new EmployeeDetailsDTO
            {
                Id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.LastName,
                Type = employee.Type,
                Specialty = employee.Specialties,
                Experience = employee.ExperienceLevel,
                Gender = employee.Gender,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                BasePriceMultiplier = employee.BasePriceMultiplier,

                City = employee.Address?.City,
                PostalCode = employee.Address?.PostalCode,
                StreetName = employee.Address?.StreetName,
                HouseNumber = employee.Address?.HouseNumber
            };
        }
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

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
            };
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
                End= treatmentBooking.End
            };
        }
        public CustomerDTO CustomerToDTO(Customer customer)
        {
            return new CustomerDTO
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                City = customer.Address.City,
                PostalCode = customer.Address.PostalCode,
                StreetName = customer.Address.StreetName,
                HouseNumber = customer.Address.HouseNumber
            };
        }
        public PrivateCustomerDTO PrivateCustomerToDTO(PrivateCustomer PrivateCustomer)
        {
            var dto = CustomerToDTO(PrivateCustomer);
            return new PrivateCustomerDTO
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                PostalCode = dto.PostalCode,
                StreetName = dto.StreetName,
                HouseNumber = dto.HouseNumber,
                LastName = PrivateCustomer.LastName,
                Birthday = PrivateCustomer.BirthDate,
                Gender = PrivateCustomer.Gender.Value
            };
        }
        public CompanyCustomerDTO BusinessCustomerToDTO(CompanyCustomer CompanyCustomer)
        {
            var dto = CustomerToDTO(CompanyCustomer);
            return new CompanyCustomerDTO
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                PostalCode = dto.PostalCode,
                StreetName = dto.StreetName,
                HouseNumber = dto.HouseNumber,
                CVRNumber = CompanyCustomer.CVRNumber
            };
        }
        public BookingDTO BookingToDTO(Booking booking)
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
    }
}

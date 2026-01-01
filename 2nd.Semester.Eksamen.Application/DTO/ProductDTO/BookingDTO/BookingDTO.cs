using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class BookingDTO
    {
        public Guid CustomerGuid { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public CustomerDTO Customer { get; set; }
        [Required]
        public DateTime Start { get; set; } = DateTime.Now;
        [Required]
        public DateTime End { get; set; } = DateTime.Now;
        [Required]
        public List<TreatmentBookingDTO> TreatmentBookingDTOs { get; set; } = new List<TreatmentBookingDTO>(){new()};
        [Required]
        public bool CustomerNotification { get; set; } = false;
        [Required]
        public WaitListDTO WaitList { get; set; } = new();
        [Required]
        public TimeSpan Duration { get; set; } = new();
        public decimal Price => TreatmentBookingDTOs.Select(tb => tb.Price).Sum();
        public int BookingId { get; set; } = 0;
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public BookingDTO(Booking booking)
        {
            BookingId = booking.Id;
            CustomerId = booking.CustomerId;
            if(booking.Customer is PrivateCustomer pc)
            {
                Customer = new PrivateCustomerDTO(pc);
            }
            else if (booking.Customer is CompanyCustomer cc)
            {
                Customer = new CompanyCustomerDTO(cc);
            }
            else
            {
                Customer = new CustomerDTO(booking.Customer);
            }
            Start = booking.Start;
            End = booking.End;
            TreatmentBookingDTOs = booking.Treatments.Select(tb => new TreatmentBookingDTO(tb)).ToList();
            Duration = booking.Duration;
            Status = booking.Status;
        }
        public BookingDTO(BookingDTO booking)
        {
            BookingId = booking.BookingId;
            CustomerId = booking.CustomerId;
            Customer = booking.Customer;
            Start = booking.Start;
            End = booking.End;
            TreatmentBookingDTOs = booking.TreatmentBookingDTOs.Select(tb => new TreatmentBookingDTO(tb)).ToList();
            Duration = booking.Duration;
            Status = booking.Status;
        }
        public BookingDTO() { } 
    }
}

using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO
{
    public class TreatmentBookingDTO
    {
        public int BookingId { get; set; }
        public Guid BookingGuid { get; set; }
        [Required]
        public TreatmentDTO Treatment { get; set; } = new();
        [Required]
        public EmployeeDTO Employee { get; set; } = new();
        public int CustomerId {get;set;}
        public Guid CustomerGuid { get; set; }
        public string CustomerName { get; set; }
        public Guid TreatmentBookingGuid { get; set; }
        public DateTime Start { get; set; } = new();
        public DateTime End { get; set; } = new();
        [Required]
        public decimal Price { get; set; } = new();
        public void UpdatePrice()
        {
            if (Treatment.TreatmentId != 0 && Employee.EmployeeId != 0)
            {
                Price = Math.Round(Employee.BasePriceMultiplier * Treatment.BasePrice);
            }
        }
        public TreatmentBookingDTO(TreatmentBooking tb, Guid bookingGuid = default, int bookingId = 0)
        {
            Treatment = new TreatmentDTO(tb.Treatment);
            Employee = new EmployeeDTO(tb.Employee);
            TreatmentBookingGuid = tb.Guid;
            Start = tb.Start;
            End = tb.End;
            Price = tb.Price;
            if (bookingGuid != default)
                BookingGuid = bookingGuid;
            BookingId = tb.BookingID;
            if(tb.Booking != null)
            {
                BookingGuid = tb.Booking.Guid;
                if(tb.Booking.Customer != null)
                {
                    CustomerName = tb.Booking.Customer.Name;
                    CustomerGuid = tb.Booking.Customer.Guid;
                }
            }
            
        }
        public TreatmentBookingDTO(TreatmentBookingDTO tb, Guid bookingGuid = default, int bookingId = 0)
        {
            Treatment = new TreatmentDTO(tb.Treatment);
            Employee = new EmployeeDTO(tb.Employee);
            TreatmentBookingGuid = tb.TreatmentBookingGuid;
            Start = tb.Start;
            End = tb.End;
            Price = tb.Price;
            BookingGuid = tb.BookingGuid;
            if (bookingGuid != default)
                BookingGuid = bookingGuid;
            BookingId = bookingId;
        }
        public TreatmentBookingDTO(TreatmentSnapshot tb, Guid bookingGuid = default, int bookingId = 0)
        {
            Treatment = new TreatmentDTO(tb);
            Employee = new EmployeeDTO(tb);
            Price = tb.PricePerUnit;
            TreatmentBookingGuid = tb.TreatmentBookingGuid;
            CustomerId = tb.BookingSnapshot.CustomerSnapshotId;
            CustomerGuid = tb.BookingSnapshot.CustomerSnapshot.Guid;
            if (bookingGuid != default)
                BookingGuid = bookingGuid;
            BookingId = bookingId;
        }
        public TreatmentBookingDTO()
        {

        }

    }
}

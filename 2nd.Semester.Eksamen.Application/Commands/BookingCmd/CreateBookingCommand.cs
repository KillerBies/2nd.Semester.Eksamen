using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.Commands.BookingCmd
{
    public class CreateBookingCommand
    {
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CreateTreatmentBookingCommand> Treatments { get; set; }
        public decimal Price { get; set; } 
        public CreateBookingCommand(BookingDTO booking)
        {
            CustomerId = booking.Customer.id;
            StartDate = booking.Start;
            EndDate = booking.End;
            Treatments = booking.TreatmentBookingDTOs.Select(tb => new CreateTreatmentBookingCommand(tb)).ToList();
            Price = booking.Price;
        }
    }
}

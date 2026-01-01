using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public abstract record DetailsContext;

    public record BookingDetailsContext(BookingDTO Booking) : DetailsContext;
    public record EmployeeDetailsContext(EmployeeDTO Employee) : DetailsContext;
    public record CustomerDetailsContext(CustomerDTO Customer) : DetailsContext;
    public record ProductContext(ProductOverviewDTO Product) : DetailsContext;
    public record DiscountContext(DiscountOverviewDTO Discount) : DetailsContext;
    public record TreatmentContext(TreatmentDTO Treatment) : DetailsContext;
    public record OrderSnapShotContext(OrderSnapshot OrderSnapshot) : DetailsContext;
    public record BookingSnapShotContext(BookingSnapshot BookingSnapshot) : DetailsContext;
    public record TreatmentBookingSnapShotContext(CustomerSnapshot CustomerSnapshot) : DetailsContext;
}

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
    public record CustomerProductContext(ProductOverviewDTO Product) : DetailsContext;
    public record CustomerDiscountContext(DiscountOverviewDTO Discount) : DetailsContext;
    public record CustomerTreatmentContext(TreatmentDTO Treatment) : DetailsContext;
    public record CustomerOrderSnapShotContext(OrderSnapshot OrderSnapshot) : DetailsContext;
    public record CustomerBookingSnapShotContext(BookingSnapshot BookingSnapshot) : DetailsContext;
    public record CustomerTreatmentBookingSnapShotContext(CustomerSnapshot CustomerSnapshot) : DetailsContext;
}

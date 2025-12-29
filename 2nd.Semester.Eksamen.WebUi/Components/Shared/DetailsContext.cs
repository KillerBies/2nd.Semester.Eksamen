using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public abstract record DetailsContext;

    public record BookingDetailsContext(Booking Booking) : DetailsContext;

    public record EmployeeDetailsContext(Employee Employee) : DetailsContext;
}

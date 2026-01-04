using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;

namespace _2nd.Semester.Eksamen.Application
{
    public abstract record DetailsContext;

    public record ContextBooking: DetailsContext;
    public record BookingDetailsContext(BookingDTO Booking) : ContextBooking;
    public record BookingEditContext(BookingDTO Booking) : ContextBooking;
    public record BookingSnapShotContext(BookingDTO OrderInfo) : ContextBooking;
    public record BookingPayContext(BookingDTO Booking) : ContextBooking;

    public record ContextEmployee : DetailsContext;
    public record EmployeeDetailsContext(EmployeeDetailsDTO Employee) : ContextEmployee;
    public record EmployeeEditContext(EmployeeDetailsDTO Employee) : ContextEmployee;
    public record EmployeeSnapShotContext(EmployeeDetailsDTO Employee) : ContextEmployee;

    public record ContextCustomer : DetailsContext;
    public record CustomerDetailsContext(CustomerDTO Customer) : ContextCustomer;
    public record CustomerEditContext(CustomerDTO Customer) : ContextCustomer;
    public record CustomerSnapShotDetailsContext(CustomerDTO Customer) : ContextCustomer;
    
    public record ContextProduct : DetailsContext;
    public record ProductContext(ProductOverviewDTO Product) : ContextProduct;
    public record ProductEditContext(ProductOverviewDTO Product) : ContextProduct;
    public record ProductSnapShotContext(ProductOverviewDTO Product) : ContextProduct;

    public record ContextDiscount : DetailsContext;
    public record DiscountContext(DiscountOverviewDTO Discount) : ContextDiscount;
    public record DiscountEditContext(DiscountOverviewDTO Discount) : ContextDiscount;
    public record DiscountSnapShotContext(DiscountOverviewDTO Discount) : ContextDiscount;
    
    public record ContextTreatment : DetailsContext;
    public record TreatmentContext(TreatmentDTO Treatment) : ContextTreatment;
    public record TreatmentEditContext(TreatmentDTO Treatment) : ContextTreatment;
    public record TreatmentSnapshotContext(TreatmentDTO Treatment) : ContextTreatment;
    
    public record OrderSnapShotContext(OrderHistoryDTO OrderSnapshot) : DetailsContext;

}

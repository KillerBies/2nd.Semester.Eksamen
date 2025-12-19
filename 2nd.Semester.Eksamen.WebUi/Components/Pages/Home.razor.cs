using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.InfrastructureServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class Home
    {
        private DateTime CurrentTime => DateTime.Now;
        private List<Treatment> treatments = new();
        [Inject] private ICustomerRepository _CustomerRepository { get; set; }
        [Inject] private ITreatmentBookingRepository _treatmentBookingRepository { get; set; }
        [Inject] private ITreatmentRepository _treatmentRepository { get; set; }
        [Inject] private IEmployeeRepository _employeeRepository { get; set; }
        [Inject] private BookingQueryService _bookingQueryService { get; set; }
        [Inject] private BookingApplicationService _bookingApplicationService { get; set; }
        private List<BookingDTO> BookingsToday => bookings.Where(b=>b.Start.Date == CurrentTime.Date).ToList();
        private int TreatmentCount => BookingsToday.Select(b => b.TreatmentBookingDTOs.Count).Sum();
        private int CustomerCount => BookingsToday.Select(b => b.Customer.id).Distinct().Count();
        private int BookingsCount => BookingsToday.Count();
        private List<BookingDTO> bookings { get; set; } = new();
        private bool ShowBookingWarning { get; set; } = false;
        private bool ShowBookingPayment { get; set; } = false;
        private BookingDTO selectedBooking { get; set; } = new();
        private string errorMessage { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                bookings = await _bookingQueryService.GetUpcomingBookingsAsync();
                if (!bookings.Any()) errorMessage = "Ingen Bookinger Blev Fundet.";
            }
            catch
            {
                errorMessage = "Ingen forbindelse til databasen.";
            }
        }


        public void GetBookingWarning(BookingDTO booking)
        {
            ShowBookingWarning = true;
            selectedBooking = booking;
        }
        public void PayBooking(BookingDTO booking)
        {
            ShowBookingPayment = true;
            selectedBooking = booking;
        }
        private async Task ConfirmCancelBooking()
        {
            try
            {
                await _bookingApplicationService.CancelBookingAsync(selectedBooking);
                bookings.Remove(selectedBooking);
            }
            catch
            {
                errorMessage = "Noget gik galt i forbindelse med at aflyse bookingen!";
            }
        }
        public async void InjectData()
        {
            var customer1 = new PrivateCustomer("Andersen", Gender.Male, new DateOnly(2004, 2, 1), "Hans Christian", new Address("oiio", "323212", "fda", "43f"), "12345678", "21w@dwq.com", "", false);
            var customer2 = new PrivateCustomer("Jensen", Gender.Male, new DateOnly(2004, 2, 1), "Jens", new Address("oiio", "323217", "fda", "43f"), "12345578", "21w@dwq.com","",false);
            var address1 = new Address("New York", "10001", "Main St", "101");
            var address2 = new Address("Los Angeles", "90001", "Elm St", "202");
            var employee1 = new Employee("Alice", "Johnson", "alice.johnson@example.com", "55511141", address1, 1.2m, "5 years", "Tekniker", "Styling, Maskinklip, ", "Female", new TimeOnly(08, 0), new TimeOnly(16,0));
            var employee2 = new Employee("Bob", "Smith", "bob.smith@example.com", "45552222", address2, 1.5m, "10 years", "Electrician", "Maskinklip, Makeup, Massage, ", "Male", new TimeOnly(09,0), new TimeOnly(17,0));
            var treatment1 = new Treatment("Deep Tissue Massage", 75.00m, "Intense massage targeting deep muscle layers to relieve tension.", "Massage", TimeSpan.FromMinutes(60));

            var treatment2 = new Treatment("Facial Rejuvenation", 50.00m, "Revitalizing facial treatment to cleanse and hydrate skin.", "Skincare", TimeSpan.FromMinutes(45));
            await _CustomerRepository.CreateNewCustomerAsync(customer1);
            await _CustomerRepository.CreateNewCustomerAsync(customer2);
            await _treatmentRepository.CreateNewAsync(treatment1);
            await _treatmentRepository.CreateNewAsync(treatment2);
            await _employeeRepository.CreateNewAsync(employee2);
            await _employeeRepository.CreateNewAsync(employee1);
        }
        private async Task getalldata()
        {
            treatments = (List<Treatment>)await _treatmentRepository.GetAllAsync();
        }
    }
}

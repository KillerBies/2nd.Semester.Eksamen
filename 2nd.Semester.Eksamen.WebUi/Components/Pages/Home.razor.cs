using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
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
        [Inject] private ICustomerRepository _CustomerRepository { get; set; }
        [Inject] private ITreatmentBookingRepository _treatmentBookingRepository { get; set; }
        [Inject] private ITreatmentRepository _treatmentRepository { get; set; }
        [Inject] private IEmployeeRepository _employeeRepository { get; set; }
        [Inject] private BookingQueryService _bookingQueryService { get; set; }
        [Inject] private BookingApplicationService _bookingApplicationService { get; set; }

        //List of bookings For frontpage
        private List<BookingDTO> bookings { get; set; } = new();

        //Current time for calculations and showing
        private DateTime CurrentTime => DateTime.Now;

        //Stats
        private int TreatmentCount => bookings.Select(b => b.TreatmentBookingDTOs.Count).Sum();
        private int CustomerCount => bookings.Select(b => b.Customer.id).Distinct().Count();
        private int BookingsCount => bookings.Count();


        //For cancel booking warning
        private bool ShowBookingWarning { get; set; } = false;

        //For booking Payment sidepage
        private bool ShowBookingPayment { get; set; } = false;

        //Booking Selected on cancel or show details
        private BookingDTO selectedBooking { get; set; } = new();

        //Booking selected on payment (including from the details window)
        private BookingDTO PayBooking { get; set; }

        //error message for showing
        private string errorMessage { get; set; } = "";


        protected override async Task OnInitializedAsync()
        {
            //Try get the bookings not yet started. If non make error message no bookings found, Else if exception error message becomes "No connection to database"
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

        //For when user clicks pay. Initializes pay window
        private async void Pay(BookingDTO booking)
        {
            PayBooking = booking;
            ShowBookingPayment = true;
        }

        //for refreshing page when payment is done or something else happened that requires a refresh to update page
        public void Refresh()
        {
            Navi.Refresh(true);
        }

        //to get booking warning when user presses cancel
        public void GetBookingWarning(BookingDTO booking)
        {
            ShowBookingWarning = true;
            selectedBooking = booking;
        }

        //on confirmation of delete from cancelation window
        private async Task ConfirmCancelBooking()
        {
            try
            {
                await _bookingApplicationService.CancelBookingAsync(selectedBooking);
                ShowBookingWarning = false;
                Refresh();
            }
            catch
            {
                errorMessage = "Noget gik galt i forbindelse med at aflyse bookingen!";
            }
        }

        //TEMP// 
        //Only for demo use to showcase layout and the like.
        public async void InjectData()
        {
            var customer1 = new PrivateCustomer("Andersen", Gender.Male, new DateOnly(2004, 2, 1), "Hans Christian", new Address("oiio", "3232", "fda", "43f"), "12345678", "21w@dwq.com", "", false);
            var customer2 = new PrivateCustomer("Jensen", Gender.Male, new DateOnly(2004, 2, 1), "Jens", new Address("oiio", "3232", "fda", "43f"), "12345578", "21w@dwq.com","",false);
            var address1 = new Address("New York", "1000", "Main St", "101");
            var address2 = new Address("Los Angeles", "9001", "Elm St", "202");
            var employee1 = new Employee("Alice", "Johnson", "alice.johnson@example.com", "55511141", address1, 1.2m, "Senior", "Staff", "Styling, Maskinklip, ", "Female", new TimeOnly(08, 0), new TimeOnly(16,0));
            var employee2 = new Employee("Bob", "Smith", "bob.smith@example.com", "45552222", address2, 1.5m, "Apprentice", "Staff", "Maskinklip, Makeup, Massage, ", "Male", new TimeOnly(09,0), new TimeOnly(17,0));
            var treatment1 = new Treatment("Deep Tissue Massage", 75.00m, "Intense massage targeting deep muscle layers to relieve tension.", "Massage", TimeSpan.FromMinutes(60));

            var treatment2 = new Treatment("Facial Rejuvenation", 50.00m, "Revitalizing facial treatment to cleanse and hydrate skin.", "Skincare", TimeSpan.FromMinutes(45));
            await _CustomerRepository.CreateNewAsync(customer1);
            await _CustomerRepository.CreateNewAsync(customer2);
            await _treatmentRepository.CreateNewAsync(treatment1);
            await _treatmentRepository.CreateNewAsync(treatment2);
            await _employeeRepository.CreateNewAsync(employee2);
            await _employeeRepository.CreateNewAsync(employee1);
        }
    }
}

using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using _2nd.Semester.Eksamen.WebUi.Components.Shared;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Application.DTO;
using System.Globalization;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class BookingForm
    {
        [Inject] private BookingQueryService _bookingQueryService { get; set; }
        [Inject] private BookingApplicationService _bookingApplicationService { get; set; }
        private string _errorMessage = string.Empty;
        [Parameter] public CustomerDTO Customer { get; set; }
        private BookingDTO Booking = new();
        private List<TreatmentDTO> AllTreatments = new();
        private List<EmployeeDTO> AllEmployees = new();
        //private List<AvailableBookingSpotDTO> AvailableBookingSpots = new();
        private List<AvailableBookingSpotDTO> AvailableBookingSpots = new List<AvailableBookingSpotDTO>
{
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(8), EndTime = DateTime.Today.AddHours(8.5), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(8.5), EndTime = DateTime.Today.AddHours(9), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(9), EndTime = DateTime.Today.AddHours(9.5), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(9.5), EndTime = DateTime.Today.AddHours(10), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(10), EndTime = DateTime.Today.AddHours(10.5), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(10.5), EndTime = DateTime.Today.AddHours(11), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(11), EndTime = DateTime.Today.AddHours(11.5), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(11.5), EndTime = DateTime.Today.AddHours(12), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(12), EndTime = DateTime.Today.AddHours(12.5), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(12.5), EndTime = DateTime.Today.AddHours(13), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(13), EndTime = DateTime.Today.AddHours(13.5), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(13.5), EndTime = DateTime.Today.AddHours(14), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(14), EndTime = DateTime.Today.AddHours(14.5), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(14.5), EndTime = DateTime.Today.AddHours(15), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(15), EndTime = DateTime.Today.AddHours(15.5), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(15.5), EndTime = DateTime.Today.AddHours(16), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(16), EndTime = DateTime.Today.AddHours(16.5), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(16.5), EndTime = DateTime.Today.AddHours(17), ContainsWantedEmployees = true },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(17), EndTime = DateTime.Today.AddHours(17.5), ContainsWantedEmployees = false },
    new AvailableBookingSpotDTO { StartTime = DateTime.Today.AddHours(17.5), EndTime = DateTime.Today.AddHours(18), ContainsWantedEmployees = true },
};
        private EditContext EditContext;
        private bool Open = false;
        protected override async Task OnInitializedAsync()
        {
            _errorMessage = "";
            EditContext = new EditContext(Booking);
            await GetData();
        }
        private async Task GetData()
        {
            try
            {
                AllTreatments = (await _bookingQueryService.GetAllTreatmentsAsync()).ToList();
                AllEmployees = (await _bookingQueryService.GetAllEmployeesAsync()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");

                // Fallback: keep lists empty so UI can still load
                AllTreatments = new List<TreatmentDTO>();
                AllEmployees = new List<EmployeeDTO>();

                _errorMessage = "Unable to load data — the server or database is offline.";
            }
        }

        private void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit Called...");
        }

        private void HandleInvalidSubmit()
        {
            Console.WriteLine("HandleInvalidSubmit Called...");
        }

        private void RemoveTreatment(TreatmentBookingDTO treatment)
        {
            Booking.TreatmentBookingDTOs.Remove(treatment);
        }

        private void AddTreatment()
        {
            Booking.TreatmentBookingDTOs.Add(new TreatmentBookingDTO());
        }
        private void ToggleDropdown() => Open = !Open;
        private void SelectTimeSlot(AvailableBookingSpotDTO slot)
        {
            Booking.Start = slot.StartTime;
            Booking.End = slot.EndTime;
        }
    }
}

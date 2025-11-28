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
        //replace the inputtext boxes and the like with a button that redirects them to a create employee and so on page.

        [Inject] private BookingQueryService _bookingQueryService { get; set; }
        [Inject] private BookingApplicationService _bookingApplicationService { get; set; }
        private string _errorMessage = string.Empty;
        [Parameter] public CustomerDTO Customer { get; set; }
        private BookingDTO Booking = new();
        private List<TreatmentDTO> AllTreatments = new();
        private List<EmployeeDTO> AllEmployees = new();
        private List<BookingDTO> AvailableBookingSpots = new();
        private string timeCardError = string.Empty;
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

        private void GetEndTime()
        {
            Booking.End = Booking.Start.Add(Booking.Duration);
        }

        private async void refreshAvailableSlots()
        {
            bool isValid = EditContext.Validate();

            if (!isValid)
            {
                try
                {
                    AvailableBookingSpots = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, DateOnly.FromDateTime(DateTime.Now), 7, 30);
                }
                catch (Exception)
                {
                    timeCardError = "Could not load available time slots.";
                }
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
        private void ToggleDropdown()
        {
            if(AvailableBookingSpots.Count != 0)
            {
                refreshAvailableSlots();
            }
            Open = !Open;
        } 
        private void SelectTimeSlot(BookingDTO slot)
        {
            Booking.Start = slot.Start;
            Booking.End = slot.End;
        }
    }
}

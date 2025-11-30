using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using _2nd.Semester.Eksamen.WebUi.Components.Shared;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Application.DTO;
using System.Globalization;
using System.Security.Principal;

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
            Booking.CustomerId = 1;
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
                AllTreatments = new List<TreatmentDTO>();
                AllEmployees = new List<EmployeeDTO>();

                _errorMessage = "Unable to load data — the server or database is offline.";
            }
        }

        private async Task Arrange()
        {
            Booking.TreatmentBookingDTOs = await _bookingQueryService.ArangeTreatments(Booking);
        }
        private async Task CreateBooking()
        {
            if(EditContext.Validate())
            {
                try
                {
                    await _bookingApplicationService.CreateBookingAsync(Booking);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Booking creation failed: {ex.Message}");
                    _errorMessage = "Booking creation failed. Please try again.";
                }
            }
        }
        private void OnCancel()
        {
            Navi.NavigateTo("/");
        }

        private async void refreshAvailableSlots()
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

        private void MoveUp(int index)
        {
            if (index == 0) return;

            (Booking.TreatmentBookingDTOs[index - 1],
             Booking.TreatmentBookingDTOs[index]) =
             (Booking.TreatmentBookingDTOs[index],
              Booking.TreatmentBookingDTOs[index - 1]);
        }
        private void MoveDown(int index)
        {
            if (index >= Booking.TreatmentBookingDTOs.Count - 1) return;

            (Booking.TreatmentBookingDTOs[index + 1],
             Booking.TreatmentBookingDTOs[index]) =
             (Booking.TreatmentBookingDTOs[index],
              Booking.TreatmentBookingDTOs[index + 1]);
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
            refreshAvailableSlots();
            Open = !Open;
        } 
        private void SelectTimeSlot(BookingDTO slot)
        {
            Booking.Start = slot.Start;
            Booking.End = slot.End;
        }
        private void UpdateTreatment(TreatmentBookingDTO updated)
        {
            // Already updated because it's a reference
            StateHasChanged();
        }
    }
}

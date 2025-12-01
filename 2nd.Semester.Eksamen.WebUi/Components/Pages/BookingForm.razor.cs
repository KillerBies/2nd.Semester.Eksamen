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
        private int Interval { get; set; } = 60;
        private List<TreatmentDTO> AllTreatments = new();
        private List<EmployeeDTO> AllEmployees = new();
        private List<BookingDTO> AvailableBookingSpots = new();
        private string timeCardError = string.Empty;
        private DateOnly startDate = new();
        private int CurrentIndex = new();
        private Dictionary<int, List<BookingDTO>> Pages = new();
        private List<BookingDTO> CurrentPage = new();
        private BookingDTO timeSelected = new();
        private EditContext EditContext;


        private string[] DaysOfWeek { get; set; }
        private TimeOnly StartTime { get; set; } = new TimeOnly(9, 0);
        private TimeOnly EndTime { get; set; } = new TimeOnly(18, 0);
        private bool IsDropdownOpen = false;

        //field for interval, arrow buttons to go back and forth between pages, generate 30 each time.
        protected override async Task OnInitializedAsync()
        {
            DaysOfWeek = Enum.GetNames(typeof(DayOfWeek));
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
        private async Task FowardPage()
        {
            if (Pages.TryGetValue(CurrentIndex+1, out _))
            {
                CurrentIndex += 1;
            }
            else
            {
                startDate = DateOnly.FromDateTime(AvailableBookingSpots.Last().Start);
                await refreshAvailableSlots();
            }
        }
        private void PageBackwards()
        {
            if(!(CurrentIndex<=0)) CurrentIndex -= 1;
        }

        private async Task refreshAvailableSlots()
        {
            try
            {
                if (!AvailableBookingSpots.Any())
                {
                    startDate = DateOnly.FromDateTime(DateTime.Now);
                    Pages[0] = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, startDate, 100, 30, Interval);
                    AvailableBookingSpots = new List<BookingDTO>(Pages[0]);
                }
                else
                {
                    Pages[CurrentIndex + 1] = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, startDate, 100, 30, Interval);
                    AvailableBookingSpots.AddRange(Pages[CurrentIndex + 1]);
                    CurrentIndex += 1;
                }
                IsDropdownOpen = AvailableBookingSpots.Any();
            }
            catch (Exception)
            {
                timeCardError = "Could not load available time slots.";
            }
        }

        private void MoveUp(int index)
        {
            AvailableBookingSpots.Clear();
            IsDropdownOpen = false;
            if (index == 0) return;

            (Booking.TreatmentBookingDTOs[index - 1],
             Booking.TreatmentBookingDTOs[index]) =
             (Booking.TreatmentBookingDTOs[index],
              Booking.TreatmentBookingDTOs[index - 1]);
        }
        private void MoveDown(int index)
        {
            AvailableBookingSpots.Clear();
            IsDropdownOpen = false;
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
            AvailableBookingSpots.Clear();
            IsDropdownOpen = false;
            Booking.TreatmentBookingDTOs.Remove(treatment);
        }

        private void AddTreatment()
        {
            AvailableBookingSpots.Clear();
            IsDropdownOpen = false;
            Booking.TreatmentBookingDTOs.Add(new TreatmentBookingDTO());
        }
        private async Task ToggleDropdown()
        {
            await refreshAvailableSlots();
        } 
        private void SelectTimeSlot(BookingDTO slot)
        {
            timeSelected = slot;
            Booking.Start = slot.Start;
            Booking.End = slot.End;
        }
        private void UpdateTreatment(TreatmentBookingDTO updated)
        {
            AvailableBookingSpots.Clear();
            IsDropdownOpen = false;
            StateHasChanged();
        }

        private bool checkfields()
        {
            return Booking.TreatmentBookingDTOs.Any(tb => (tb.Employee.EmployeeId == 0 || tb.Treatment == null || tb.Employee == null ||string.IsNullOrEmpty(tb.Treatment.Category) || tb.Treatment.TreatmentId == 0));
        }



        // Bound to dropdown
        private string SelectedDay { get; set; } = "";

        // Filtered dates based on selection
    }
}

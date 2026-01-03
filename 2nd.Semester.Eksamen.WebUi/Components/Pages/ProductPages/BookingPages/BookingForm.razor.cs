using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.WebUi.Components.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Security.Principal;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.BookingPages
{
    public partial class BookingForm
    {
        //Add field to put in own booking time start, check and give feedback on whether that time is available.
        //when treatment is added, removed or changed (duration is diffrent) the selected time needs to reset
        //when is edit it also should ideally also give a possible time that is simply an extention of the current one.

        //replace the inputtext boxes and the like with a button that redirects them to a create employee and so on page.

        [Inject] private BookingFormService _bookingFormService { get; set; }
        [Inject] private BookingQueryService _bookingQueryService { get; set; }
        [Inject] private BookingApplicationService _bookingApplicationService { get; set; }
        private string _errorMessage = string.Empty;
        [Parameter] public CustomerDTO Customer { get; set; }
        private string CustomerName { get; set; } = "";
        [Inject] private Domain_to_DTO ToDTOAdapter { get; set; }
        [Parameter] public BookingDTO EditBooking { get; set; }
        public BookingDTO Booking { get; set; } = new();
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public int CustomerId { get; set; }
        private string ErrorMsg = "";
        private int Interval { get; set; } = 60;
        [Parameter] public EventCallback OnClose { get; set; }
        private List<TreatmentDTO> AllTreatments = new();
        private List<EmployeeDTO> AllEmployees = new();
        private List<BookingDTO> AvailableBookingSpots = new();
        private string timeCardError = string.Empty;
        private DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
        private int CurrentIndex = new();
        private Dictionary<int, List<BookingDTO>> Pages = new();
        private List<BookingDTO> CurrentPage = new();
        private BookingDTO timeSelected = new();
        private EditContext EditContext;
        [Parameter] public int BookingId { get; set; }
        private CustomerDTO selectedCustomer { get; set; }
        public bool showPopup = false;


        private string[] DaysOfWeek { get; set; }
        private TimeOnly StartTime { get; set; } = new TimeOnly(8, 0);
        private TimeOnly EndTime { get; set; } = new TimeOnly(18, 0);
        private bool IsDropdownOpen = false;


        protected override async Task OnInitializedAsync()
        {
            if(BookingId != 0)
            {
                IsEdit = true;
                EditBooking = await _bookingFormService.GetBookingById(BookingId);
            }
            _errorMessage = "";
            if(IsEdit)
            {
                Booking = new BookingDTO(EditBooking);
                ResetBookingTimes();
            }
            else
            {
                Booking = new BookingDTO();
            }
            EditContext = new EditContext(Booking);
            await GetData();
        }
        private async Task GetData()
        {
            try
            {
                selectedCustomer = IsEdit ? await _bookingQueryService.GetCustomerByIDAsync(Booking.CustomerId) : await _bookingQueryService.GetCustomerByIDAsync(CustomerId);
                AllTreatments = (await _bookingQueryService.GetAllTreatmentsAsync()).ToList();
                AllEmployees = (await _bookingQueryService.GetAllEmployeesAsync()).ToList();
                if (selectedCustomer == null)
                {
                    ErrorMsg = "Kunden kunne ikke findes. Prøv igen.";
                }
                else
                {
                    Booking.Customer = selectedCustomer;
                    CustomerName = Booking.Customer.Name;
                }
            }
            catch
            {
                AllTreatments = new List<TreatmentDTO>();
                AllEmployees = new List<EmployeeDTO>();

                _errorMessage = "Unable to load data — the server or database is offline.";
            }
        }
        private async Task StartTimeSelected(ChangeEventArgs e)
        {
            await refreshAvailableSlots();
        }
        private async Task CreateBooking()
        {
            if (EditContext.Validate())
            {
                try
                {
                    if (!IsEdit)
                    {
                        await _bookingApplicationService.CreateBookingAsync(Booking);
                        Navi.NavigateTo("/BookingOverview");
                    }
                    else
                    {
                        Booking.BookingId = EditBooking.BookingId;
                        await _bookingApplicationService.RescheduleBookingAsync(Booking);
                        await OnClose.InvokeAsync();
                    }
                }
                catch (Exception ex)
                {
                    _errorMessage = "Booking creation failed. Please try again.";
                }
            }
        }


        private void ResetBookingTimes()
        {
            Booking.Start = new();
            Booking.End = new();
            foreach (var treatment in Booking.TreatmentBookingDTOs)
            {
                treatment.Start = new();
                treatment.End = new();
            }
        }
        private void OnCancel()
        {
            Navi.NavigateTo("/BookingOverview");
        }

        private async void OnStartChange(TimeOnly time)
        {
            StartTime = time;
            AvailableBookingSpots.Clear();
            await refreshAvailableSlots();
        }
        private async Task FowardPage()
        {
            if (Pages.TryGetValue(CurrentIndex + 1, out _))
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
            if (!(CurrentIndex <= 0)) CurrentIndex -= 1;
        }

        private async Task refreshAvailableSlots()
        {
            try
            {
                if (!AvailableBookingSpots.Any())
                {
                    startDate = DateOnly.FromDateTime(DateTime.Now);
                    if(IsEdit)
                    {
                        Pages[0] = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, startDate, 100, 30, Interval, StartTime, EditBooking.TreatmentBookingDTOs);
                    }
                    else
                    {
                        Pages[0] = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, startDate, 100, 30, Interval, StartTime);
                    }
                    AvailableBookingSpots = new List<BookingDTO>(Pages[0]);
                }
                else
                {
                    if (IsEdit)
                    {
                        Pages[CurrentIndex + 1] = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, startDate, 100, 30, Interval, StartTime, EditBooking.TreatmentBookingDTOs);
                    }
                    else
                    {
                        Pages[CurrentIndex + 1] = await _bookingQueryService.GetBookingSuggestionsAsync(Booking.TreatmentBookingDTOs, startDate, 100, 30, Interval, StartTime);
                    }
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
            GenReset();
            if (index == 0) return;
            (Booking.TreatmentBookingDTOs[index - 1],Booking.TreatmentBookingDTOs[index]) = (Booking.TreatmentBookingDTOs[index],Booking.TreatmentBookingDTOs[index - 1]);
        }
        private void MoveDown(int index)
        {
            GenReset();
            if (index >= Booking.TreatmentBookingDTOs.Count - 1) return;
            (Booking.TreatmentBookingDTOs[index + 1],Booking.TreatmentBookingDTOs[index]) =(Booking.TreatmentBookingDTOs[index],Booking.TreatmentBookingDTOs[index + 1]);
        }

        private void GenReset()
        {
            timeSelected = new();
            AvailableBookingSpots.Clear();
            ResetBookingTimes();
            IsDropdownOpen = false;
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
            GenReset();
            Booking.TreatmentBookingDTOs.Remove(treatment);
        }

        private void AddTreatment()
        {
            GenReset();
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
            for (int i = 0; i < slot.TreatmentBookingDTOs.Count(); i++)
            {
                Booking.TreatmentBookingDTOs[i].Start = slot.TreatmentBookingDTOs[i].Start;
                Booking.TreatmentBookingDTOs[i].End = slot.TreatmentBookingDTOs[i].End;
            }
        }
        private void UpdateTreatment(TreatmentBookingDTO updated)
        {
            GenReset();
        }

        private bool checkfields()
        {
            return Booking.TreatmentBookingDTOs.Any(tb => tb.Employee.EmployeeId == 0 || tb.Treatment == null || tb.Employee == null || string.IsNullOrEmpty(tb.Treatment.Category) || tb.Treatment.TreatmentId == 0);
        }
        private bool checkfieldsAndTime()
        {
            return Booking.TreatmentBookingDTOs.Any(tb => tb.Employee.EmployeeId == 0 || tb.Treatment == null || tb.Employee == null || string.IsNullOrEmpty(tb.Treatment.Category) || tb.Treatment.TreatmentId == 0 || tb.Start > tb.End || tb.Start == tb.End);
        }

    }
}

using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Schedules.EmployeeSchedules;
using Microsoft.AspNetCore.Components;
using Radzen;
using Syncfusion.Blazor.Schedule.Internal;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System.ComponentModel.Design.Serialization;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages.EmployeeSchedule
{
    partial class EmployeeSchedulePage
    {
        [Inject] IScheduleService _scheduleService { get; set; }
        private List<EmployeeDTO> Employees { get; set; }
        private DateTime CurrentDate => @DateTime.Now;
        private List<ScheduleItem> ScheduleItems { get; set; }
        private int SelectedEmployeeId { get; set; }
        private string _errorMessage { get; set; } = string.Empty;
        private bool ShowAddWindow { get; set; } = false;
        private List<ScheduleDay> schedule { get; set; } = new();

        private List<TimeRange> SelectedScheduleItems { get; set; } = new();
        private string SelectedType { get; set; } = "";
        private bool ShowDetailsWindow { get; set; } = false;
        private EmployeeVicationDTO VicationDTO { get; set; } = new();
        private List<string> Types { get; set; } = new()
        {
            "Pause",
            "Booking",
            "Ferie",
        };
        private Dictionary<string, string> ColorIndex { get;} = new Dictionary<string, string>()
        {
            { "Freetime", "#42d7f5" },
            { "Booked", "#b55054" },
            { "Unavailable", "#42d7f5" },
        };
        protected override async Task OnInitializedAsync()
        {
            await GetData();
        }
        public bool CreateEmployee { get; set; } = false;
        public bool UpdateEmployee { get; set; } = false;
        public bool ShowDelete { get; set; } = false;
        public DetailsContext DeleteContext { get; set; }
        [Inject] private NavigationManager Nav { get; set; } = null!;
        private async Task GetEmployeeSchedule(int EmployeeId)
        {
            int i = 0;
            schedule = await _scheduleService.GetEmployeeSchedule(EmployeeId);
            ScheduleItems = schedule.SelectMany(s => s.TimeRanges.Select(tr => new ScheduleItem
            {
                Start = s.Date.ToDateTime(tr.Start),
                End = s.Date.ToDateTime(tr.End),
                Id = i++,
                Title = tr.Name,
                Type = tr.Type,
                Guid = tr.Guid
            })).Where(tr => tr.Type != "Freetime").ToList();
        }
        private async Task GetEmployees()
        {
            Employees = (List<EmployeeDTO>)await _scheduleService.GetEmployees();
        }
        
        private async Task GetData()
        {
            await GetEmployees();
            if (Employees == null || !Employees.Any()) _errorMessage = "No Employees could be found";
            else
            {
                SelectedEmployeeId = Employees.First().EmployeeId;
                VicationDTO.EmployeeId = SelectedEmployeeId;
                await GetEmployeeSchedule(SelectedEmployeeId);
            }
        }
        public class ScheduleItem
        {
            public int Id { get; set; }
            public Guid Guid { get; set; }
            public string? Title { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End {  get; set; }
            public string Type { get; set; }
        }

        private async Task OnEmployeeChanged(object value)
        {
            SelectedEmployeeId = (int)value;
            await GetEmployeeSchedule(SelectedEmployeeId);
        }

        void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<ScheduleItem> args)
        {
            ShowDetailsWindow = true;
            var selectedItem = schedule
                .SelectMany(s => s.TimeRanges)
                .FirstOrDefault(tr =>
                    tr.Start == TimeOnly.FromDateTime((DateTime)args.Data.Start) &&
                    tr.End == TimeOnly.FromDateTime((DateTime)args.Data.End)
                );

            if (selectedItem == null)
                return;

            var matchingItems = schedule
                .SelectMany(s => s.TimeRanges)
                .Where(tr => tr.ActivityGuid == selectedItem.ActivityGuid)
                .ToList();

            SelectedScheduleItems.AddRange(matchingItems);
            SelectedScheduleItems.OrderBy(si => si.Start);
        }
        void OnAppintmentRender(SchedulerAppointmentRenderEventArgs<ScheduleItem> args)
        {
            args.Attributes["style"] = GetColor(args.Data.Type);
        }

        private string GetColor(string type)
        {
            return type switch
            {
                "Booked" => "background-color: #034265;",
                "Unavailable" => "background-color: #034265;",
                _=> "background-color: #b55054;"
            };
        }

        private void OnAddToSchedule()
        {
            ShowAddWindow = true;
        }

        private async Task OnConfirmAddVication()
        {
            VicationDTO.EmployeeId = SelectedEmployeeId;
            if(VicationDTO.Start < DateOnly.FromDateTime(DateTime.Now) || VicationDTO.End < DateOnly.FromDateTime(DateTime.Now) || VicationDTO.Start > VicationDTO.End)
            {
                _errorMessage = "Datoer ikke gyldige";
                return;
            }
            try
            {
                _errorMessage = "";
                await _scheduleService.CreateEmployeeVication(VicationDTO);
                ShowAddWindow = false;
            }
            catch
            {
                _errorMessage = "Ferien er lagt på en dag med eksisterende planer og blev derfor ikke lavt";
                ShowAddWindow = false;
            }
        }


        [Inject] public IHistoryService _historyService { get; set; }


        public bool ScheduleSelect { get; set; } = false;
        public Stack<DetailsContext> ContextStack { get; set; } = new();

        private void OnEditBooking(BookingEditContext context)
        {
            if (context.Booking == null)
                return;
            Nav.NavigateTo($"/BookingForm/{context.Booking.CustomerId}/{context.Booking.BookingId}");
        }
        private async Task TreatmentSelect(Guid guid)
        {
            ContextStack.Clear();
            var booking = await _historyService.GetBookingScheduleAsync(guid);
            if(booking.Status != BookingStatus.Pending)
            {
                ContextStack.Push(new BookingSnapShotContext(booking));
            }
            else
            {
                ContextStack.Push(new BookingDetailsContext(booking));
            }
            ShowDetailsWindow = true;
        }
        private void Delete(DetailsContext context)
        {
            DeleteContext = context;
            ShowDelete = true;
        }
        private void AddBookingToCustomer(int customerId)
        {
            if (customerId <= 0)
                return;
            Nav.NavigateTo($"/BookingForm/{customerId}");
        }

        private void Refresh()
        {
            Nav.Refresh(true);
        }

    }
}

using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using WebUIServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.WebUi.Components.Shared;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.TreatmentPages;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{
    public partial class EmployeeDetails
    {
        [Parameter] public int Id { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        [Inject] public IHistoryService _historyService { get; set; }
        [Inject] public IBookingOverviewService _bookingService { get; set; }

        public string _errorMessage { get; set; } = string.Empty;
        [Inject] private IEmployeeRepository _repo { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Parameter] public EventCallback ShowEdit { get; set; }
        private bool ShowConfirmDelete { get; set; } = false;
        [Parameter] public EventCallback<DetailsContext> OnPushContext { get; set; }

        public List<TreatmentHistoryDTO> History { get; set; } = new();
        public List<TreatmentBookingDTO> Upcomming { get; set; } = new();

        [Parameter] public EmployeeDetailsContext Context { get; set; }
        [Parameter] public EmployeeSnapShotContext SnapshotContext { get; set; }
        private EmployeeDetailsDTO Employee { get; set; }
        private EmployeeDetailsDTO EmployeeSnapShot { get; set; }
        [Parameter] public EventCallback<Guid> OnClickCustomer { get; set; }
        [Parameter] public EventCallback<Guid> OnClickEmployee { get; set; }
        [Parameter] public EventCallback<Guid> OnClickTreatment { get; set; }
        [Parameter] public EventCallback<Guid> OnClickBooking { get; set; }
        [Parameter] public EventCallback<DetailsContext> OnEdit { get; set; }
        [Parameter] public EventCallback<DetailsContext> OnDelete { get; set; }
        [Parameter] public bool IsSnapshot { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (IsSnapshot)
            {
                EmployeeSnapShot = SnapshotContext.Employee;
                History = await _historyService.GetEmployeeTreatmentHistoryByGuidAsync(Employee.Guid);
                var historyGuids = History.Select(t => t.BookingGuid).ToHashSet();
                Upcomming = (await _historyService.GetEmployeeUpcommingTreatmentHistoryByGuidAsync(Employee.Guid)).Where(t => !historyGuids.Contains(t.BookingGuid)).ToList();
            }
            else
            {
                Employee = Context.Employee;
                History = await _historyService.GetEmployeeTreatmentHistoryByGuidAsync(Employee.Guid);
                var historyGuids = History.Select(t => t.BookingGuid).ToHashSet();
                Upcomming = (await _historyService.GetEmployeeUpcommingTreatmentHistoryByGuidAsync(Employee.Guid)).Where(t => !historyGuids.Contains(t.BookingGuid)).ToList();
            }
        }
    }
}

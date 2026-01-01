using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
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

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{
    public partial class EmployeeDetails
    {
        [Parameter] public int Id { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        [Inject] public IHistoryService _historyService { get; set; }
        [Inject] public IBookingOverviewService _bookingService { get; set; }
        private Employee Employee { get; set; }
        [Inject] private IEmployeeRepository _repo { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        public List<TreatmentHistoryDTO> History { get; set; }
        public List<TreatmentBookingDTO> Upcomming { get; set; }
        [Parameter] public EventCallback ShowEdit { get; set; }
        private bool ShowConfirmDelete { get; set; } = false;
        [Parameter] public EventCallback<TreatmentHistoryDTO?> OnClickCompleted { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO?> OnClickPending { get; set; }
        [Parameter] public EventCallback<Guid?> OnClickCustomer { get; set; }
        [Parameter] public EventCallback<Guid?> OnClickTreatment { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employee = await _repo.GetByIDAsync(Id);
            History = await _historyService.GetEmployeeTreatmentHistoryByGuidAsync(Employee.Guid);
            Upcomming = await _historyService.GetEmployeeUpcommingTreatmentHistoryByGuidAsync(Employee.Guid);
        }
        private async Task ShowEditWindow()
        {
            await ShowEdit.InvokeAsync();
        }
        private void StartEdit()
        {
            Nav.NavigateTo($"/edit-employee/{Employee.Id}");
        }

        private void TryDelete()
        {
            ShowConfirmDelete = true;
        }
        private async Task ConfirmDelete()
        {
            await DeleteEmployee();
            ShowConfirmDelete = false;
        }
        private async Task DeleteEmployee()
        {
            await EmployeeService.DeleteEmployeeAsync(Employee.Id);
            Nav.NavigateTo("/employees");
        }

    }
}

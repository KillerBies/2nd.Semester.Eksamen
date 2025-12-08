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

namespace Components.Pages.PersonPages.EmployeePages
{
    public partial class EmployeeDetails
{
        [Parameter] public int Id { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        private Employee Employee { get; set; }
        [Inject] private IEmployeeRepository _repo { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        private bool ShowConfirmDelete { get; set; } = false;
        [Inject] IJSRuntime JS { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employee = await _repo.GetByIDAsync(Id);
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

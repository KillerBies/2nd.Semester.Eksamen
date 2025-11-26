using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.WebUi.Services;
using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class EmployeeDetails
{
        [Parameter] public int Id { get; set; }
        [Inject] NavigationManager Nav { get; set; }
        private Employee Employee { get; set; }
        [Inject] private IEmployeeRepository _repo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employee = await _repo.GetByIDAsync(Id);
        } 
        private void StartEdit()
        {
            // Navigate to your edit page, passing the employee ID
            Nav.NavigateTo($"/employees/edit/{Employee.Id}");
        }
    }
}

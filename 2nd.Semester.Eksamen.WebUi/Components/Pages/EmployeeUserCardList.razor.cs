using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Application.Commands;
using _2nd.Semester.Eksamen.Infrastructure.Repositories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class EmployeeUserCardList : ComponentBase
    {
        [Inject]
        private IEmployeeRepository EmployeeRepository { get; set; } = null!;  // inject the repository
        
        [Inject] 
        private ReadEmployeeUserCardsCommand ReadCardsCommand { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!; // Allows navigation to create employee

        public List<EmployeeUserCardModel> Employees { get; set; } = new();

        public string SearchTermName { get; set; } = "";
        public string SearchTermPhone { get; set; } = "";

        public bool LoadFailed { get; set; }

        // Filtered list based on search term

        public IEnumerable<EmployeeUserCardModel> FilterEmployees(Func<EmployeeUserCardModel, string> selector, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Employees;

            return Employees.Where(e => selector(e).Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<EmployeeUserCardModel> FilteredEmployees
        {
            get
            {
                // Filter by name first
                var filtered = FilterEmployees(e => e.Name, SearchTermName);

                // Then filter by phone
                filtered = filtered.Where(e =>
                    string.IsNullOrWhiteSpace(SearchTermPhone) || e.PhoneNumber.Contains(SearchTermPhone, StringComparison.OrdinalIgnoreCase)
                );

                return filtered;
            }
        }

        protected override void OnInitialized()
        {
            // Example seed data
            Employees.Add(new EmployeeUserCardModel
            {
                Name = "Anna Hansen",
                Type = EmployeeType.Staff.GetDescription(),
            });

            Employees.Add(new EmployeeUserCardModel
            {
                Name = "Maria Jensen",
                Type = EmployeeType.Freelance.GetDescription()
            });
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                LoadFailed = false;
                var allEmployees = await EmployeeRepository.GetAllAsync();

                Employees = allEmployees
                    .Select(e => new EmployeeUserCardModel
                    {
                        Id = e!.Id,
                        Name = e.Name + " " + e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        Type = e.Type
                    })
                    .ToList();
            }
            catch (Exception ex) 
            {
                LoadFailed = true;
                Console.WriteLine("Database error: " + ex.Message);
            }
        }

        

        private void GoToEmployee(int id)
        {
            Navigation.NavigateTo($"/employees/{id}");
        }


        protected void AddCard()
        {
            // Add card from database
        }
        private void GoToAddEmployee()
        {
            Navigation.NavigateTo("/employees/create");
        }


    }
}
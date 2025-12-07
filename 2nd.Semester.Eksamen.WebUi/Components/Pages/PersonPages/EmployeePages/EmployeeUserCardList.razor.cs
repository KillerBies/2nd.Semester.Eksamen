using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Helpers;
using _2nd.Semester.Eksamen.Infrastructure.Repositories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class EmployeeUserCardList : ComponentBase
    {
        [Inject]
        private IEmployeeRepository EmployeeRepository { get; set; } = null!;  // inject the repository
        
        [Inject] 
        private ReadEmployeeUserCardsCommand ReadCardsCommand { get; set; } = null!;

        [Inject]
        private NavigationManager Nav { get; set; } = null!; // Allows navigation to create employee

        public List<EmployeeUserCardDTO> Employees { get; set; } = new();

        public string SearchTermName { get; set; } = "";
        public string SearchTermPhone { get; set; } = "";

        public bool LoadFailed { get; set; }

        public bool ShowEmployeeDetails { get; set; } = false;
        public int SelectedEmployeeId { get; set; } = 0;

        // Filtered list based on search term

        public IEnumerable<EmployeeUserCardDTO> FilterEmployees(Func<EmployeeUserCardDTO, string> selector, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Employees;

            return Employees.Where(e => selector(e).Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<EmployeeUserCardDTO> FilteredEmployees
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

        protected override async Task OnInitializedAsync()
        {
            try
            {
                LoadFailed = false;
                var allEmployees = await EmployeeRepository.GetAllAsync();

                Employees = allEmployees
                    .Select(e => new EmployeeUserCardDTO
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
            SelectedEmployeeId = id;
            ShowEmployeeDetails = true;
        }


        protected void AddCard()
        {
            // Add card from database
        }
        private void GoToAddEmployee()
        {
            Nav.NavigateTo("/createemployee");
        }


    }
}
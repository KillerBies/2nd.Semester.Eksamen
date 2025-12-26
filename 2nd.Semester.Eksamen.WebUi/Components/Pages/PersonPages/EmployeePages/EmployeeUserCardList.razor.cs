using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{
    public partial class EmployeeUserCardList : ComponentBase
    {
        [Inject] private IEmployeeRepository EmployeeRepository { get; set; } = null!;
        [Inject] private NavigationManager Nav { get; set; } = null!;

        public List<EmployeeUserCardDTO> Employees { get; set; } = new();

        public string SearchTermName { get; set; } = "";
        public string SearchTermPhone { get; set; } = "";

        public bool LoadFailed { get; set; }
        public bool ShowEmployeeDetails { get; set; } = false;
        public int SelectedEmployeeId { get; set; } = 0;
        public List<Employee> rawEmployees { get; set; } = new();
        public int FrilanceCount => Employees.Count(e=>e.Type == "Freelance");
        public int FuildtidsCount => Employees.Count(e => e.Type == "Staff");
        public bool CreateEmployee { get; set; } = false;
        public bool UpdateEmployee { get; set; } = false;


        // FINAL AND/OR filter – updates automatically on typing
        public IEnumerable<EmployeeUserCardDTO> FilteredEmployees =>
            Employees.Where(e =>
                (string.IsNullOrWhiteSpace(SearchTermName) ||
                 e.Name.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase))
                &&
                (string.IsNullOrWhiteSpace(SearchTermPhone) ||
                 e.PhoneNumber.Contains(SearchTermPhone, StringComparison.OrdinalIgnoreCase))
            );

        protected override async Task OnInitializedAsync()
        {
            try
            {
                LoadFailed = false;

                var rawEmployees = await EmployeeRepository.GetAllAsync();

                Employees = rawEmployees
                    .Select(e => new EmployeeUserCardDTO
                    {
                        Id = e!.Id,
                        Name = $"{e.Name} {e.LastName}",
                        PhoneNumber = e.PhoneNumber,
                        Type = e.Type,
                        BasePriceMultiplier = e.BasePriceMultiplier,
                        Email = e.Email,
                        ExperienceLevel = Enum.Parse<ExperienceLevels>(e.ExperienceLevel),
                        Gender = Enum.Parse<Gender>(e.Gender),
                        City = e.Address.City

                    })
                    .ToList();
            }
            catch
            {
                LoadFailed = true;
            }
        }

        private void Refresh()
        {
            Nav.Refresh(true);
        }
        private void GoToEmployee(int id)
        {
            SelectedEmployeeId = id;
            ShowEmployeeDetails = true;
        }

        private void GoToAddEmployee()
        {
            Nav.NavigateTo("/createemployee");
        }
    }
}

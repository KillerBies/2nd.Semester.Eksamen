using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using Microsoft.AspNetCore.Components;

namespace Components.Pages.PersonPages.EmployeePages
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

                var allEmployees = await EmployeeRepository.GetAllAsync();

                Employees = allEmployees
                    .Select(e => new EmployeeUserCardDTO
                    {
                        Id = e!.Id,
                        Name = $"{e.Name} {e.LastName}",
                        PhoneNumber = e.PhoneNumber,
                        Type = e.Type
                    })
                    .ToList();
            }
            catch
            {
                LoadFailed = true;
            }
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

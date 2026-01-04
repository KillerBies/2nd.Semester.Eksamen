using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.EmployeePages
{
    public partial class EmployeeUserCardList : ComponentBase
    {
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private NavigationManager Nav { get; set; } = null!;

        public string SearchTermName { get; set; } = "";
        public string SearchTermPhone { get; set; } = "";

        public Stack<DetailsContext> ContextStack { get; set; } = new Stack<DetailsContext>();
        public DetailsContext CurrentContext => ContextStack.Peek();
        public bool LoadFailed { get; set; }
        public bool ShowEmployeeDetails { get; set; } = false;
        public int SelectedEmployeeId { get; set; } = 0;
        public List<EmployeeDetailsDTO> Employees { get; set; } = new();
        public int FrilanceCount => Employees.Count(e=>e.Type == "Freelance");
        public int FuildtidsCount => Employees.Count(e => e.Type == "Staff");
        public bool CreateEmployee { get; set; } = false;
        public bool UpdateEmployee { get; set; } = false;
        public bool ShowDelete { get; set; } = false;
        public DetailsContext DeleteContext { get; set; }


        // FINAL AND/OR filter – updates automatically on typing
        public IEnumerable<EmployeeDetailsDTO> FilteredEmployees =>
            Employees.Where(e =>
                (string.IsNullOrWhiteSpace(SearchTermName) || (
                 e.FirstName.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase)
            || e.LastName.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase))
                &&
                (string.IsNullOrWhiteSpace(SearchTermPhone) ||
                 e.PhoneNumber.Contains(SearchTermPhone, StringComparison.OrdinalIgnoreCase))
            ));

        protected override async Task OnInitializedAsync()
        {
            try
            {
                LoadFailed = false;
                Employees = (await EmployeeService.GetAllEmployeeDetailsAsync()).ToList();
            }
            catch
            {
                LoadFailed = true;
            }
        }
        private void OnEditBooking(BookingEditContext context)
        {
            if (context.Booking == null)
                return;
            Nav.NavigateTo($"/BookingForm/{context.Booking.CustomerId}/{context.Booking.BookingId}");
        }
        private async Task EmployeeSelect(EmployeeDetailsDTO employee)
        {
            ContextStack.Clear();
            ContextStack.Push(new EmployeeDetailsContext(employee));
            ShowEmployeeDetails = true;
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

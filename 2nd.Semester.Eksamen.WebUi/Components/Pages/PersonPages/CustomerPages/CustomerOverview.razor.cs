using _2nd.Semester.Eksamen.Application;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.History;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.WebUi.Components.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.CustomerPages
{
    public partial class CustomerOverview
    {
        [Inject] private NavigationManager Nav { get; set; } = null!;
        public string SearchTermName { get; set; } = "";
        private bool EditCustomer { get; set; } = false;
        public string SearchTermPhone { get; set; } = "";
        private bool IsVisible = false;
        private bool CreateCustomer { get; set; } = false;
        [Inject] public ICustomerService _customerService { get; set; }
        private int CompanyCount => Customers.Count(c => c is CompanyCustomerDTO);
        private int PrivateCount => Customers.Count(c => c is PrivateCustomerDTO);
        private List<CustomerDTO> Customers = new();
        private List<CustomerDTO> FilterdCustomers => Customers.Where(c => (string.IsNullOrWhiteSpace(SearchTermName) || c.Name.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase)) && (string.IsNullOrWhiteSpace(SearchTermPhone) || c.PhoneNumber.Contains(SearchTermPhone, StringComparison.OrdinalIgnoreCase))).ToList();
        private CustomerDTO? selectedCustomer;
        public bool isVisible = false;
        public bool ShowWarning = false;
        [Inject] NavigationManager Navi { get; set; }
        [Inject] IBookingOverviewService _bookingOverviewService { get; set; }

        private List<BookingDTO> bookingList = new();
        private List<BookingDTO> CompletedBookings { get; set; } = new();

        private BookingDTO SelectedBooking { get; set; } = new();
        private BookingSnapshot SelectedCompletedBooking { get; set; }

        private bool toggleBookingWarning = false;
        private bool LoadFailed = false;
        private bool OpenEdit = false;
        private bool ShowDelete = false;
        private bool ShowDetails = false;
        public DetailsContext DeleteContext { get; set; }

        public Stack<DetailsContext> ContextStack { get; set; } = new Stack<DetailsContext>();
        public DetailsContext CurrentContext => ContextStack.Peek();

        protected override async Task OnInitializedAsync()
        {
            Customers = await _customerService.GetAllCustomersAsDTO();
        }

        private void Refresh()
        {
            Nav.Refresh(true);
        }




        private async Task DeleteCustomer(int id)
        {

            await _customerService.DeleteByIdAsync(selectedCustomer.id);

        }
        private async Task CreateNewBookingForCustomer()
        {
            Nav.NavigateTo($"/BookingForm/{selectedCustomer.id}");
        }
        private async Task Select(CustomerDTO customer)
        {
            ContextStack.Push(new CustomerDetailsContext(customer));
            ShowDetails = true;
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
            Navi.NavigateTo($"/BookingForm/{customerId}");
        }
    }
}


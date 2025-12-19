using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
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
        public string SearchTermPhone { get; set; } = "";
        private bool LoadFailed = false;
        private bool OpenEdit = false;
        [Inject] public ICustomerService _customerService { get; set; }
        private int BronzeCount => Customers.Count(c=>c.Type == "Bronze");
        private int SilverCount => Customers.Count(c => c.Type == "Sølv");
        private int GoldCount => Customers.Count(c => c.Type == "Guld");
        private List<CustomerDTO> Customers = new();
        private List<CustomerDTO> FilterdCustomers => Customers.Where(c => (string.IsNullOrWhiteSpace(SearchTermName) || c.Name.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase)) && (string.IsNullOrWhiteSpace(SearchTermPhone) || c.PhoneNumber.Contains(SearchTermPhone, StringComparison.OrdinalIgnoreCase))).ToList();
        private CustomerDTO? selectedCustomer;


        public bool isVisible;


        protected override async Task OnInitializedAsync()
        {
            Customers = await _customerService.GetAllCustomersAsDTO();
        }


        private void GoToCreateCustomer()
        {
            Nav.NavigateTo("/create-customer");
        }
        //Overlays true or false
        private void ShowOverlay(CustomerDTO customer)
        {
            selectedCustomer = customer;
            isVisible = true;

        }
        private void HideOverlay()
        {
            selectedCustomer = null;
            isVisible = false;
        }
        private async Task DeleteCustomer(int id)
        {

            await _customerService.DeleteByIdAsync(selectedCustomer.id);

        }




    }
}


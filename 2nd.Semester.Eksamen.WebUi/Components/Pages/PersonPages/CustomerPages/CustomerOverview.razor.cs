using System.Net.NetworkInformation;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;

using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;

using _2nd.Semester.Eksamen.WebUi.Components.Shared;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.CustomerPages
{
    public partial class CustomerOverview
{
        private readonly  ICustomerService _customerService;
      public CustomerOverview(ICustomerService customerService)
        {
            _customerService = customerService;
        }
    List<CustomerDTO> dTOs = new();
    private CustomerDTO? selectedCustomer;


    public bool isVisible;


    protected override async Task OnInitializedAsync()
    {
        dTOs = await _customerService.GetAllCustomersAsDTO();
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


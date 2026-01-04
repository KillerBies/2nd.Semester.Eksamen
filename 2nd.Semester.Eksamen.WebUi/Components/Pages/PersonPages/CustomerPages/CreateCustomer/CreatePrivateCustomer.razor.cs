using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.PersonPages.CustomerPages.CreateCustomer
{
    public partial class CreatePrivateCustomer
    {
        public PrivateCustomerDTO privateCustomerDTO = new PrivateCustomerDTO()
        {
            BirthdayWrapper = DateTime.Now
        };
        [Inject] public ICustomerUpdateService _customerService { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public bool IsBooking { get; set; } = false;
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public PrivateCustomerDTO EditCustomer { get; set; }

        protected override async Task OnInitializedAsync()
        {
           if(IsEdit)
            {
                privateCustomerDTO = EditCustomer;
            }
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                if (IsBooking)
                {
                    int id = await createCustomerService.CreatePrivateCustomerAsync(privateCustomerDTO);
                    Navi.NavigateTo($"/BookingForm/{id}");
                    await OnClose.InvokeAsync();
                }
                else
                {
                    if (IsEdit)
                        await _customerService.UpdateCustomer(privateCustomerDTO);
                    else
                        await createCustomerService.CreatePrivateCustomerAsync(privateCustomerDTO);
                    await OnClose.InvokeAsync();
                    Navi.NavigateTo($"/CustomerOverview");
                }
            }
            catch
            {

            }
        }
    }
}

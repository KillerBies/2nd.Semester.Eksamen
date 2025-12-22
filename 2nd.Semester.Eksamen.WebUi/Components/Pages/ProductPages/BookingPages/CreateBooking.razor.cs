using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using Microsoft.AspNetCore.Components;


namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.BookingPages
{
    public partial class CreateBooking
    {
        private CustomerDTO? searchedCustomer;
        public string phoneNumber;
        private bool customerNotFound;
        [Parameter] public EventCallback OnClose { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            phoneNumber = "";
            customerNotFound = false;
        }

        private async Task GoToExistingCustomer()
        {
            await OnClose.InvokeAsync();
            Navi.NavigateTo($"/BookingForm/{searchedCustomer.id}");
        }

        private async Task GoToCreateCustomer()
        {
            await OnClose.InvokeAsync();
            Navi.NavigateTo($"/create-customer");
        }
        private async Task SearchForPhoneNumber()
        {
            searchedCustomer = await bookingQueryService.SearchPhoneNumber(phoneNumber);
            if (searchedCustomer == null) customerNotFound = true;
            else customerNotFound = false;
        }
    }
}

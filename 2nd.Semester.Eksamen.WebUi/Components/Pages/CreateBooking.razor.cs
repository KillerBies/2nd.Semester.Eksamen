using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Application.Services;


namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class CreateBooking
{
        private Customer? searchedCustomer;
        public string phoneNumber = "";

       
        private void GoToExistingCustomer()
        {
            Navi.NavigateTo($"/BookingForm/{searchedCustomer.Id}");
        }

        private void GoToCreatePrivateCustomer()
        {
            Navi.NavigateTo("/create-private-customer");
        }

        private void GoToCreateCompanyCustomer()
        {
            Navi.NavigateTo("/create-company-customer");
        }

        private async Task SearchForPhoneNumber()
        {
            searchedCustomer = await bookingFormService.GetCustomerByPhoneNumberAsync(phoneNumber);


        }
    }
}

using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;


namespace Components.Pages.ProductPages.BookingPages
{
    public partial class CreateBooking
{
        private CustomerDTO? searchedCustomer;
        public string phoneNumber;
        private bool customerNotFound;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            phoneNumber = "";
            customerNotFound = false;
        }

        private void GoToExistingCustomer()
        {
            Navi.NavigateTo($"/BookingForm/{searchedCustomer.id}");
        }

        private void GoToCreateCustomer()
        {
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

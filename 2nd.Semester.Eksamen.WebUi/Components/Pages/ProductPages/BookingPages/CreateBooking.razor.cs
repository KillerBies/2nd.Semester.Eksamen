using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;


namespace Components.Pages.ProductPages.BookingPages
{
    public partial class CreateBooking
{
        private CustomerDTO? searchedCustomer;
        public string phoneNumber = "";

       
        private void GoToExistingCustomer()
        {
            Navi.NavigateTo($"/BookingForm/{searchedCustomer.id}");
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
            searchedCustomer = await bookingQueryService.GetCustomerByPhoneNumberAsync(phoneNumber);
        }
    }
}

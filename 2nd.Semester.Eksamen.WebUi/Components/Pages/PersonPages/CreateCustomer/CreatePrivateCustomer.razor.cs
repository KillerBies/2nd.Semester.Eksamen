using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;

namespace Components.Pages.PersonPages.CreateCustomer
{
    public partial class CreatePrivateCustomer
    {
        public PrivateCustomerDTO privateCustomerDTO = new PrivateCustomerDTO()
        {
            BirthdayWrapper = DateTime.Now
        };

        private async Task HandleValidSubmit()
        {
            int id = await createCustomerService.CreatePrivateCustomerAsync(privateCustomerDTO);
            
            Navi.NavigateTo($"/BookingForm/{id}");
        }
    }
}

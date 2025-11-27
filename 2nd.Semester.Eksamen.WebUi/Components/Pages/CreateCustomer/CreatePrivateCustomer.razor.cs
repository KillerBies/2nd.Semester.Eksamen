using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.CreateCustomer
{
    public partial class CreatePrivateCustomer
    {
        public PrivateCustomerDTO privateCustomerDTO = new PrivateCustomerDTO()
        {
            BirthdayWrapper = new DateTime(1920, 1, 1)
        };

        private async Task HandleValidSubmit()
        {
            int id = await createCustomerService.CreatePrivateCustomerAsync(privateCustomerDTO);
            
            Navi.NavigateTo($"/BookingForm/{id}");
        }
    }
}

using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using Microsoft.AspNetCore.Components;


namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.CreateCustomer
{
    public partial class CreateCompanyCustomer
    {
        

        public CompanyCustomerDTO companyCustomerDTO = new();

        



        private async Task HandleValidSubmit()
        {
          int id = await createCustomerService.CreateCompanyCustomerAsync(companyCustomerDTO);
            //INDSÆT Manglende TLF SØGNING UD FRA DTO PHONE
            Navi.NavigateTo($"/BookingForm/{id}");
        }

    }
}

using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using Microsoft.AspNetCore.Components;


namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.CreateCustomer
{
    public partial class CreateCompanyCustomer
    {
        

        public CompanyCustomerDTO companyCustomerDTO = new();

        //TODO Remember to prepare the injection in DI-Container



        private async Task HandleValidSubmit()
        {
            await createCustomerService.CreateCompanyCustomerAsync(companyCustomerDTO);
        }

    }
}

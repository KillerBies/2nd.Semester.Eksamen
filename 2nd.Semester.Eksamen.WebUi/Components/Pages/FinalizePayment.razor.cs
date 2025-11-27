using Microsoft.AspNetCore.Components;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces; // adjust if your interface namespace differs
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{
    public partial class FinalizePayment
    {
        // [Parameter] public int CustomerId { get; set; }
        [Inject] private IPrivateCustomerService CustomerService { get; set; } = null!;
        [Inject] private NavigationManager Nav { get; set; } = null!;

        private string? message;

        private async Task FinalizePaymentAsync()
        {
            int CustomerId = 1;
            PrivateCustomer? customer = await CustomerService.GetByIDAsync(CustomerId);

            if (customer == null)
            {
                message = $"Customer with id {CustomerId} not found.";
                return;
            }



            // finish payment here

            if (!customer.SaveAsCustomer)
            {
                await CustomerService.DeleteAsync(customer);
            }
            else
            {
                // Increment visits
                customer.AddVisit();

                await CustomerService.UpdateAsync(customer);

                message = $"Visits incremented and saved! New visits: {customer.NumberOfVisists}";
            }
        }

    }
}

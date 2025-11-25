using Microsoft.AspNetCore.Components;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages
{

    public partial class EmployeeDetails
    {
        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // load employee using Id here
        }
    }


}

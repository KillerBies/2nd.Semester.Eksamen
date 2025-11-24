using _2nd.Semester.Eksamen.Application.DTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public partial class TreatmentEditor
    {
        [Parameter] public EventCallback<TreatmentBookingDTO> OnRemove { get; set; }
        [Parameter] public List<TreatmentDTO> PossibleTreatments { get; set; }
        [Parameter] public List<EmployeeDTO> PossibleEmployees { get; set; }
        [Parameter] public TreatmentBookingDTO TreatmentBooking { get; set; }
        [CascadingParameter] EditContext EditContext { get; set; }


        private async Task Remove()
        {
            await OnRemove.InvokeAsync(TreatmentBooking);
        }
        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            StateHasChanged();
        }
        protected override void OnParametersSet()
        {
            if (TreatmentBooking.Treatment.TreatmentId != null && TreatmentBooking.Employee.EmployeeId != null)
            {
                TreatmentBooking.UpdatePrice(PossibleTreatments, PossibleEmployees);
            }
        }
    }
}

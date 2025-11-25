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
        protected override void OnParametersSet()
        {
            if (TreatmentBooking.Treatment.TreatmentId != 0 && TreatmentBooking.Employee.EmployeeId != 0)
            {
                TreatmentBooking.UpdatePrice(PossibleTreatments, PossibleEmployees);
            }
        }













        private bool AnyEmployees()
        {
            return PossibleEmployees.Count() != 0;
        }
        private bool AnyTreatments()
        {
            return PossibleTreatments.Count() != 0;
        }
        private bool TreatmentCategorySelected()
        {
            return (!string.IsNullOrEmpty(TreatmentBooking.Treatment.Category));
        }
        private bool CanSelectTreatment()
        {
            return (AnyTreatments() && TreatmentCategorySelected());
        }
        private bool TreatmentSelected()
        {
            return (TreatmentBooking.Treatment.TreatmentId != 0 || !string.IsNullOrEmpty(TreatmentBooking.Treatment.Name));
        }
        private bool CanSelectEmployee()
        {
            return (AnyEmployees() && TreatmentSelected() && TreatmentCategorySelected());
        }
        private bool EmployeeSelected()
        {
            return (TreatmentBooking.Employee.EmployeeId != 0 || !string.IsNullOrEmpty(TreatmentBooking.Employee.Name));
        }
        private bool CanSelectPrice()
        {
            return (EmployeeSelected() && TreatmentCategorySelected() && TreatmentSelected() && AnyTreatments());
        }
        private bool CanWritePrice()
        {
            return (EmployeeSelected() && TreatmentCategorySelected() && TreatmentSelected());
        }
    }
}

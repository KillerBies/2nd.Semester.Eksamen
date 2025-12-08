using _2nd.Semester.Eksamen.Application.DTO.PersonDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.EmployeeDTO;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Employees;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Mono.TextTemplating;
using System.Text.RegularExpressions;

namespace _2nd.Semester.Eksamen.WebUi.Components.Shared
{
    public partial class TreatmentEditor
    {


        //Make page on submit that gives a summary with an ok button
        [Parameter] public EventCallback<TreatmentBookingDTO> OnRemove { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO> OnSelectedParameterChange { get; set; }
        [Parameter] public EventCallback<TreatmentBookingDTO> OnChange { get; set; }
        [Parameter] public List<TreatmentDTO> PossibleTreatments { get; set; }
        [Parameter] public List<EmployeeDTO> PossibleEmployees { get; set; }
        [Parameter] public TreatmentBookingDTO TreatmentBooking { get; set; }
        [Parameter] public EventCallback<int> OnUp { get; set; }
        [Parameter] public EventCallback<int> OnDown { get; set; }
        [Parameter] public int Index { get; set; }
        [CascadingParameter] EditContext EditContext { get; set; }


        private TreatmentDTO selectedTreatment => PossibleTreatments.FirstOrDefault(pt => pt.TreatmentId == TreatmentId);
        private EmployeeDTO selectedEmployee => PossibleEmployees.FirstOrDefault(pe => pe.EmployeeId == EmployeeId);

        private int? employeeId { get; set; } = null;
        private int? EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;

                if (employeeId.HasValue)
                {
                    TreatmentBooking.Employee = selectedEmployee;
                }
                else
                {
                    TreatmentBooking.Employee = new EmployeeDTO();
                }
                TreatmentBooking.UpdatePrice();

                Change();
            }
        }
        private int? treatmentId { get; set; } = null;
        private int? TreatmentId
        {
            get => treatmentId;
            set
            {
                treatmentId = value;

                if (treatmentId.HasValue)
                {
                    TreatmentBooking.Treatment = selectedTreatment;
                }
                else
                {
                    TreatmentBooking.Treatment = new TreatmentDTO();
                }

                // Always reset employee when treatment changes
                EmployeeId = null;
                TreatmentBooking.Employee = new EmployeeDTO();
                TreatmentBooking.Price = 0;

                Change();
            }
        }
        private string? category { get; set; } = null;
        private string? Category
        {
            get => category;
            set
            {
                category = value;
                TreatmentId = null;
                TreatmentBooking.Treatment = new TreatmentDTO();
                EmployeeId = null;
                TreatmentBooking.Employee = new EmployeeDTO();
                TreatmentBooking.Price = 0;

                Change();
            }
        }

        private void OnCategoryChanged(ChangeEventArgs e)
        {
            // Reset everything first
            EmployeeId = null;
            TreatmentBooking.Employee = new EmployeeDTO();

            TreatmentId = null;
            TreatmentBooking.Treatment = new TreatmentDTO();
            TreatmentBooking.Price = 0;

            Change();
        }


        private async Task UpPress() => await OnUp.InvokeAsync(Index);
        private async Task DownPress() => await OnDown.InvokeAsync(Index);

        private async Task Remove() => await OnRemove.InvokeAsync(TreatmentBooking);
        private async Task NotifyChange()
        {
            await OnSelectedParameterChange.InvokeAsync(TreatmentBooking);
        }
        protected override void OnAfterRender(bool firstRender)
        {
            Console.WriteLine("Render!");
        }
        private void Change()
        {
            OnChange.InvokeAsync(TreatmentBooking);
        }

    }
}



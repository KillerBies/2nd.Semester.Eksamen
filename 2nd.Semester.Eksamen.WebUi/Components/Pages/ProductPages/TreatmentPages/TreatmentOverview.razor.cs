using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.TreatmentPages
{
    public partial class TreatmentOverview
    {

        [Inject] public ITreatmentService _treatmentService { get; set; }
        [Inject] public NavigationManager Navi {get;set;} 
        private List<TreatmentDTO> Treatments = new();
        private TreatmentDTO? selectedTreatment;
        private string SearchTermName { get; set; }
        private List<TreatmentDTO> FilterdTreatments => Treatments.Where(t => (string.IsNullOrWhiteSpace(SearchTermName) || t.Name.Contains(SearchTermName, StringComparison.OrdinalIgnoreCase))).ToList();


        public bool isVisible;


        protected override async Task OnInitializedAsync()
        {
            Treatments = await _treatmentService.GetAllTreatmentsAsDTOAsync();
        }

        //Overlays true or false
        private void ShowOverlay(TreatmentDTO treatment)
        {
            selectedTreatment = treatment;
            isVisible = true;

        }
        private void HideOverlay()
        {
            selectedTreatment = null;
            isVisible = false;
        }
        private async Task DeleteTreatment(int id)
        {

            await _treatmentService.DeleteByIdDbAsync(selectedTreatment.TreatmentId);

        }

        private void NewTreatmentNavigation()
        {
            Navi.NavigateTo("/Create-Treatment");
        }
    }
}

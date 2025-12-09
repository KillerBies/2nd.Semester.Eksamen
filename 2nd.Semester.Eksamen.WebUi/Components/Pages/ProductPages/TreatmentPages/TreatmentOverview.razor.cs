using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;

namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.TreatmentPages
{
    public partial class TreatmentOverview
{

        private readonly ITreatmentService _treatmentService;
        public TreatmentOverview(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        List<TreatmentDTO> dTOs = new();

        private TreatmentDTO? selectedTreatment;


        public bool isVisible;


        protected override async Task OnInitializedAsync()
        {
            dTOs = await _treatmentService.GetAllTreatmentsAsDTOAsync();
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

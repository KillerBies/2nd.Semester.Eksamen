using System.Net.NetworkInformation;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Adapters;
using System.Collections.Generic;
namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.TreatmentPages
{
    public partial class CreateTreatment
{

        private readonly DTO_to_Domain dtoDomain;
        private readonly ITreatmentService _treatmentService;
        public CreateTreatment(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }



        public class SpecialtyItem
        {
            public string Specialty { get; set; }
            public bool Status { get; set; }
        }
        private List<SpecialtyItem> specialtyItems = new();
        TreatmentDTO treatment = new TreatmentDTO();
        public int hour;
        public int min;
        private DateTime Varighed { get; set; }
        List<string> specialties = new();
        List<string> manuallyAddedSpecialties = new();
        string newSpecialty = "";
        void AddSpecialty()
        {
            if (!string.IsNullOrWhiteSpace(newSpecialty))
            {
                manuallyAddedSpecialties.Add(newSpecialty.Trim());
                newSpecialty = "";
            }
        }

        void RemoveSpecialty(string item)
        {
            manuallyAddedSpecialties.Remove(item);
        }

        protected override async Task OnInitializedAsync()
        {
            specialties = await _treatmentService.GetAllUniqueSpecialtiesAsync();
            specialtyItems = specialties.Select(s => new SpecialtyItem() { Specialty = s }).ToList();
        }

        public async Task HandleValidSubmit()
        {
            treatment.Duration = new TimeSpan(hour, min, 0);
            List<string> requiredSpecialties = new();

            foreach (var specialtyItem in specialtyItems)
            {
                if (specialtyItem.Status == true)
                {

                    requiredSpecialties.Add(specialtyItem.Specialty);
                }
            }
            requiredSpecialties.AddRange(manuallyAddedSpecialties);
            treatment.RequiredSpecialties = requiredSpecialties
        .Select(s => s + ", ")
        .ToList();





            await _treatmentService.CreateNewTreatmentAsync(treatment);
        }










    }
}

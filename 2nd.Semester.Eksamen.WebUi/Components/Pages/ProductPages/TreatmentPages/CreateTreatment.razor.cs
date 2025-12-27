using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.NetworkInformation;
namespace _2nd.Semester.Eksamen.WebUi.Components.Pages.ProductPages.TreatmentPages
{
    public partial class CreateTreatment
{

        private readonly DTO_to_Domain dtoDomain;
        private readonly ITreatmentService _treatmentService;
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public TreatmentDTO TreatmentEdit {get;set;}
        [Inject] public IProductOverviewService productOverviewService { get; set; }
        public List<string> Categories { get; set; } = new();
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
            Categories = await productOverviewService.GetAllCategoriesAsync();
            treatment.Category = "_";
            specialties = await _treatmentService.GetAllUniqueSpecialtiesAsync();
            specialtyItems = specialties.Select(s => new SpecialtyItem() { Specialty = s }).ToList();
            if(IsEdit)
            {
                treatment = TreatmentEdit;
                foreach(var specialty in treatment.RequiredSpecialties)
                {
                    string editSpecialty = specialty.TrimEnd().Trim(',');
                    var item = specialtyItems.FirstOrDefault(si => si.Specialty == editSpecialty);
                    if (item == null)
                    {
                        manuallyAddedSpecialties.Add(editSpecialty);
                    }
                    else
                    {
                        var specialtyItem = specialtyItems.FirstOrDefault(si => si.Specialty == editSpecialty);
                        if (specialtyItem != null)
                        {
                            specialtyItem.Status = true;
                        }
                    }
                }
            }
        }

        public async Task HandleValidSubmit()
        {
            List<string> requiredSpecialties = new();

            foreach (var specialtyItem in specialtyItems)
            {
                if (specialtyItem.Status == true)
                {

                    requiredSpecialties.Add(specialtyItem.Specialty);
                }
            }
            requiredSpecialties.AddRange(manuallyAddedSpecialties);treatment.RequiredSpecialties = requiredSpecialties.Select(s => s + ", ").ToList();
            try
            {
                if(IsEdit)
                    await _treatmentService.UpdateTreatment(treatment);
                else
                    await _treatmentService.CreateNewTreatmentAsync(treatment);
                await OnClose.InvokeAsync();
                Navi.NavigateTo("/TreatmentOverview");
            }
            catch
            {

            }
        }










    }
}

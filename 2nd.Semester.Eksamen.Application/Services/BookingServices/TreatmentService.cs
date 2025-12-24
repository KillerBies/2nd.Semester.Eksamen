using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class TreatmentService : ITreatmentService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly Domain_to_DTO _domain_To_DTO;
        public TreatmentService(IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository, Domain_to_DTO domain_To_DTO)
        {
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
            _domain_To_DTO = domain_To_DTO;
        }

        public async Task<List<string>> GetAllUniqueSpecialtiesAsync()
        {


            var allSpecialties = await _employeeRepository.GetAllSpecialtiesAsync();
            return allSpecialties.Where(s => s != null).SelectMany(s => s.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Select(s => s.Trim()).Distinct().ToList();
        
        }
        public async Task CreateNewTreatmentAsync(TreatmentDTO treatmentDTO)
        {
           
            Treatment treatment = new Treatment(treatmentDTO.Name, treatmentDTO.BasePrice, treatmentDTO.Description, treatmentDTO.Category, treatmentDTO.Duration, treatmentDTO.RequiredSpecialties);
           
            await _treatmentRepository.CreateNewAsync(treatment);
        }
        public async Task UpdateTreatment(TreatmentDTO treatmentDTO)
        {
            Treatment treatment = new Treatment(treatmentDTO.Name, treatmentDTO.BasePrice, treatmentDTO.Description, treatmentDTO.Category, treatmentDTO.Duration, treatmentDTO.RequiredSpecialties) { Id=treatmentDTO.TreatmentId};
            await _treatmentRepository.UpdateAsync(treatment);
        }
        public async Task<List<TreatmentDTO>> GetAllTreatmentsAsDTOAsync()
        {
            
            List<TreatmentDTO> treatmentDTOs = new();
            var treatments = await _treatmentRepository.GetAllAsync();
            if (treatments == null)
            { return null; }
            foreach (var treatment in treatments)
            {
              TreatmentDTO dTO = _domain_To_DTO.TreatmentToDTO(treatment);
                treatmentDTOs.Add(dTO);
            }
            return treatmentDTOs;

        }
    
        public async Task DeleteByIdDbAsync(int id)
        {
            await _treatmentRepository.DeleteByIdAsync(id);
        }


    }
}

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
       
        public TreatmentService(IEmployeeRepository employeeRepository, ITreatmentRepository treatmentRepository)
        {
            _employeeRepository = employeeRepository;
            _treatmentRepository = treatmentRepository;
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
    }
}

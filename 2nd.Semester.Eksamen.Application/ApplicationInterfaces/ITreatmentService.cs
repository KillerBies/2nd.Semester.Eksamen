using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.DTO.ProductDTO.BookingDTO;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
namespace _2nd.Semester.Eksamen.Application.ApplicationInterfaces
{
    public interface ITreatmentService
    {




    public Task<List<string>> GetAllUniqueSpecialtiesAsync();
    public Task CreateNewTreatmentAsync(TreatmentDTO treatmentDTO);

    public Task <List<TreatmentDTO>> GetAllTreatmentsAsDTOAsync();
    public Task DeleteByIdDbAsync(int id);
    }
}

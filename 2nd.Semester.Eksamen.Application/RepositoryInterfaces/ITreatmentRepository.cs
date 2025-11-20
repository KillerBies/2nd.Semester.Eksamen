using Microsoft.Extensions.DependencyInjection;
using System;
using _2nd.Semester.Eksamen.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Products;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface ITreatmentRepository
    {
        //Repository for treatment data. Used to get info about a treatment (not a booked treatment).
        public Task<Treatment> GetByIDAsync(int id);
        public Task<IEnumerable<Treatment>> GetAllAsync();
        public Task<IEnumerable<Treatment>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Treatment treatment);
        public Task UpdateAsync(Treatment treatment);
        public Task DeleteAsync(Treatment treatment);

    }
}

using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface IBookingRepository
    {
        //Repository for Campaign. 
        public Task<Campaign?> GetByIDAsync(int id);
        public Task<IEnumerable<Campaign?>> GetAllAsync();
        public Task<IEnumerable<Campaign?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Campaign Campaign);
        public Task UpdateAsync(Campaign Campaign);
        public Task DeleteAsync(Campaign Campaign);
    }
}

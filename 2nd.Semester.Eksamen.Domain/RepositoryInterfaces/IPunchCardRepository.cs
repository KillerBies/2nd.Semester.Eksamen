using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface IPunchCardRepository
    {
        //Repository for PunchCards.
        public Task<PunchCard> GetByIDAsync(int id);
        public Task<PunchCard> GetByCustomerIDAsync(int id);
        public Task<IEnumerable<PunchCard>> GetAllAsync();
        public Task<IEnumerable<PunchCard>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(PunchCard PunchCard);
        public Task UpdateAsync(PunchCard PunchCard);
        public Task DeleteAsync(PunchCard PunchCard);
    }
}

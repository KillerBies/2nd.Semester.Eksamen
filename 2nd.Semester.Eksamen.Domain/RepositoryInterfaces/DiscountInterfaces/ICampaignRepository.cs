using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces
{
    public interface ICampaignRepository
    {
        //Repository for Campaign. 
        public Task<Campaign?> GetByIDAsync(int id);
        public Task<IEnumerable<Campaign?>> GetAllAsync();
        public Task<IEnumerable<Campaign?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Campaign campaign);
        public Task UpdateAsync(Campaign Campaign);
        public Task DeleteAsync(Campaign Campaign);
    }
}

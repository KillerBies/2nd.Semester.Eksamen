using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface ICampaignRepository
    {
        //Repository for Campaign. 
        public Task<Campaign?> GetByIDAsync(int id);
        public Task<IEnumerable<Campaign?>> GetAllAsync();
        public Task<IEnumerable<Campaign?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Campaign Campaign);
        public Task UpdateAsync(Campaign campaign);
        public Task DeleteAsync(Campaign campaign);
    }
}

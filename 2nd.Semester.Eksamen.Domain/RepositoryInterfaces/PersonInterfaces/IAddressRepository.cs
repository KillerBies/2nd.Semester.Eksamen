using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces
{
    public interface IAddressRepository
    {
        //Repository for Address. 
        public Task<Address?> GetByIDAsync(int id);
        public Task<IEnumerable<Address?>> GetAllAsync();
        public Task<IEnumerable<Address?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Address Address);
        public Task UpdateAsync(Address Address);
        public Task DeleteAsync(Address Address);
    }
}

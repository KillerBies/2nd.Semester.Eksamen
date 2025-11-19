using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Application.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        //Repository for Customer. 
        public Task<Customer?> GetByIDAsync(int id);
        public Task<IEnumerable<Customer?>> GetAllAsync();
        public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(Customer Customer);
        public Task UpdateAsync(Customer Customer);
        public Task DeleteAsync(Customer Customer);
    }
}

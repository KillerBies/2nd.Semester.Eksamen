using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetByIDAsync(int id);
        public Task<IEnumerable<Customer?>> GetAllAsync();
        public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task CreateNewCustomerAsync(Customer Customer);
        public Task UpdateCustomerAsync(Customer Customer);
        public Task DeleteCustomerAsync(Customer Customer);
    }
}

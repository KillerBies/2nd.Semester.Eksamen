using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces
{
    public interface ICompanyCustomerRepository
    {
        //Repository for Customer. 
        public Task<Customer?> GetByIDAsync(int id);
        //public Task<IEnumerable<Customer?>> GetAllAsync();
        //public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(CompanyCustomer Customer);
        public Task<bool> PhoneAlreadyExistsAsync(string PhoneNumber);
        //public Task UpdateAsync(CompanyCustomer Customer);
        //public Task DeleteAsync(CompanyCustomer Customer);
    }
}

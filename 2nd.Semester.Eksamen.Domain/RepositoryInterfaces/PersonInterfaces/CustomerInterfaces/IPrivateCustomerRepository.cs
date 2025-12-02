using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces
{
    public interface IPrivateCustomerRepository
    {
        //Repository for Customer. 
        public Task<PrivateCustomer?> GetByIDAsync(int id);
        public Task <PrivateCustomer?> GetByPhoneAsync(string phoneNumber);
        //public Task<IEnumerable<Customer?>> GetAllAsync();
        //public Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter);
        public Task CreateNewAsync(PrivateCustomer Customer);
        public Task<bool> PhoneAlreadyExistsAsync(string PhoneNumber);
        //public Task UpdateAsync(PrivateCustomer Customer);
        public Task DeleteAsync(PrivateCustomer Customer);
        public Task UpdateAsync(PrivateCustomer Customer);

    }
}

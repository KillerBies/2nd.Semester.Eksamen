using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces
{
    public interface ICustomerQueryRepository
    {
        public Task<Customer?> GetByPhoneNumberAsync(string PhoneNumber);
        public Task<Customer?> GetByIDAsync(Guid id);
        public Task<IEnumerable<Customer>?> GetAllCustomersAsync();
    }
}

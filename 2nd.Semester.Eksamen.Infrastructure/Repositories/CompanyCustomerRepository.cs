using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class CompanyCustomerRepository : ICompanyCustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CompanyCustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //public async Task<Customer?> GetByIDAsync(int id)
        //{

        //}
        //public async Task<IEnumerable<Customer?>> GetAllAsync()
        //{

        //}
        //public async Task<IEnumerable<Customer?>> GetByFilterAsync(Filter filter)
        //{

        //}

        public async Task CreateNewAsync(CompanyCustomer customer)
        {


            //Adds Customer to Customers table in Database.
            _dbContext.CompanyCustomers.Add(customer);
            await _dbContext.SaveChangesAsync();


        }
        //public async Task UpdateAsync(PrivateCustomer Customer)
        //{

        //}
        //public async Task DeleteAsync(PrivateCustomer Customer)
        //{
        //}


    }
}

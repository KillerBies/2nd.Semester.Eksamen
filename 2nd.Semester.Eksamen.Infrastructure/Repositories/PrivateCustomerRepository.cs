using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories
{
    public class PrivateCustomerRepository : IPrivateCustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public PrivateCustomerRepository(AppDbContext dbContext)
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
       
        public async Task CreateNewAsync(PrivateCustomer customer)
        {
         

            //Adds Customer to PrivateCustomers table in Database.
            _dbContext.PrivateCustomers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> PhoneAlreadyExistsAsync(string phone)
        {
            return await _dbContext.PrivateCustomers.AnyAsync(c => c.PhoneNumber == phone);
        }
        //public async Task UpdateAsync(PrivateCustomer Customer)
        //{

        //}
        //public async Task DeleteAsync(PrivateCustomer Customer)
        //{
        //}


    }
}

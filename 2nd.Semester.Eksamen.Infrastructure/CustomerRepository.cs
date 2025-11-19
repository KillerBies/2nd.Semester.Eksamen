using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Application.Interfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;

namespace _2nd.Semester.Eksamen.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        
        public async Task CreateNewCustomer(Customer customer)
        {
            //Adds Customer to Customers table in Database.
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            
        }


    }
}

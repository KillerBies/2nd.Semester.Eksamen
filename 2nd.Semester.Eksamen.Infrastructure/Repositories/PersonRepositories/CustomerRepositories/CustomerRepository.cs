using _2nd.Semester.Eksamen.Application.DTO.PersonDTO.CustomersDTO;
using _2nd.Semester.Eksamen.Domain;
using _2nd.Semester.Eksamen.Domain.Entities.Discounts;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public CustomerRepository(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        // ================= CREATE =================
        async Task ICustomerRepository.AddNewCustomerAsync(Customer customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception("Noget gik galt og kunden kunne ikke oprettes");
            }
        }
        // ================= UPDATE =================
        async Task ICustomerRepository.UpdateCustomerAsync(Customer Customer)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var adressToUpdate = await _context.Adresses.FindAsync(Customer.AddressId);
                if (adressToUpdate != null)
                {
                    adressToUpdate.UpdateStreetName(Customer.Address.StreetName);
                    adressToUpdate.UpdatePostalCode(Customer.Address.PostalCode);
                    adressToUpdate.UpdateHouseNumber(Customer.Address.HouseNumber);
                    adressToUpdate.UpdateCity(Customer.Address.City);
                }
                if (Customer is PrivateCustomer pc)
                {
                    var customerToUpDate = await _context.PrivateCustomers.FirstOrDefaultAsync(c => c.Id == Customer.Id);
                    if (customerToUpDate != null)
                    {
                        customerToUpDate.NumberOfVisists = Customer.NumberOfVisists;
                        customerToUpDate.TrySetPhoneNumber(pc.PhoneNumber);
                        customerToUpDate.TrySetLastName(pc.Name, pc.LastName);
                        customerToUpDate.SetBirthDate(pc.BirthDate, (DateTime.Today.Year - pc.BirthDate.Year));
                        customerToUpDate.Email = pc.Email;
                        customerToUpDate.Gender = pc.Gender;
                        customerToUpDate.Notes = pc.Notes;
                        customerToUpDate.SaveAsCustomer = pc.SaveAsCustomer;
                    }
                }
                else if (Customer is CompanyCustomer cc)
                {
                    var customerToUpDate = await _context.CompanyCustomers.FirstOrDefaultAsync(c => c.Id == Customer.Id);
                    if (customerToUpDate != null)
                    {
                        customerToUpDate.NumberOfVisists = Customer.NumberOfVisists;
                        customerToUpDate.TrySetPhoneNumber(cc.PhoneNumber);
                        customerToUpDate.Email = cc.Email;
                        customerToUpDate.Notes = cc.Notes;
                        customerToUpDate.TrySetCVRNumber(cc.CVRNumber);
                        customerToUpDate.SaveAsCustomer = cc.SaveAsCustomer;
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        // ================= DELETE =================
        async Task ICustomerRepository.DeleteCustomerByIdDbAsync(Guid id)
        {
            var _context = await _factory.CreateDbContextAsync();
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                    throw new ArgumentNullException("Denne Kunde kunne ikke findes");
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

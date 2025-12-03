using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;
using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;

namespace _2nd.Semester.Eksamen.Domain.Entities.History
{
    public record CustomerSnapshot : BaseSnapshot
    {
        public string Name { get; init; }
        public Address Address { get; init; }
        public string PhoneNumber { get; init; }
        public CustomerSnapshot() { }
        public CustomerSnapshot(Customer customer)
        {
            Name = customer.Name;
            Address = customer.Address;
            PhoneNumber = customer.PhoneNumber;

        }
        
        public static CustomerSnapshot CreateCustomerSnapshot(Customer customer)
        {
            return customer switch
            {
                CompanyCustomer cc => new CompanyCustomerSnapshot(cc),
                PrivateCustomer pc => new PrivateCustomerSnapshot(pc),
                _ => new CustomerSnapshot(customer)
            };
        }


    }


    public record CompanyCustomerSnapshot : CustomerSnapshot
    {

        public string? CVR { get; init; }

        public CompanyCustomerSnapshot() { }
        public CompanyCustomerSnapshot(CompanyCustomer companyCustomer) : base(companyCustomer)
        {
            CVR = companyCustomer.CVRNumber;
        }
    }

    public record PrivateCustomerSnapshot : CustomerSnapshot
    {
        public PrivateCustomerSnapshot() { }
        public string LastName { get; init; }


        public PrivateCustomerSnapshot(PrivateCustomer privateCustomer) : base(privateCustomer) 
        {
            LastName = privateCustomer.LastName;
        }

    }

}

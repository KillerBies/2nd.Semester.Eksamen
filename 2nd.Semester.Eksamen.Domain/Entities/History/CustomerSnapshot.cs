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
        public string Name { get; private set; }
        public AddressSnapshot AddressSnapshot { get; private set; }
        public string PhoneNumber { get; private set; }
        public int BookingSnapshotId { get; set; }
        public BookingSnapshot BookingSnapshot { get; set; }

        protected CustomerSnapshot() { }
        public CustomerSnapshot(Customer customer)
        {
            Name = customer.Name;
            AddressSnapshot = new AddressSnapshot(customer.Address);
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

        public string? CVR { get; set; }

        protected CompanyCustomerSnapshot() { }
        public CompanyCustomerSnapshot(CompanyCustomer companyCustomer) : base(companyCustomer)
        {
            CVR = companyCustomer.CVRNumber;
        }
    }

    public record PrivateCustomerSnapshot : CustomerSnapshot
    {
        protected PrivateCustomerSnapshot() { }
        public string LastName { get; set; }


        public PrivateCustomerSnapshot(PrivateCustomer privateCustomer) : base(privateCustomer) 
        {
            LastName = privateCustomer.LastName;
        }

    }

}

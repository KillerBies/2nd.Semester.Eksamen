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
        //Customer Info
        public string Name { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public int AddressSnapshotId { get; protected set; }
        public AddressSnapshot AddressSnapshot { get; protected set; }
        

        //Connected Booking
        public Guid? BookingSnapShotId { get; protected set; }
        public BookingSnapshot BookingSnapshot { get; protected set; }

        public CustomerSnapshot() { }
        public CustomerSnapshot(Customer customer) : base(customer.RefrenceId)
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

        public string? CVR { get; protected set; }

        protected CompanyCustomerSnapshot() { }
        public CompanyCustomerSnapshot(CompanyCustomer companyCustomer) : base(companyCustomer)
        {
            CVR = companyCustomer.CVRNumber;
        }
    }

    public record PrivateCustomerSnapshot : CustomerSnapshot
    {
        protected PrivateCustomerSnapshot() { }
        public string LastName { get; protected set; }


        public PrivateCustomerSnapshot(PrivateCustomer privateCustomer) : base(privateCustomer) 
        {
            LastName = privateCustomer.LastName;
        }

    }

}

using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class CompanyCustomer: Customer
    {
        //Customer who is a company
        public string CVRNumber { get; private set; }  = null!;

        public CompanyCustomer(string name, string cvrnumber, Address address, string phonenumber, string email, bool saveAsCustomer) : base(name, address, phonenumber, email, saveAsCustomer)
        {
            TrySetCVRNumber(cvrnumber);
        }
        public CompanyCustomer()
        {
        }

        //method to set CVR number
        public bool TrySetCVRNumber(string cvrnumber)
        {
            if(cvrnumber.Trim().All(char.IsDigit) && cvrnumber.Trim().Length == 8) //checks if CVR number (without empty space) only contains digits and is 8 digits long
            {
                CVRNumber = cvrnumber.Trim();
                return true;
            }
            return false;
        }


    }
}

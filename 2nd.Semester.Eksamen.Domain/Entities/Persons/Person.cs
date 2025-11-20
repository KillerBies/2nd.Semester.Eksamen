using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public abstract class Person : BaseEntity
    {
        //Basic elements of a person
        public string Name { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;
        public string Email { get; private set; } = null!;


        public Person() { }
        public Person(string name, Address address, string phoneNumber, string email)
        {
            Name = name;
          //  TrySetName(name);
            Address = address;
            TrySetPhoneNumber(phoneNumber);
            TrySetEmail(email);
        }




        //method to change name of person
        public bool TrySetName(string name)
        {
            if(NameCheck(name)) //checks if name (without special characters) only contains letters
            {
                Name = name.Trim(); //sets name to name without empty space at start and end
                return true;
            }
            return false;
        }

        //method to check if name is valid
        protected bool NameCheck(string name) //protected so it can be used in derived classes
        {
            return name.Trim(new char[] { ' ', '-', '.', '\'' }).All(char.IsLetter); //checks if name (without special characters) only contains letters
        }


        //method to set phone number of person
        public bool TrySetPhoneNumber(string phoneNumber)
        {
            if(phoneNumber.Trim().All(char.IsDigit) && phoneNumber.Trim().Length == 8) //checks if phonenumber (without empty space) only contains digits and is 8 digits long
            {
                PhoneNumber = phoneNumber.Trim();
                return true;
            }
            return false;
        }




        //method to set email of person
        public bool TrySetEmail(string email)
        {
            if(email.Contains("@") && email.Contains(".") && !email.Contains(" ")) //basic check if email contains @ and . 
            {
                Email = email.Trim();
                return true;
            }
            return false;
        }
    }
}

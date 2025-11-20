using _2nd.Semester.Eksamen.Domain.Entities.Tilbud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Persons
{
    public class PrivateCustomer : Customer
    {
        //Customer who is a private person
        public string LastName { get; private set; } = null!;
        public Gender? Gender { get; set; }
        public int Age { get { return GetAge(); } }
        public DateOnly BirthDate { get;  set; }
        



        public PrivateCustomer(string lastname, Gender gender, DateOnly birthday, string name, Address address,string phonenumber,string email) : base(name,address,phonenumber,email)
    {
            TrySetLastName(name, lastname);
            Gender = gender;
            Birthday = birthday;
           // SetBirthDate(birthday, age);
        }
        public PrivateCustomer()
        {
        }
               




        //method to set last and first name of private person
        public bool TrySetLastName(string firstname, string lastname)
        {
            TrySetName(firstname); //sets firstname of person class
            if (NameCheck(lastname))
            {
                LastName = lastname.Trim();
                return true;
    }
            return false;
    }


        //method to get age 
        protected int GetAge()
        {
            //if age is just a number it can become out of date so we calculate it from birthdate
            DateTime CurrentTime = DateTime.Now;
            int age = CurrentTime.Year - BirthDate.Year;
            if (CurrentTime.Month < BirthDate.Month || CurrentTime.Month == BirthDate.Month && CurrentTime.Day < BirthDate.Day)//birthdate has yet accured this year subtract 1 year from age
            {
                age--;
            }
            return age;
        }

        //method to set birthdate
        public void SetBirthDate(DateOnly birthday, int age)
        {
            //Customer may not want to give their exact birthdate so we set it by giving birthday and age
            BirthDate = birthday.AddYears(-age);
        }
    }
}

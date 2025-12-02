namespace _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer
{
    public class CompanyCustomer : Customer
    {
        //Customer who is a company
        public string CVRNumber { get; private set; } = null!;

        public CompanyCustomer(string name, string cvrnumber, Address address, string phonenumber, string email, string notes, bool saveAsCustomer) : base(name, address, phonenumber, email, notes, saveAsCustomer)
        {
            TrySetCVRNumber(cvrnumber);
        }
        public CompanyCustomer()
        {
        }

        //method to set CVR number
        public bool TrySetCVRNumber(string cvrnumber)
        {
            if (cvrnumber.Trim().All(char.IsDigit) && cvrnumber.Trim().Length == 8) //checks if CVR number (without empty space) only contains digits and is 8 digits long
            {
                CVRNumber = cvrnumber.Trim();
                return true;
            }
            return false;
        }


    }
}

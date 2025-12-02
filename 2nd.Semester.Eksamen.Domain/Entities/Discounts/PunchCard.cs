using _2nd.Semester.Eksamen.Domain.Entities.Persons.Customer;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts.TreatmentProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Discounts
{
    public class PunchCard : BaseEntity
    {
        public Customer Customer { get; private set; } = null!; //Customer who owns the punch card
        public int PunchNumber { get; private set; } = 0; //Number of punches on the card
        public Treatment Treatment { get; private set; } = null!; //Type of treatment the punch card is for
        public int FreeTreatments { get; private set; } = 0; //Number of free treatments earned

        public PunchCard(Customer customer, int punchNumber, Treatment treatment) 
        { 
            Customer = customer;
            PunchNumber = punchNumber;
            Treatment = treatment;
        }
        public PunchCard() { }




        //method to add a punch mark and check for free treatments 
        public void AddPunch()
        {
            PunchNumber++;
            Check();
        }

        //method to check if a free treatment is earned
        protected void Check()
        {
            if(PunchNumber%10 == 0 && PunchNumber != 0)
            {
                FreeTreatments++;
            }
        }
        public bool TryRedeemFreeTreatment()
        {
            if(FreeTreatments > 0)
            {
                FreeTreatments--;
                return true;
            }
            return false;
        }
    }
}

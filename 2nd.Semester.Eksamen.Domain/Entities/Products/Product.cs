
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2nd.Semester.Eksamen.Domain.Entities.Products
{
    public class Product : BaseEntity 
    {
        //Product details
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public Product(string name, decimal price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
        public Product() { }











        //Method to change the name of the product if the new name is not null or whitespace
        public bool TryChangeName(string newName)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                Name = newName;
                return true;
            }
            return false;
        }




        //Method to change the price of the product if the new price is greater than 0
        public bool TryChangePrice(decimal newPrice)
        {
            if (newPrice > 0)
            {
                Price = newPrice;
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructDeconstruct.Classes
{
    public class Product
    {
        public Product(string name, float price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Product(string name, float price)
        {
            Name = name;
            Price = price;
            Quantity = 0;
        }

        public void Deconstruct(out string name, out double price, out int quantity)
        {
            name = Name;
            price = Price;
            quantity = Quantity;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}

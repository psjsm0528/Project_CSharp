using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Class
{
    public class Customer
    {
        public string Name { get; set; }
        public string Grade { get; set; }
        public int Money { get; set; }
        public Dictionary<string, int> PreferredPrices { get; set; }

        public Customer(string name, string grade, int money, Dictionary<string, int> preferredPrices)
        {
            Name = name;
            Grade = grade;
            Money = money;
            PreferredPrices = preferredPrices;
        }
    }
}

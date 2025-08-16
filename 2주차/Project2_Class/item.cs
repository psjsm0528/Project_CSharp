using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Class
{
    class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int BuyPrice { get; set; }

        public Item(string name, string type, int buyPrice)
        {
            Name = name;
            Type = type;
            BuyPrice = buyPrice;
        }
    }
}
        
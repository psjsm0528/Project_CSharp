using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Class
{
     class Player
    {
        public int Money { get; set; }
        public int DebtRepaymentTerm { get; set; }
        public List<Customer> MetCustomers { get; set; }
        public List<Item> Inventory { get; private set; }

        public Player(int initialMoney, int initialDebtTerm)
        {
            Money = initialMoney;
            DebtRepaymentTerm = initialDebtTerm;
            MetCustomers = new List<Customer>();
            Inventory = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public void RemoveItem(string itemName)
        {
            var itemToRemove = Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                Inventory.Remove(itemToRemove);
            }
        }
    }
}

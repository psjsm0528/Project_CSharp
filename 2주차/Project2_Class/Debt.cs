using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Class
{
    internal class Debt
    {
        public int Amount { get; set; }
        public bool IsEventDebt { get; set; }

        public Debt(int amount, bool isEventDebt = false)
        {
            Amount = amount;
            IsEventDebt = isEventDebt;
        }
    }
}

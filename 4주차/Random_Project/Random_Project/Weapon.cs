using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Project
{
    internal class Weapon : Item
    {
        int situation;
        public int Situation => situation;

        public Weapon(ItemRank rank, string name, int situation) : base(rank, name)
        {
            this.situation = situation;
        }
    }
}

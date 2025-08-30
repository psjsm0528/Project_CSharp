using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Project
{
    internal class Character : Item
    {
        int age;

        public int Age => age;

        public Character(ItemRank rank, string name, int age) : base(rank, name)
        {
            this.age = age;
        }
    }
}

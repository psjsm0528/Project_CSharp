using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_Project
{
    class Gatcha
    {
        //랜덤이란 임의의 숫자를 반환하는 계념입니다.
        //0~10 사이의 숫자를 전달해서 그 숫자하나를 출력하는 코드.

        // seed 계념
        // 랜덤 최초의 값에 따라서 그 다음 결과가 결정이 되어진다.
        // 수도 랜덤 (Pseudo Random) - 알고리즘에 의해서 결과가 정해진 랜덤이다.

        /*
         *  랜덤 - 실행할 때 마다 다른 값을 생성하고 있다.
         *  - 실행할때 동일한 결과를 생성할 수 있는 기능을 제공한다.
         *  - 랜덤을 생성할 때 동일한 seed를 실행하면 같은 결과가 나타난다.
         *  - 게임에서 사용되는 랜덤이 재대로 작동하는지 테스트를 하기 위해서.
         */

        int seed = Environment.TickCount;
        Random random = new Random();
        List<Item> gatchaTable = new List<Item>();
        float[] probabilityByRank = { 50, 30, 10, 5, 3, 2 };
        float[] symPrebability;
        List<Item>[] itemRanks = new List<Item>[6];


        public void GenerateProbability()
        {
            float totalPro = probabilityByRank.Sum();
            symPrebability = new float[probabilityByRank.Length];
            float cumValue = 0;

            for (int i=0; i < probabilityByRank.Length; i++)
            {
                cumValue += probabilityByRank[i];
                symPrebability[i] = cumValue;
            }
        }
        public void GenerateSeed()
        {
            seed = Environment.TickCount;
            Console.WriteLine("Seed 값 :" +  seed);
        }
        public void GenerateRandom()
        {
            random = new Random(seed);
        }
        // 가차를 뽑을 확률, 만들어서 특정 확률의 아이템을 뽑았을 때 그 아이템이 뽑히게 하고 싶다.
        // 6개의 등급에 확률 6개, 100% (10%, 25%, 15%, 10%, 10%, 30%)
        public void SetTable()
        {
            gatchaTable.Add(new Weapon(ItemRank.D, "smoke shell",10));
            gatchaTable.Add(new Weapon(ItemRank.D, "grenade",10));
            gatchaTable.Add(new Weapon(ItemRank.C, "shotgun",20));
            gatchaTable.Add(new Weapon(ItemRank.C, "fist",20));
            gatchaTable.Add(new Weapon(ItemRank.B, "RPG",30));
            gatchaTable.Add(new Weapon(ItemRank.B, "flash grenade",30));
            gatchaTable.Add(new Weapon(ItemRank.A, "Sniper", 40));
            gatchaTable.Add(new Weapon(ItemRank.A, "assault rifle", 40));
            gatchaTable.Add(new Weapon(ItemRank.S, "Ice ray", 50));
            gatchaTable.Add(new Weapon(ItemRank.S, "bow", 50));
            gatchaTable.Add(new Weapon(ItemRank.SS, "Flamethrower",60));
            gatchaTable.Add(new Weapon(ItemRank.SS, "Minigun",60));

            itemRanks[0] = gatchaTable.Where(rank => rank.Rank == ItemRank.D).ToList();
            itemRanks[1] = gatchaTable.Where(rank => rank.Rank == ItemRank.C).ToList();
            itemRanks[2] = gatchaTable.Where(rank => rank.Rank == ItemRank.B).ToList();
            itemRanks[3] = gatchaTable.Where(rank => rank.Rank == ItemRank.A).ToList();
            itemRanks[4] = gatchaTable.Where(rank => rank.Rank == ItemRank.S).ToList();
            itemRanks[5] = gatchaTable.Where(rank => rank.Rank == ItemRank.SS).ToList();
        }
        public List<Item> PickRank()
        {
            float rand = (float)random.NextDouble() * 100f;
            
            for(int i=0; i< symPrebability.Length; i++)
            {
                if(rand < symPrebability[i])
                {
                    return itemRanks[i];
                }
            }
            return itemRanks[ symPrebability.Length - 1];
        }
        public void pick()
        {
            // 내가 뽑은 가챠의 등급을 먼저 뽑는다.
            List<Item> pickeditem = PickRank();
           int rand = random.Next(0, pickeditem.Count);
            //Console.WriteLine(" 랜덤 값 " + rand);

            if(pickeditem[rand] is Weapon)
            {
                Console.WriteLine(" 랭크" + pickeditem[rand].Rank + " 이름" + pickeditem[rand].Name+" 품질 " + (pickeditem[rand] as Weapon).Situation);
            }
            else
            {
                Console.WriteLine(" 랭크" + pickeditem[rand].Rank + " 이름" + pickeditem[rand].Name);
            }
        }

        public void Pick10Time()
        {
            for (int i=0; i < 10; i++)
            {
                pick();
            }
        }
    }
}

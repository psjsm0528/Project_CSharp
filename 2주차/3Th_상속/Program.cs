namespace _3Th_상속
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 게임에 등장할 클래스를 만들어 봅시다.

            // 아이템 클래스 여러분들이 생성해보세요.
            // AI를 사용하여서, 아이템 이름, 가격, 아이템의 타입을 분류를 해보세요.
            // 어떤 분류인가? (포션, 장착 가능한것, 연금 재료 ....... )
            // 열거형 enum 사용하여서 Item 클래스를 생성하면 됩니다.
            // 14:15분 까지 Item 클래스를 위의 내용으로 생성해보세요.

            // 이 클래스를 재사용하는 방법을 베워봅니다.

            Potion bluePotion = new Potion("블루 포션", 100, ItemType.Potion);
            Potion redPotion = new Potion("빨간 포션", 100, ItemType.Potion);
            Weapon sword = new Weapon("칼", 500, ItemType.Weapon);
            Weapon gun = new Weapon("총", 700, ItemType.Weapon);
            Armor plateArmor = new Armor("판금 감옷", 1500, ItemType.Armor);
            Weapon scissors = new Weapon("가위", 300, ItemType.Weapon);

            List<Item> inventory = new List<Item>();
            inventory.Add(bluePotion);
            inventory.Add(redPotion);
            inventory.Add(sword);
            inventory.Add(gun);
            inventory.Add(plateArmor);
            inventory.Add(scissors);
            // 위의 Item 여러분들이 직접 생성한 아이템 데이터입니다. 게임 (콘솔)에서 사용할 수 잇는 클래스를 만들어 봅시다.

            // ItemManager, Player UseItem

            // 클래스의 다형성
            Player player = new Player();

            foreach(var item in inventory)
            {
                player.UseItem(item);
            }
            //player.UseItem(bluePotion);
            //player.UseItem(redPotion);
            //player.UseItem(sword);
            //player.UseItem(gun);
            //player.UseItem(plateArmor);
            //player.UseItem(scissors);
        }

        class Player
        {
            public void UseItem(Item item)
            {
                
                item.Use();
            }
        }
    }

    
}

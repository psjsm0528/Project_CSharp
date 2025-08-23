using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace _3Th_상속
{
    // ItemType 열거형 정의
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        Consumable,
    }

    // Item 클래스 정의
    public class Item
    {
        // 필드
        public string ItemName;
        public int Price;
        public ItemType Type;

        // 생성자 (필드를 초기화)
        public Item(string itemName, int price, ItemType type)
        {
            ItemName = itemName;
            Price = price;
            Type = type;
        }

        // 아이템 정보를 출력하는 메서드 (선택 사항)
        public void DisplayItemInfo()
        {
            Console.WriteLine($"아이템 이름: {ItemName}");
            Console.WriteLine($"가격: {Price}");
            Console.WriteLine($"타입: {Type}");
        }



        public virtual void Use()
        {
            Console.WriteLine($" {ItemName} - 가격 : {Price} - 아이템 분류 : {Type} ");

        }
    }

    // 포션 클래스는 Item의 기능을 성속받겠다. (상속 , inheritance)
    class Potion : Item
    {
        public Potion(string itemName, int price, ItemType type) : base(itemName, price, type)
        {
        }

        public override void Use()
        {
            base.Use();
            Console.WriteLine("포션을 사용했습니다");
        }
    }
    class Weapon : Item
    {
        public Weapon(string itemName, int price, ItemType type) : base(itemName, price, type)
        {
        }

        public override void Use()
        {
            base.Use();
            Console.WriteLine("무기를 사용했습니다");
        }
    }
    class Armor : Item
    {
        public Armor(string itemName, int price, ItemType type) : base(itemName, price, type)
        {
        }

        public override void Use()
        {
            base.Use();
            Console.WriteLine("갑옷를 사용했습니다");
        }
    }
    class Consumable : Item
    {
        public Consumable(string itemName, int price, ItemType type) : base(itemName, price, type)
        {
        }

        public override void Use()
        {
            base.Use();
            Console.WriteLine("가위");
        }
    }
}
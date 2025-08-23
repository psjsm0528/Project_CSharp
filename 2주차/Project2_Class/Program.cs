namespace Project2_Class
{
    internal class Program
    {
        private static Player player;
        private static Debt playerDebt;
        private static List<Customer> customers;
        private static List<Item> availableItems;
        private static Random random = new Random();
        static void Main(string[] args)
        {
            // 1. 게임을 ai로 만들기 (복습)
            // 아무 ai 사이트에 들어가서 만들고 싶은 게임을 검색하는 방법
            // C#으로 만들어라. 구체적인 정보를 전달해야 ai 원하는 결과물을 도출할 수 있다.
            // 테트리스  게임, 블록 4개, .. 조권 만들어줘. + 함수, 변수

            // 게임을 만들어 달라고 질문했을 때의 단점
            // - 여러분이 게임에 추가하고 싶은 콘텐츠를 스스로 추가하기가 어렵다.
            // 남이 만들어 놓은 규칙에서 추가해야 되기 때문에 (작성된 코드를 이해를 하지 못한 경우)
            // 프로그래밍 언어 문법이 존재하는데, 이 문법을 이해하지 못하고 사용하면 에러가 발생한다.( 네이밍 문제 )

            // 2. 게임의 구성 요소를 ai에게 작성해달라고 요청해보기.
            // 게임을 작은 구성 요소를 나누어야 할 필요성을 느끼셨다면..
            // 어떻게 나눌 것인가? -> Class 객체 지향 프로그래밍
            // class를 생성하는 것. 클래스의 관계를 설계하는 것. 
            // 플레이어 ( player) , 목적, 방해 요소(enemy, trap, environment)

            // 플레이어가 처음에 소지금을 가지고 게임을 시작합니다. 이 플레이어는 특정 기간마다 빚을 변제해야 합니다.
            // 플레이어는 아이템을 특정한 소비자에게 판매할 수 있습니다. 소비자는 아이템의 선호도가 존재해서, 소비자만 판매할 수 있는
            // 금액이 다릅니다. (정보) 위의 내용으로 게임을 만든다고 가정했을 때, 이 게임에 필요한 클래스를 아래에 정의를 해보세요.
            // 객체 : 플레이어 (소지금, 남은 빚 변제 기간, 만난 고객 정보)
            // 빚  ( 수치, 이벤트)
            // 고객 (등급, 소지금)
            // 아이템 (고객마다 원하는 종류가 다양하다)

            // 3. 게임에 등장할 요소(클래스) 어느 정도 구현하였으면, 게임에 등장시켜야 합니다.
            // 어디에 그 코드를 작성하는가? main함수에서 코드가 실행된다. 만들어 놓은 클래스를 이 함수에서 다시 호출한다.
            // 4가지 클래스를 사용해서 게임을 메인함수에 플레이가 되도록 만들어줘.

            Console.Title = "비즈니스 시뮬레이션 게임";
            InitializeGame();
            StartGameLoop();
            EndGame();
        }

        private static void InitializeGame()
        {
            player = new Player(1000, 10);
            playerDebt = new Debt(5000);
            player.MetCustomers.AddRange(new List<Customer>
        {
            new Customer("김철수", "일반", 10000, new Dictionary<string, int> { { "과자", 1500 }, { "음료수", 2000 } }),
            new Customer("박영희", "VIP", 20000, new Dictionary<string, int> { { "음료수", 3000 }, { "사탕", 2500 } }),
        });

            availableItems = new List<Item>
        {
            new Item("과자", "식료품", 500),
            new Item("음료수", "음료", 800),
            new Item("사탕", "식료품", 300),
            new HighItem("장난감", "잡화", 1500, 2)
        };

            // 아이템의 실제 구매 가격은 게임 시작 시 한 번 결정
            UpdateMarketPrices();

            Console.Clear();
            Console.WriteLine("🎉 업그레이드된 비즈니스 시뮬레이션에 오신 것을 환영합니다! 🎉");
            Console.WriteLine($"소지금: {player.Money}원 | 빚: {playerDebt.Amount}원");
            Console.WriteLine($"남은 변제 기간: {player.DebtRepaymentTerm}일");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("엔터 키를 눌러 게임을 시작하세요.");
            Console.ReadKey();
        }

        private static void StartGameLoop()
        {
            while (player.DebtRepaymentTerm > 0 && player.Money > 0 && playerDebt.Amount > 0)
            {
                Console.Clear();
                Console.WriteLine($"--- 남은 변제 기간: {player.DebtRepaymentTerm}일 ---");
                Console.WriteLine($"소지금: {player.Money}원 | 남은 빚: {playerDebt.Amount}원");
                Console.WriteLine("---------------------------------------------");

                // 턴 시작 시 이벤트 발생
                CheckForRandomEvent();
                // 시장 가격 변동
                UpdateMarketPrices();

                // 이번 턴에 만날 고객 목록 업데이트
                List<Customer> currentTurnCustomers = new List<Customer>();
                currentTurnCustomers.Add(player.MetCustomers[random.Next(player.MetCustomers.Count)]);
                if (random.Next(10) < 5) // 50% 확률로 새로운 고객 추가
                {
                    currentTurnCustomers.Add(new Customer($"새로운 고객 {random.Next(1, 100)}", "일반", random.Next(5000, 15000), GetRandomPreferredPrices()));
                }

                // 턴별 활동 선택
                Console.WriteLine("1. 아이템 구매 | 2. 아이템 판매 | 3. 빚 갚기 | 4. 다음 날");
                var choice = Console.ReadKey(true).KeyChar;

                switch (choice)
                {
                    case '1':
                        PurchaseItem();
                        break;
                    case '2':
                        SellItem(currentTurnCustomers);
                        break;
                    case '3':
                        PayDebt();
                        break;
                    case '4':
                        Console.WriteLine("\n다음 날로 넘어갑니다.");
                        break;
                    default:
                        Console.WriteLine("유효하지 않은 입력입니다. 다시 선택하세요.");
                        break;
                }

                player.DebtRepaymentTerm--;
                Console.WriteLine("\n아무 키나 눌러 다음 날로...");
                Console.ReadKey();
            }
        }
        
        private static void UpdateMarketPrices()
        {
            foreach (var item in availableItems)
            {
                int priceChange = random.Next(-100, 101); // -100 ~ +100 가격 변동
                item.BuyPrice = Math.Max(100, item.BuyPrice + priceChange); // 최소 가격 100원
            }
        }

        private static void CheckForRandomEvent()
        {
            if (random.Next(10) < 2) // 20% 확률로 이벤트 발생
            {
                int eventDebtAmount = random.Next(1000, 3001);
                playerDebt.Amount += eventDebtAmount;
                Console.WriteLine($"🚨 돌발 이벤트 발생! 급한 지출로 빚이 {eventDebtAmount}원 늘었습니다!");
                playerDebt.IsEventDebt = true;
            }
            else
            {
                playerDebt.IsEventDebt = false;
            }
        }

        private static void PurchaseItem()
        {
            Console.Clear();
            Console.WriteLine("--- 아이템 구매 ---");
            Console.WriteLine("현재 소지금: " + player.Money);
            Console.WriteLine($"소지 아이템 수: {player.Inventory.Count} / 10");
            Console.WriteLine("---------------------------------------------");

            if (player.Inventory.Count >= 10)
            {
                Console.WriteLine("인벤토리가 가득 찼습니다! 아이템을 더 이상 구매할 수 없습니다.");
                return;
            }

            for (int i = 0; i < availableItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableItems[i].Name} - {availableItems[i].BuyPrice}원");
            }
            Console.WriteLine("구매할 아이템 번호를 입력하세요 (나가기: 0):");

            if (int.TryParse(Console.ReadLine(), out int itemIndex) && itemIndex > 0 && itemIndex <= availableItems.Count)
            {
                var itemToBuy = availableItems[itemIndex - 1];
                if (player.Money >= itemToBuy.BuyPrice)
                {
                    player.Money -= itemToBuy.BuyPrice;
                    player.AddItem(itemToBuy);
                    Console.WriteLine($"'{itemToBuy.Name}'을(를) 구매했습니다. 남은 돈: {player.Money}");
                }
                else
                {
                    Console.WriteLine("💰 소지금이 부족합니다.");
                }
            }
        }

        private static void SellItem(List<Customer> currentTurnCustomers)
        {
            Console.Clear();
            Console.WriteLine("--- 아이템 판매 ---");
            if (!player.Inventory.Any())
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                return;
            }

            Console.WriteLine("소지 아이템:");
            foreach (var item in player.Inventory.GroupBy(x => x.Name))
            {
                Console.WriteLine($"- {item.Key} x {item.Count()}개");
            }

            Console.WriteLine("\n오늘 만난 고객:");
            for (int i = 0; i < currentTurnCustomers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {currentTurnCustomers[i].Name} ({currentTurnCustomers[i].Grade})");
            }

            Console.WriteLine("\n판매할 아이템 이름을 입력하세요:");
            string itemName = Console.ReadLine();
            var sellItem = player.Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (sellItem != null)
            {
                Console.WriteLine("어떤 고객에게 판매하시겠습니까? (번호 입력)");
                if (int.TryParse(Console.ReadLine(), out int customerIndex) && customerIndex > 0 && customerIndex <= currentTurnCustomers.Count)
                {
                    var customer = currentTurnCustomers[customerIndex - 1];
                    if (customer.PreferredPrices.ContainsKey(sellItem.Name))
                    {
                        int price = customer.PreferredPrices[sellItem.Name];
                        player.Money += price;
                        player.RemoveItem(sellItem.Name);
                        Console.WriteLine($"'{sellItem.Name}'을(를) {customer.Name}에게 {price}원에 판매했습니다.");
                    }
                    else
                    {
                        Console.WriteLine($"{customer.Name}은(는) '{sellItem.Name}'을(를) 원하지 않습니다.");
                    }
                }
            }
            else
            {
                Console.WriteLine("보유하고 있지 않은 아이템입니다.");
            }
        }

        private static void PayDebt()
        {
            Console.Clear();
            Console.WriteLine("--- 빚 상환 ---");
            Console.WriteLine($"현재 남은 빚: {playerDebt.Amount}원");
            Console.WriteLine("얼마를 갚으시겠습니까?");

            if (int.TryParse(Console.ReadLine(), out int amountToPay) && amountToPay > 0)
            {
                if (player.Money >= amountToPay)
                {
                    player.Money -= amountToPay;
                    playerDebt.Amount -= amountToPay;
                    Console.WriteLine($"{amountToPay}원을 갚았습니다. 남은 빚: {playerDebt.Amount}원");
                }
                else
                {
                    Console.WriteLine("소지금이 부족합니다.");
                }
            }
        }

        private static Dictionary<string, int> GetRandomPreferredPrices()
        {
            var preferences = new Dictionary<string, int>();
            int numItems = random.Next(1, availableItems.Count);
            var shuffledItems = availableItems.OrderBy(x => random.Next()).Take(numItems).ToList();
            foreach (var item in shuffledItems)
            {
                int price = item.BuyPrice + random.Next(100, 1000);
                preferences.Add(item.Name, price);
            }
            return preferences;
        }

        private static void EndGame()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("🎉 게임 종료! 🎉");
            if (playerDebt.Amount <= 0)
            {
                Console.WriteLine("👏 모든 빚을 청산하고 승리했습니다!");
            }
            else if (player.Money <= 0)
            {
                Console.WriteLine("💸 소지금 부족으로 더 이상 게임을 진행할 수 없습니다.");
            }
            else
            {
                Console.WriteLine("⏰ 시간 초과! 빚을 모두 갚지 못했습니다.");
            }
            Console.WriteLine($"최종 소지금: {player.Money}원 | 남은 빚: {playerDebt.Amount}원");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("아무 키나 눌러 종료하세요.");
            Console.ReadKey();
        }
    }
}
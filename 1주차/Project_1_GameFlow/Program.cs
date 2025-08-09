using System;
using System.Collections.Generic;
using System.Threading;

namespace Project_1_GameFlow
{
    // 캐릭터의 정보를 담을 클래스입니다.
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public List<string> Skills { get; set; } // 스킬 목록
        public Dictionary<string, int> Inventory { get; set; } // 아이템 목록

        public Player(string name, int health, int attack)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Skills = new List<string>();
            Inventory = new Dictionary<string, int>();
        }
    }

    // 몬스터의 정보를 담을 클래스입니다.
    public class Monster
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }

        public Monster(string name, int health, int attack)
        {
            Name = name;
            Health = health;
            Attack = attack;
        }
    }

    internal class Program
    {
        /// <summary>
        /// 특정 문자열의 색상을 변경하여 콘솔에 출력하는 함수입니다.
        /// </summary>
        /// <param name="message">색상을 변경할 문자열입니다.</param>
        /// <param name="color">문자열에 적용할 색상의 이름 (예: "Red", "Blue", "Green" 등)입니다.</param>
        public static void SetTextColor(string message, string color)
        {
            // 원래 콘솔의 색상을 저장합니다.
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                // string으로 받은 color 값을 ConsoleColor enum 타입으로 변환합니다.
                // 대소문자를 구분하지 않고 변환하기 위해 'true'를 사용합니다.
                if (Enum.TryParse(color, true, out ConsoleColor newColor))
                {
                    Console.ForegroundColor = newColor;
                }
                else
                {
                    // 유효하지 않은 색상 이름이 입력되면 오류 메시지를 출력합니다.
                    // 원래 색상으로 되돌리고 함수를 종료합니다.
                    Console.WriteLine($"오류: '{color}'은(는) 유효한 콘솔 색상이 아닙니다.");
                    return;
                }

                // 색상이 변경된 메시지를 출력합니다.
                Console.WriteLine(message);
            }
            finally
            {
                // 함수가 끝난 후에는 원래 색상으로 되돌립니다.
                // 이렇게 하면 이후에 출력되는 텍스트가 영향을 받지 않습니다.
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// 게임의 시작 제목을 콘솔에 출력하는 함수입니다.
        /// </summary>
        public static void ShowTitle()
        {
            // ASCII 아트 타이틀
            string titleArt = @"
  ___  ____  _____ ____  _    _    _  _ _  _
 / _ \|  _ \| ____|  _ \| |  | |  | \ | | \ | |
| | | | |_) |  _| | |_) | |  | |  |  \| |  \| |
| |_| |  _ <| |___|  _ <| |__| |__| |\  | |\  |
 \___/|_| \_|_____|_| \_\_____|_____|_| \_|_| \_|
";
            SetTextColor(titleArt, "Yellow"); // 노란색으로 제목 출력
            SetTextColor("          -- THE ADVENTURES OF HERO --", "White"); // 흰색으로 서브 타이틀 출력
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // 2. 캐릭터 & 몬스터
            Player player = new Player("용사", 100, 15);
            player.Skills.Add("파이어볼");
            player.Skills.Add("힐링");
            player.Inventory.Add("포션", 3);

            Monster monster = new Monster("오크", 80, 10);

            // 콘솔 창 설정
            Console.Title = "게임 정보 화면";
            Console.CursorVisible = false; // 콘솔 커서 숨기기

            // 게임 시작 시 제목을 한 번만 표시합니다.
            ShowTitle();

            while (player.Health > 0 && monster.Health > 0)
            {
                // UI 화면 그리기 (매 턴마다 화면을 지우고 새로 그립니다)
                DrawUI(player, monster);

                // 플레이어의 턴
                SetTextColor("\n[당신의 턴입니다] 무엇을 하시겠습니까?", "White");
                SetTextColor("1. 공격  2. 스킬 사용  3. 아이템 사용", "White");

                ConsoleKey key = Console.ReadKey(true).Key; // 키 입력 대기 (입력된 키는 콘솔에 표시되지 않음)

                switch (key)
                {
                    case ConsoleKey.D1: // 1. 공격
                        monster.Health -= player.Attack;
                        SetTextColor($"\n> {player.Name}이(가) {monster.Name}에게 {player.Attack}의 피해를 입혔습니다!", "Green");
                        break;
                    case ConsoleKey.D2: // 2. 스킬 사용
                        if (player.Skills.Count > 0)
                        {
                            string skillName = player.Skills[0]; // 현재는 첫 번째 스킬만 사용하도록 가정
                            if (skillName == "파이어볼")
                            {
                                int skillDamage = player.Attack * 2;
                                monster.Health -= skillDamage;
                                SetTextColor($"\n> {player.Name}이(가) 스킬 '{skillName}'을 사용해 {monster.Name}에게 {skillDamage}의 피해를 입혔습니다!", "Cyan");
                            }
                            else if (skillName == "힐링")
                            {
                                int healAmount = 20;
                                player.Health += healAmount;
                                SetTextColor($"\n> {player.Name}이(가) 스킬 '{skillName}'을 사용해 체력 {healAmount}를 회복했습니다!", "Cyan");
                            }
                            else
                            {
                                SetTextColor("\n> 사용할 수 없는 스킬입니다.", "Red");
                            }
                        }
                        else
                        {
                            SetTextColor("\n> 사용할 수 있는 스킬이 없습니다.", "Red");
                        }
                        break;
                    case ConsoleKey.D3: // 3. 아이템 사용
                        if (player.Inventory.ContainsKey("포션") && player.Inventory["포션"] > 0)
                        {
                            int healAmount = 30;
                            player.Health += healAmount;
                            player.Inventory["포션"]--;
                            SetTextColor($"\n> {player.Name}이(가) 아이템 '포션'을 사용해 체력 {healAmount}를 회복했습니다!", "Magenta");
                            SetTextColor($"(남은 포션: {player.Inventory["포션"]}개)", "DarkMagenta");
                        }
                        else
                        {
                            SetTextColor("\n> 포션이 없습니다.", "Red");
                        }
                        break;
                    default:
                        SetTextColor("\n> 잘못된 입력입니다. 다시 선택해주세요.", "Red");
                        Thread.Sleep(1000); // 사용자에게 메시지를 보여줄 시간
                        continue; // 다시 선택할 수 있도록 현재 턴을 건너뜁니다.
                }

                Console.ReadKey(); // 다음 턴으로 넘어가기 위해 키 입력 대기

                // 몬스터의 턴
                if (monster.Health > 0)
                {
                    player.Health -= monster.Attack;
                    SetTextColor($"\n> {monster.Name}이(가) {player.Name}에게 {monster.Attack}의 피해를 입혔습니다!", "Red");
                }

                Thread.Sleep(1000); // 턴과 턴 사이의 간격
            }

            // 게임 종료 화면
            Console.Clear();
            Console.SetCursorPosition(10, 10); // 커서 위치 설정

            if (player.Health <= 0)
            {
                SetTextColor("GAME OVER", "Red");
                SetTextColor($"\n> {player.Name}이(가) 사망했습니다.", "Red"); // 플레이어 사망 메시지
            }
            else if (monster.Health <= 0) // 몬스터 사망 시
            {
                SetTextColor("VICTORY!", "Yellow");
                SetTextColor($"\n> {monster.Name}님이 사망하셨습니다.", "Yellow"); // 몬스터 사망 메시지
            }
            Console.ReadKey(); // 게임 종료 후 키 입력 대기
        }

        /// <summary>
        /// 게임 UI를 화면에 그리는 보조 함수입니다.
        /// 플레이어와 몬스터의 현재 상태를 표시합니다.
        /// </summary>
        /// <param name="player">플레이어 객체입니다.</param>
        /// <param name="monster">몬스터 객체입니다.</param>
        static void DrawUI(Player player, Monster monster)
        {
            Console.Clear(); // 화면 초기화

            // 플레이어 정보, 스킬 및 아이템 정보를 문자열로 구성
            string playerInfo = $"이름: {player.Name}\n체력: {player.Health}\n공격력: {player.Attack}\n\n[체력]";
            string skillsInfo = "[ 스킬 ]\n- " + string.Join("\n- ", player.Skills);

            string inventoryInfo = "[ 아이템 ]\n";
            foreach (var item in player.Inventory)
            {
                inventoryInfo += $"- {item.Key}: {item.Value}개\n";
            }

            // 플레이어 정보 섹션 헤더
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"|           < 플레이어 >            |           < 스킬 & 아이템 >           |");
            Console.WriteLine("-------------------------------------------------------");
            Console.ResetColor();

            // 플레이어 정보와 스킬/아이템 정보를 나란히 출력
            DrawSideBySide(playerInfo, skillsInfo + "\n" + inventoryInfo, player.Health, 100);

            // 몬스터 정보 섹션 헤더
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n-------------------------------------------------------");
            Console.WriteLine($"|               < 몬스터 >                    |");
            Console.WriteLine("---------------------------------------------");
            Console.ResetColor();

            // 몬스터 정보 출력
            Console.WriteLine($"  이름: {monster.Name}");
            Console.WriteLine($"  체력: {monster.Health}");
            Console.WriteLine($"  공격력: {monster.Attack}");
            Console.WriteLine();

            // 몬스터 체력 바 출력
            Console.Write("  [체력]");
            DrawHealthBar(monster.Health, 80, ConsoleColor.Red);
            Console.WriteLine();

            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// 두 개의 텍스트 블록을 나란히 콘솔에 출력하는 보조 함수입니다.
        /// </summary>
        /// <param name="leftText">왼쪽에 출력할 텍스트입니다.</param>
        /// <param name="rightText">오른쪽에 출력할 텍스트입니다.</param>
        /// <param name="health">플레이어의 현재 체력입니다.</param>
        /// <param name="maxHealth">플레이어의 최대 체력입니다.</param>
        static void DrawSideBySide(string leftText, string rightText, int health, int maxHealth)
        {
            string[] leftLines = leftText.Split('\n');
            string[] rightLines = rightText.Split('\n');

            int maxLines = Math.Max(leftLines.Length, rightLines.Length);

            for (int i = 0; i < maxLines; i++)
            {
                string leftLine = i < leftLines.Length ? leftLines[i] : "";
                string rightLine = i < rightLines.Length ? rightLines[i] : "";

                // 왼쪽 텍스트는 30칸 폭으로 왼쪽 정렬, 오른쪽 텍스트는 '|' 뒤에 출력
                Console.Write($"  {leftLine,-30}");
                Console.Write($"| {rightLine}");
                Console.WriteLine();
            }

            // 플레이어의 체력 바를 왼쪽 섹션 아래에 출력
            Console.Write("  ");
            DrawHealthBar(health, maxHealth, ConsoleColor.Red);
            Console.WriteLine();
        }

        /// <summary>
        /// 체력 바를 콘솔에 그리는 함수입니다.
        /// </summary>
        /// <param name="current">현재 체력 값입니다.</param>
        /// <param name="max">최대 체력 값입니다.</param>
        /// <param name="color">체력 바의 채워진 부분에 사용할 색상입니다.</param>
        static void DrawHealthBar(int current, int max, ConsoleColor color)
        {
            Console.ForegroundColor = color; // 채워진 부분 색상 설정
            int barWidth = 15; // 체력 바의 전체 길이
            int filledBarCount = current * barWidth / max; // 채워질 부분의 개수 (비율에 따라 계산)

            // 채워진 부분 출력
            for (int i = 0; i < filledBarCount; i++)
            {
                Console.Write("■");
            }
            // 비어있는 부분 색상 설정 (회색)
            Console.ForegroundColor = ConsoleColor.Gray;
            // 비어있는 부분 출력
            for (int i = filledBarCount; i < barWidth; i++)
            {
                Console.Write("□");
            }
            Console.ResetColor(); // 콘솔 색상 초기화
            Console.Write($" {current}/{max}"); // 현재 체력 / 최대 체력 표시
        }
    }
}

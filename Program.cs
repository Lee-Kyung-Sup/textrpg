using System.ComponentModel.Design;

namespace chp2_hw
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Clear();
            StartMenu();
        }

        //시작메뉴
        static void StartMenu()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine(" 1.상태보기\n 2.인벤토리\n 3.상점\n");
                Console.WriteLine("원하시는 행동을 입력해주세요\n>>");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (number == 0)
                    {
                        break;
                    }
                    UserInput(number);
                }
            }
        }
        //각입력에 따른 출력
        static void UserInput(int number)
        {
            switch (number)
            {
                case 1:
                    status();
                    break;
                case 2:
                    inven();
                    break;
                case 3:
                    store();
                    break;

            }
        }
        //캐릭 상태
        static job a = new job(01, "Lee", 10, 5, 100, 3500);
        static void status()
        {
            Console.Clear();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            //job a = new job(01, "Lee", 10, 5, 100, 1500);



            a.Printplayer();

            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");
            if (int.TryParse(Console.ReadLine(), out int exit) && exit == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 선택입니다.");
            }
        }
        //인벤토리
        static void inven()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\n인벤토리\n 보유중인 아이템을 관리 할 수 있습니다.");
                Console.WriteLine("[아이템 목록]");

                a.displayinven();

                Console.WriteLine("\n1.장착관리");
                Console.WriteLine("\n2.나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");
                if (int.TryParse(Console.ReadLine(), out int r))
                {
                    if (r == 1)
                    {
                        equipinven();
                        break;
                    }
                    else if (r == 2)
                    {
                        return; // 인벤토리 관리 메뉴 종료
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택입니다.");
                    }

                }
                else
                {
                    Console.WriteLine("숫자를 입력.");
                    return; // 인벤토리 관리 메뉴 종료
                }
                Console.ReadLine();
            }
        }

        //직업 클래스
        public class job
        {
            public List<equipment> Inventory { get; } = new List<equipment>();
            public int Level;
            public string Name;
            public int Attackd;
            public int Defense;
            public int Hp;
            public int Gold { get; set; }
            public job(int level, string name, int sttackd, int defense, int hp, int gold)
            {
                Level = level;
                Name = name;
                Attackd = sttackd;
                Defense = defense;
                Hp = hp;
                Gold = gold;
            }
            public void PrintGold()
            {
                Console.WriteLine($"보유 골드: {Gold} G");
            }

            public void Printplayer()
            {
                Console.WriteLine($"Lv. {Level}\n {Name} (전사)\n 공격력: {Attackd + getequippedstat("공격력")}\n " +
            $"방어력: {Defense + getequippedstat("방어력")}\n 체력: {Hp}\n Gold: {Gold}");
            }

            public void displayinven()
            {
                Console.WriteLine("\n");
                if (Inventory.Count == 0)
                {
                    Console.WriteLine(" ");
                }
                else
                {
                    int count = 1;
                    foreach (equipment item in a.Inventory)
                    {
                        Console.Write($"[{count++}] ");
                        item.PrintInfo();
                        Console.Write(item.IsEquipped ? "[E]" : "");
                        Console.WriteLine();
                    }
                }
            }
        }

        //장비
        public class equipment
        {
            public int Num { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public int Stat { get; set; }
            public string Explain { get; set; }
            public int Price { get; set; }
            public bool IsEquipped { get; set; }
            public equipment(int num, string name, string type, int stat, string explain, int price)
            {
                Num = num;
                Name = name;
                Type = type;
                Stat = stat;
                Explain = explain;
                Price = price;
            }


            public void PrintInfo()
            {
                Console.WriteLine($"{Num}. {Name} | {Type}{Stat} | {Explain} | {Price}");
            }
        }

        //장착 관리
        static void equipinven()
        {
            Console.Clear();
            Console.WriteLine("[장착 관리]\n");
            Console.WriteLine("[아이템 목록]");

            int count = 1;
            foreach (equipment item in a.Inventory)
            {
                Console.Write($"[{count++}] ");
                item.PrintInfo();
                Console.Write(item.IsEquipped ? "[E]" : " ");
                Console.WriteLine(" ");
            }

            Console.WriteLine("\n장착할 아이템을 선택해주세요. (0: 나가기)\n>>");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0)
                {
                    return; // 장착 관리 메뉴 종료
                }
                else if (choice > 0 && choice <= a.Inventory.Count)
                {
                    toggleequip(a.Inventory[choice - 1]);
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다.");
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력하세요.");
            }

            Console.ReadLine();
        }

        //장착 관리 1번 눌렀을 시
        static void toggleequip(equipment item)
        {
            Console.Clear();
            Console.WriteLine($"아이템 장착 토글: {item.Name}\n");
            item.PrintInfo();

            if (item.IsEquipped)
            {
                Console.WriteLine("장착을 해제합니다.");

            }
            else
            {
                Console.WriteLine("장착합니다.");

            }

            item.IsEquipped = !item.IsEquipped;
            Console.WriteLine("아무 키나 누르세요.");
        }

        //상태보기에 스탯추가
        static int getequippedstat(string Type)
        {
            int totalStat = 0;
            foreach (equipment item in a.Inventory)
            {
                if (item.IsEquipped && item.Type == Type)
                {
                    totalStat += item.Stat;
                }
            }
            return totalStat;
        }

        //상점
        static void store()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("상점\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다\n");
                a.Printplayer();
                Console.WriteLine("\n[아이템 목록]");

                equipment e1 = new equipment(1, "수련자 갑옷", "방어력", 5, "수련에 도움을 주는 갑옷입니다.", 1000);
                equipment e2 = new equipment(2, "무쇠 갑옷", "방어력", 9, "무쇠로 만들어진 튼튼한 갑옷.", 2000);
                equipment e3 = new equipment(3, "스파르타의 갑옷", "방어력", 15, "스파르타 전사들이 사용했다는 전설의 갑옷.", 3500);
                equipment e4 = new equipment(4, "낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600);
                equipment e5 = new equipment(5, "청동 도끼", "공격력", 5, "어디선가 사용됐던거 같은 또기입니다.", 1500);
                equipment e6 = new equipment(6, "스파르타의 창", "공격력", 7, "스파르타 전사들이 사용했다는 전설의 창입니다.", 2000);


                e1.PrintInfo();
                e2.PrintInfo();
                e3.PrintInfo();
                e4.PrintInfo();
                e5.PrintInfo();
                e6.PrintInfo();

                Console.WriteLine("\n1.아이템 구매");
                Console.WriteLine("\n0.나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");
                if (int.TryParse(Console.ReadLine(), out int r))
                {
                    if (r == 1)
                    {
                        storebuy(a, e1, e2, e3, e4, e5, e6);
                        break;
                    }
                    else if (r == 0)
                    {
                        return; // 상점 종료
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택입니다.");
                    }

                    Console.ReadLine();

                }
            }

        }

        //상점-아이템구매
        static void storebuy(job a, params equipment[] items)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("상점-아이템 구매\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다\n");
                a.PrintGold();
                Console.WriteLine("\n[아이템 목록]");

                foreach (equipment item in items)
                {
                    item.PrintInfo();
                }

                Console.WriteLine("\n0.나가기");
                Console.WriteLine("\n 원하시는 행동을 입력해주세요.\n>>");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        break;
                    }
                    else if (choice > 0 && choice <= items.Length)
                    {
                        buyitem(a, items[choice - 1]);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택입니다. 다시 선택");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력");
                }
                Console.ReadLine();

            }
        }

        //상점 아이템 구매
        static void buyitem(job a, equipment item)
        {
            Console.Clear();
            Console.WriteLine($"아에템 구매: {item.Name}");
            item.PrintInfo();

            if (a.Gold >= item.Price)
            {
                if (a.Inventory.Contains(item))
                {
                    Console.WriteLine($"{item.Name}은 이미 보유하고 있습니다.");
                    Console.WriteLine("아무키나 누르세요.");
                }
                else
                {
                    Console.WriteLine("구매를 완료하였습니다");
                    a.Gold -= item.Price;
                    a.Inventory.Add(item);
                    Console.WriteLine("아무키나 누르세요.");
                }
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
                Console.WriteLine("아무키나 누르세요.");
            }
        }
    }
}
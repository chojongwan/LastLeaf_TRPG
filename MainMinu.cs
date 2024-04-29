namespace TRPGTest
{
    public enum ItemType                // 열거형 (아이템 종류, 종류 구분으로 공격력 방어력을 구분)
    {
        Weapon,
        Armor
    }
    public class Player
    {
        public int Gold { get; set; } = 800; // 초기 골드
        public int LV { get; set; } = 1;
        public int Attack { get; set; } = 5;
        public int Defense { get; set; } = 5;
        public int HP { get; set; } = 100;
        public int DungeonClearCount { get; set; } = 0;     //던전클리어 카운트에 따른 레벨 변화
        public List<Item> Inventory { get; } = new List<Item>(); // 인벤토리 리스트

    }
    public class Item          //프로퍼티
    {
        public string Name { get; set; } // 아이템 이름
        public string Effect { get; set; } // 아이템 효과
        public string Description { get; set; } // 아이템 설명
        public int Price { get; set; } // 아이템 가격
        public bool IsPurchased { get; set; } = false; // 아이템 구매 여부
        public ItemType Type { get; set; } // 아이템 종류
        public bool IsEquipped { get; set; } //아이템 장착 여부


        public Item(string name, string effect, string description, int price, ItemType type)
        {
            Name = name;
            Effect = effect;
            Description = description;
            Price = price;
            Type = type;
        }
    }

    public class MainMinu
    {
        Status showStatus= new Status();
        Inventory inventory =new Inventory();
        Shop shop =new Shop();
        Dungeon dungeon =new Dungeon();
        Rast rast =new Rast();
        
        public void ShowMainMenu()
        {
            string input = "";
            while (input != "0")
            {
                Console.Clear();
                Console.WriteLine(@" $$$$$$\                                           $$$$$$\    $$\                          $$\           $$\       $$\ 
$$  __$$\                                         $$  __$$\   $$ |                         $$ |          $$ |      $$ |
$$ /  \__| $$$$$$\  $$$$$$\$$$$\   $$$$$$\        $$ /  \__|$$$$$$\    $$$$$$\   $$$$$$\ $$$$$$\         $$ |      $$ |
$$ |$$$$\  \____$$\ $$  _$$  _$$\ $$  __$$\       \$$$$$$\  \_$$  _|   \____$$\ $$  __$$\\_$$  _|        $$ |      $$ |
$$ |\_$$ | $$$$$$$ |$$ / $$ / $$ |$$$$$$$$ |       \____$$\   $$ |     $$$$$$$ |$$ |  \__| $$ |          \__|      \__|
$$ |  $$ |$$  __$$ |$$ | $$ | $$ |$$   ____|      $$\   $$ |  $$ |$$\ $$  __$$ |$$ |       $$ |$$\                     
\$$$$$$  |\$$$$$$$ |$$ | $$ | $$ |\$$$$$$$\       \$$$$$$  |  \$$$$  |\$$$$$$$ |$$ |       \$$$$  |      $$\       $$\ 
 \______/  \_______|\__| \__| \__| \_______|       \______/    \____/  \_______|\__|        \____/       \__|      \__|
                                                                                                                       
                                                                                                                       
                                                                                                                       ");                                          //시작할때 로고
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("0. 게임종료");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전");
                Console.WriteLine("5. 휴식하기\n");
                Console.WriteLine("6. 저장하기");
                Console.WriteLine("7. 불러오기\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");

                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Environment.Exit(0); // 프로그램 종료
                        break;
                    case "1":
                        showStatus.ShowStatus(player);
                        break;
                    case "2":
                        inventory.ShowInventory(player);
                        break;
                    case "3":
                        shop.ShowShop(player);
                        break;
                    case "4":
                        dungeon.ShowDungeon(player);
                        break;
                    case "5":
                        rast.Rest( player);
                        break;
                    case "6":
                        Save Save = new Save();
                        Save.SaveGameData( player);
                        break;
                    case "7":
                        Save saveInstance = new Save();
                        player = saveInstance.LoadGameData();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        ShowMainMenu();
                        break;
                }
            }
        }
        Player player = new Player(); // 플레이어 객체 생성

    }
}


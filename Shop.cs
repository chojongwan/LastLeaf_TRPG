using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TRPGTest
{
    internal class Shop
    {
        // 상점의 아이템 목록
        static Item[] shopItems = new Item[]
        {
        new Item("가죽 갑옷", "방어력 +5", "기본적인 방어력을 가진 갑옷입니다.", 1000, ItemType.Armor),
        new Item("무쇠갑옷", "방어력 +17", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, ItemType.Armor),
        new Item("스파르타의 갑옷", "방어력 +30", "스파르타의 전사들이 사용했다는 전설등급 갑옷입니다.", 3500, ItemType.Armor),
        new Item("아테네의 갑옷", "방어력 +50", "올림푸스의 신 '아테네'가 사용했다는 신화등급 갑옷입니다.", 7000, ItemType.Armor),
        new Item("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검입니다.", 600, ItemType.Weapon),
        new Item("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Weapon),
        new Item("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설등급 창입니다.", 3500, ItemType.Weapon),
        new Item("헤라클래스의 몽둥이", "공격력 +15", "올림푸스의 신화적 전사 헤라클레스가 사용했다는 신화등급의 몽둥이입니다.", 7000, ItemType.Weapon),
        new Item("제우스의 아스트라페", "공격력 +50", "올림푸스 그 자체인 제우스가 사용했다는 고유무기 입니다.", 30000, ItemType.Weapon)
        };
        public void ShowShop(Player player)
        {
            string input = "";
            while (input != "0")
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine($"[보유 골드] {player.Gold} G\n");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Length; i++)
                {
                    string purchaseStatus = shopItems[i].IsPurchased ? "구매완료" : $"{shopItems[i].Price} G";
                    Console.WriteLine($"{i + 1}. {shopItems[i].Name} | {shopItems[i].Effect} | {shopItems[i].Description} | {purchaseStatus}");
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                input = Console.ReadLine();
                if (input == "1")
                    BuyItem(player);
                else if (input == "2")
                    SellItem(player);
                
                
            }
        }
        public void BuyItem(Player player)
        {
            string input = "";
            while (input != "0")
            {
                Console.WriteLine("구매할 아이템의 번호를 입력해주세요.");
               input = Console.ReadLine();
                // 입력이 유효한지 확인합니다.
                if (int.TryParse(input, out int itemIndex) && itemIndex >= 1 && itemIndex <= shopItems.Length)
                {
                    itemIndex--; // 배열 인덱스로 변환, 배열의 인덱스는 0부터 시작하기 때문에 번호를 1번부터 하기위해
                    if (shopItems[itemIndex].IsPurchased)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= shopItems[itemIndex].Price)
                    {
                        player.Gold -= shopItems[itemIndex].Price; // 골드 차감
                        player.Inventory.Add(shopItems[itemIndex]); // 인벤토리에 아이템 추가
                        shopItems[itemIndex].IsPurchased = true; // 구매 완료 표시
                        Console.WriteLine("구매를 완료했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                input = Console.ReadLine();
                 Console.WriteLine("잘못된 입력입니다.");
                
            }
        }
        public void SellItem(Player player)
        {
            Console.Clear();
            Console.WriteLine("판매할 아이템을 선택해주세요:\n");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Inventory[i].Name} - {player.Inventory[i].Price / 2} G");
            }
            Console.WriteLine("\n0. 나가기");
            string input = Console.ReadLine();
            if (input == "0")
            {
                ShowShop(player);
            }
            else if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= player.Inventory.Count)
            {
                selectedIndex--; // 배열 인덱스로 변환
                Item selectedItem = player.Inventory[selectedIndex];

                int sellingPrice = selectedItem.Price / 2; // 판매 가격은 원가의 절반입니다.
                player.Gold += sellingPrice; // 골드 증가
                player.Inventory.RemoveAt(selectedIndex); // 인벤토리에서 아이템 제거
                for (int i = 0; i < shopItems.Length; i++)
                {
                    if (shopItems[i].Name == selectedItem.Name)
                    {
                        shopItems[i].IsPurchased = false; // 구매 상태 초기화
                        break;
                    }
                }
                Console.WriteLine($"\n{selectedItem.Name}을(를) 판매했습니다. (+{sellingPrice} G)\n");
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
            }
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }
}

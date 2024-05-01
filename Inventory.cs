using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TRPGTest
{
    internal class Inventory
    {
        public void ShowInventory(Player player)
        {
            string input = "";
            //input = Console.ReadLine();

            while (input != "0")
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                if (player.Inventory.Count == 0)                    //Inventory은 아이템 리스트
                {
                    Console.WriteLine("보유 중인 아이템이 없습니다.");
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    input = Console.ReadLine();
                    if (input != "0")
                    {

                        Thread.Sleep(1);
                    }
                }
                else
                {
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < player.Inventory.Count; i++)
                    {
                        string equipped = player.Inventory[i].IsEquipped ? "[E]" : "";                  // 아이템이 장착되었는지 확인하여 표시, 삼항 연산자-> equipped
                        Console.WriteLine($"{i + 1}. {equipped}{player.Inventory[i].Name}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("장착할 장비의 번호를 입력해주세요:");
                    Console.WriteLine("장착 해재 ~버튼.");
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    input = Console.ReadLine();
                    if (input=="~" || input == "`")
                    {
                        ShowUnequipMenu(player);
                    }
                    else if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= player.Inventory.Count)        //selectedIndex 인벤토리에서 해당 아이템을 찾을때 사용 (out 인자를 사용)
                    {
                        EquipItem(selectedIndex - 1, player); // 선택한 아이템을 장착합니다.    -1을 하는 이유 받은 값은 아이템 번호 1이다 1을 보내야하는데 받는곳은 배열을 사용하니 -1을해서 배열 정렬을 하는것
                    }
                }
            }
            
        }
        // 아이템 장착 기능 구현
        public void EquipItem(int selectedIndex, Player player)
        {
            string input = "";
            while (input != "0")
            {
                input = "";
                //현재 플레이거 가지고 있는 아이템을 selected로 가져오는 것
                Item selected = player.Inventory[selectedIndex];

                if (selected.IsEquipped)
                {
                    Console.WriteLine("이미 장착된 아이템입니다.");
                }
                else
                {
                    // 이미 장착된 아이템의 수를 세어봅니다.
                    int equippedItemCount = 0;
                    foreach (var item in player.Inventory)
                    {
                        if (item.IsEquipped)
                            equippedItemCount++;
                    }
                    // 이미 2개의 아이템을 장착했는지 확인합니다.
                    if (equippedItemCount >= 2)
                    {
                        Console.WriteLine("더 이상 아이템을 장착할 수 없습니다.");
                    }
                    else
                    {
                        // 선택한 아이템을 장착합니다.
                        selected.IsEquipped = true;

                        // 선택한 아이템의 효과를 능력치에 반영합니다.
                        switch (selected.Type)
                        {
                            case ItemType.Weapon:
                                player.Attack += GetEffectValue(selected.Effect);
                                break;
                            case ItemType.Armor:
                                player.Defense += GetEffectValue(selected.Effect);
                                break;
                        }
                        Console.WriteLine($"{selected.Name}을(를) 장착했습니다.");
                    }
                }
                // 다시 원하는 행동을 입력받습니다.
                Console.WriteLine("새로고침 입력 0.");
                input = Console.ReadLine();
                
                
            }
        }
        // 아이템 해제 기능 구현
        public void UnequipItem(int selectedIndex, Player player)
        {

            // 선택한 아이템을 가져옵니다.
            Item selected = player.Inventory[selectedIndex];

            // 선택한 아이템이 이미 해제된 상태인지 확인합니다.
            if (!selected.IsEquipped)
            {
                Console.WriteLine("선택한 아이템은 이미 해제되었습니다.");
                return;
            }
            // 선택한 아이템을 해제합니다.
            selected.IsEquipped = false;
            // 선택한 아이템의 효과를 능력치에서 제거합니다.
            switch (selected.Type)
            {
                case ItemType.Weapon:
                    player.Attack -= GetEffectValue(selected.Effect);
                    break;
                case ItemType.Armor:
                    player.Defense -= GetEffectValue(selected.Effect);
                    break;
            }
            Console.WriteLine($"{selected.Name}을(를) 해제했습니다.");

        }
        // 아이템 해제를 위한 인벤토리 표시 기능 구현
        public void ShowUnequipMenu(Player player)
        {
            string input = "";
            while (input != "0")
            {
                Console.Clear();
                Console.WriteLine("장착 중인 아이템 목록");
                Console.WriteLine("장착 중인 아이템을 해제할 수 있습니다.\n");

                // 장착 중인 아이템 목록을 표시합니다.
                bool hasEquippedItems = false;
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    if (player.Inventory[i].IsEquipped)
                    {
                        Console.WriteLine($"{i + 1}. {player.Inventory[i].Name}");
                        hasEquippedItems = true;
                    }
                }
                if (!hasEquippedItems)
                {
                    Console.WriteLine("장착 중인 아이템이 없습니다.");
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                    input = Console.ReadLine();
                    Console.ReadKey();


                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("해제할 아이템의 번호를 입력해주세요:");
                    Console.WriteLine("0. 나가기\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                    input = Console.ReadLine();
                    if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= player.Inventory.Count)
                    {
                        UnequipItem(selectedIndex - 1,player); // 선택한 아이템을 해제합니다.
                        //ShowInventory(player);
                    }
                }
            }
        }
        // 아이템 효과 값을 가져오는 메서드
        public int GetEffectValue(string effect)
        {
            int value = 0;
            foreach (char c in effect)
            {
                if (char.IsDigit(c))
                {
                    value = value * 10 + (c - '0');
                }
            }
            return value;
        }
    }
}

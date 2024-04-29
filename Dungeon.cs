using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TRPGTest
{
    internal class Dungeon
    {
        public void ShowDungeon(Player player)
        {
            string input = "";
            while (input != "0")
            {
               input = "";

                Console.Clear();
                if (player.HP < 0)      //HP가 0이될때 게임 오버 구문
                {
                    GameOver(player);
                }
                Console.WriteLine("던전");
                Console.WriteLine("전투로 골드를 얻을 수 있는 던전입니다.\n");
                Console.WriteLine("요구능력치 : 방어력, 공격력 : 전리품 증가");
                Console.WriteLine($"[현재 HP] {player.HP} \n");
                Console.WriteLine($"[현재 Gold] {player.Gold}G \n");
                Console.WriteLine("[던전 목록]");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("1. 쉬움 [요구 능력치 15]");
                Console.WriteLine("2. 보통 [요구 능력치 30]");
                Console.WriteLine("3. 어려움 [요구 능력치 60]");
                input = Console.ReadLine();
                if (input == "1")
                    Dungeon1(player);
                else if (input == "2")
                    Dungeon2(player);
                else if (input == "3")
                    Dungeon3(player);
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    //ShowDungeon(player);
                }
            }
        }
        //쉬움
        public void Dungeon1(Player player)
        {
            
            bool clear = ClearDungeon(15, player.Defense); // 쉬운 던전의 권장 방어력은 15입니다.
            if (clear)
            {
                int baseReward = 1000; // 쉬운 던전의 기본 클리어 보상
                int reward = Compensation(baseReward, player.Attack);
                player.Gold += reward;
                Console.WriteLine($"던전을 클리어했습니다! 보상으로 {reward} G를 획득하였습니다.");
                
                player.DungeonClearCount++;
                if ((player.DungeonClearCount/2) >player.LV)
                {
                    player.LV++;
                    player.Attack += 1; // 공격력 증가
                    player.Defense += 2; // 방어력 증가
                    Console.WriteLine($"레벨이 올랐습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
                else
                {
                    Console.WriteLine($"던전을 클리어하였습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
            }
            else
            {
                Random rand = new Random();
                int hpLoss = rand.Next(20, 36) / 2; // 기본 체력 감소량은 20 ~ 35입니다.
                player.HP -= hpLoss;
                Console.WriteLine($"던전 클리어에 실패했습니다. 체력이 {hpLoss}만큼 감소하였습니다.");
            }
            Console.WriteLine("아무키나 눌러 던전으로 돌아가기");
            string input = Console.ReadLine();
            Console.ReadKey();


        }
        //보통
        public void Dungeon2(Player player)
        {
            bool clear = ClearDungeon(30, player.Defense); // 보통 던전의 권장 방어력은 30입니다.
            if (clear)
            {
                int baseReward = 2000; // 보통 던전의 기본 클리어 보상
                int reward = Compensation(baseReward, player.Attack);
                player.Gold += reward;
                Console.WriteLine($"던전을 클리어했습니다! 보상으로 {reward} G를 획득하였습니다.");
                player.DungeonClearCount++;
                if ((player.DungeonClearCount / 3) > player.LV)
                {
                    player.LV++;
                    player.Attack += 1; // 공격력 증가
                    player.Defense += 2; // 방어력 증가
                    Console.WriteLine($"레벨이 올랐습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
                else
                {
                    Console.WriteLine($"던전을 클리어하였습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
            }
            else
            {
                Random rand = new Random();
                int hpLoss = rand.Next(20, 36) / 2; // 기본 체력 감소량은 20 ~ 35입니다.
                player.HP -= hpLoss;
                Console.WriteLine($"던전 클리어에 실패했습니다. 체력이 {hpLoss}만큼 감소하였습니다.");
            }
            Console.WriteLine("아무키나 눌러 던전으로 돌아가기");
            string input = Console.ReadLine();
            Console.ReadKey();

        }
        //하드
        public void Dungeon3(Player player)
        {
            bool clear = ClearDungeon(60, player.Defense); // 어려움 던전의 권장 방어력은 60.
            if (clear)
            {
                int baseReward = 5000; // 어려움 던전의 기본 클리어 보상
                int reward = Compensation(baseReward, player.Attack);
                player.Gold += reward;
                Console.WriteLine($"던전을 클리어했습니다! 보상으로 {reward} G를 획득하였습니다.");
                player.DungeonClearCount++;
                if ((player.DungeonClearCount / 6) > player.LV)
                {
                    player.LV++;
                    player.Attack += 1; // 공격력 증가
                    player.Defense += 2; // 방어력 증가
                    Console.WriteLine($"레벨이 올랐습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
                else
                {
                    Console.WriteLine($"던전을 클리어하였습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
            }
            else
            {
                Random rand = new Random();
                int hpLoss = rand.Next(40, 56) / 2; // 기본 체력 감소량은 40 ~ 55입니다.
                player.HP -= hpLoss;
                Console.WriteLine($"던전 클리어에 실패했습니다. 체력이 {hpLoss}만큼 감소하였습니다.");
            }
            Console.WriteLine("아무키나 눌러 던전으로 돌아가기");
            string input = Console.ReadLine();
            Console.ReadKey();
        }
        public int Compensation(int baseReward, int attack)
        {
            Random rand = new Random();
            int attackreward = rand.Next(attack, attack * 2 + 1); // 공격력 * 1 ~ 2의 범위에서 %
            return baseReward + attackreward;
        }
        //클리어, 실패 판별
        public bool ClearDungeon(int capability, int defense)
        {
            if (defense < capability)
            {
                Random rand = new Random();
                return rand.Next(1, 101) > 40; // 40%의 확률로 실패
            }
            return true; // 클리어 가능
        }
        //게임 오버기능
        public void GameOver(Player player)
        {
            player.HP = 0;
            Console.WriteLine(@"  /$$$$$$                                           /$$$$$$                                      /$$       /$$
 /$$__  $$                                         /$$__  $$                                    | $$      | $$
| $$  \__/  /$$$$$$  /$$$$$$/$$$$   /$$$$$$       | $$  \ $$ /$$    /$$ /$$$$$$   /$$$$$$       | $$      | $$
| $$ /$$$$ |____  $$| $$_  $$_  $$ /$$__  $$      | $$  | $$|  $$  /$$//$$__  $$ /$$__  $$      | $$      | $$
| $$|_  $$  /$$$$$$$| $$ \ $$ \ $$| $$$$$$$$      | $$  | $$ \  $$/$$/| $$$$$$$$| $$  \__/      |__/      |__/
| $$  \ $$ /$$__  $$| $$ | $$ | $$| $$_____/      | $$  | $$  \  $$$/ | $$_____/| $$                          
|  $$$$$$/|  $$$$$$$| $$ | $$ | $$|  $$$$$$$      |  $$$$$$/   \  $/  |  $$$$$$$| $$             /$$       /$$
 \______/  \_______/|__/ |__/ |__/ \_______/       \______/     \_/    \_______/|__/            |__/      |__/
                                                                                                              
                                                                                                              
                                                                                                              ");

            Console.WriteLine("게임 오버!!! 체력이 0이 되었습니다");
            Console.WriteLine("죽어서 모든 돈을 잃어버렸습니다....");
            Console.WriteLine();
            player.HP = 100;
            player.Gold = 0;
            string re = Console.ReadLine();
            Console.WriteLine("아무키나 눌러 던전으로 돌아가기");
            string input = Console.ReadLine();
            Console.Clear();
        }
    }
}

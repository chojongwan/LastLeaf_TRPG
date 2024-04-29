using System;
using System.Collections.Generic;
using System.Linq;
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
                if (player.HP <= 0) // HP가 0이하인 경우 게임 오버
                {
                    GameOver(player);
                    break;
                }
                Console.WriteLine("던전");
                Console.WriteLine("전투로 골드를 얻을 수 있는 던전입니다.\n");
                Console.WriteLine("요구능력치 : 방어력, 공격력 : 전리품 증가");
                Console.WriteLine($"[현재 HP] {player.HP} \n");
                Console.WriteLine($"[현재 Gold] {player.Gold}G \n");
                Console.WriteLine("1. 전투 시작");
                Console.WriteLine("0. 나가기");
                input = Console.ReadLine();
                if (input == "1")
                    StartBattle(player); // 전투 시작
                else
                    Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 전투 시작
        public void StartBattle(Player player)
        {
            Console.Clear();
            Console.WriteLine("**Battle!!**");

            // 몬스터 랜덤 등장 (1~4마리)
            Random rand = new Random();
            int monsterCount = rand.Next(1, 5); // 1~4 사이의 랜덤한 몬스터 수
            List<Monster> monsters = new List<Monster>();

            Console.WriteLine("총 {0}마리의 몬스터가 등장했습니다.\n", monsterCount);

            // 랜덤 몬스터 생성
            for (int i = 0; i < monsterCount; i++)
            {
                monsters.Add(CreateRandomMonster());
            }

            // 플레이어 정보 출력
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0} {1} ({2})", player.LV, player.Name, player.Job);
            Console.WriteLine("HP: {0}/{1}\n", player.HP, player.MaxHP);

            // 몬스터 정보 출력
            Console.WriteLine("[몬스터]");
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine("{0}. Lv.{1} {2}  HP {3}", i + 1, monsters[i].Level, monsters[i].Name, monsters[i].HP);
            }

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");

            // TODO: 전투 진행 로직 구현
        }

        // 랜덤 몬스터 생성 메서드
        public Monster CreateRandomMonster()
        {
            Random rand = new Random();
            int level = rand.Next(2, 6); // 랜덤한 레벨 (2~5)
            string name = "";
            int hp = 0;
            int atk = 0;

            switch (level)
            {
                case 2:
                    name = "미니언";
                    hp = rand.Next(10, 21); // 10~20 사이의 랜덤한 체력
                    atk = rand.Next(3, 7); // 3~6 사이의 랜덤한 공격력
                    break;
                case 3:
                    name = "공허충";
                    hp = rand.Next(8, 16); // 8~15 사이의 랜덤한 체력
                    atk = rand.Next(6, 10); // 6~9 사이의 랜덤한 공격력
                    break;
                case 4:
                    name = "대포미니언";
                    hp = rand.Next(20, 31); // 20~30 사이의 랜덤한 체력
                    atk = rand.Next(7, 12); // 7~11 사이의 랜덤한 공격력
                    break;
                case 5:
                    name = "킹크랩";
                    hp = rand.Next(25, 36); // 25~35 사이의 랜덤한 체력
                    atk = rand.Next(10, 16); // 10~15 사이의 랜덤한 공격력
                    break;
            }

            return new Monster(name, level, hp, atk);
        }

        // 게임 오버 메서드
        public void GameOver(Player player)
        {
            Console.WriteLine("게임 오버! 체력이 0이 되었습니다.");
            Console.WriteLine("모든 돈을 잃어버렸습니다...");
            player.Gold = 0;
            player.HP = player.MaxHP; // 체력 초기화
            Console.WriteLine("아무 키나 눌러 던전으로 돌아가기");
            Console.ReadKey();
        }
    }
}

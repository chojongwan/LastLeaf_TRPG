using System;
using System.Collections.Generic;
using System.Threading;

namespace TRPGTest
{
    internal class Dungeon
    {
        public class Monster
        {
            public string MonsterName { get; set; } // 몬스터 이름
            public int MonsterLV { get; set; } // 몬스터 레벨

            public int MonsterHP { get; set; } // 몬스터 체력
            public int AttackMonster { get; set; } // 몬스터 공격력
            public bool MonsterDie { get; set; } // 몬스터 생사구분
        }
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
            Console.WriteLine("HP: {0}/{1}\n", player.HP, 100);

            // 몬스터 정보 출력
            Console.WriteLine("[몬스터]");
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine("{0}. Lv.{1} {2}  HP {3}", i + 1, monsters[i].MonsterLV, monsters[i].MonsterName, monsters[i].MonsterHP);
            }

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 다음");
            string input = Console.ReadLine();

            // 공격 선택
            if (input == "1")
            {
                AttackMonster(player, monsters);
            }
            else if (input == "0")
            {
                // 다음 페이즈 진행
                EnemyPhase(player, monsters);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
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
                    name = "슈퍼미니언";
                    hp = rand.Next(25, 36); // 25~35 사이의 랜덤한 체력
                    atk = rand.Next(10, 16); // 10~15 사이의 랜덤한 공격력
                    break;
            }

            Monster newMonster = new Monster();
            newMonster.MonsterName = name;
            newMonster.MonsterLV = level;
            newMonster.MonsterHP = hp;
            newMonster.AttackMonster = atk;

            return newMonster;
        }


        // 몬스터 공격 메서드
        public void AttackMonster(Player player, List<Monster> monsters)
        {
            // 몬스터 선택
            Console.WriteLine("\n대상을 선택해주세요.");
            int targetIndex;
            if (!int.TryParse(Console.ReadLine(), out targetIndex) || targetIndex < 1 || targetIndex > monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            // 선택한 몬스터 인덱스 계산 (0부터 시작하는 인덱스로 변환)
            int monsterIndex = targetIndex - 1;

            // 선택한 몬스터가 이미 죽은 경우
            if (monsters[monsterIndex].MonsterDie)
            {
                Console.WriteLine("이미 죽은 몬스터를 공격할 수 없습니다.");
                return;
            }

            // 몬스터 공격
            int damage = CalculateDamage(player.Attack);
            monsters[monsterIndex].MonsterHP -= damage;

            // 공격 결과 출력
            Console.WriteLine("\nChad 의 공격!");
            Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]", monsters[monsterIndex].MonsterName, damage);

            // 몬스터가 죽은 경우
            if (monsters[monsterIndex].MonsterLV <= 0)
            {
                monsters[monsterIndex].MonsterDie = true;
                Console.WriteLine("{0}이(가) 사망했습니다.", monsters[monsterIndex].MonsterName);
            }

            // 전투 다음 페이즈 진행
            EnemyPhase(player, monsters);
        }
        // 공격력 계산 메서드 (10% 오차 적용)
        public int CalculateDamage(int attack)
        {
            Random rand = new Random();
            double error = rand.NextDouble() * 0.2 - 0.1; // -0.1부터 0.1까지의 오차
            int finalDamage = (int)Math.Ceiling(attack * (1 + error)); // 최종 공격력 계산 (오차 적용)
            return finalDamage;
        }
        // 적 몬스터 공격 페이즈
        public void EnemyPhase(Player player, List<Monster> monsters)
        {
            Console.WriteLine("\nEnemy Phase 시작\n");

            // 죽은 몬스터가 아닌 몬스터들에 대해 공격 수행
            foreach (var monster in monsters)
            {
                if (!monster.MonsterDie)
                {
                    // 몬스터 공격력 계산
                    int damage = CalculateDamage(monster.AttackMonster);

                    // 플레이어 체력 감소
                    player.HP -= damage;

                    // 공격 결과 출력
                    Console.WriteLine("{0}의 공격!", monster.MonsterName);
                    Console.WriteLine("Chad을(를) 공격하여 {0}만큼의 데미지를 입혔습니다.", damage);
                    Console.WriteLine("Chad의 체력: {0}/{1}\n", player.HP, 100);

                    // 플레이어가 죽은 경우
                    if (player.HP <= 0)
                    {
                        Console.WriteLine("플레이어가 사망했습니다. 게임 오버!");
                        GameOver(player);
                        return;
                    }
                }
            }

            // 다음 행동 선택
            Console.WriteLine("0. 다음");
            string input = Console.ReadLine();
            if (input == "0")
            {
                // 플레이어의 차례로 돌아가기
                StartBattle(player);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                // 다시 Enemy Phase 진행
                EnemyPhase(player, monsters);
            }
        }

        // 게임 오버 메서드
        public void GameOver(Player player)
        {
            Console.WriteLine("게임 오버! 체력이 0이 되었습니다.");
            Console.WriteLine("모든 돈을 잃어버렸습니다...");
            player.Gold = 0;
            player.HP = 100; // 체력 초기화
            Console.WriteLine("아무 키나 눌러 던전으로 돌아가기");
            Console.ReadKey();
        }
    }
}

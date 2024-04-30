﻿using System;
using System.Collections.Generic;

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
            public int MonsterCount { get; set; }
        }

        List<Monster> monsters; // 몬스터 리스트
        public Random rand = new Random();
        public int monsterCount = 0;
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
        public Dungeon()
        {
            monsters = new List<Monster>(); // 몬스터 리스트 초기화
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

            // 몬스터가 생성되지 않은 경우에만 몬스터 생성
            if (monsters.Count == 0)
            {
                // 몬스터 랜덤 등장 (1~4마리)
                //Random rand = new Random();
                monsterCount = rand.Next(1, 5); // 1~4 사이의 랜덤한 몬스터 수

                Console.WriteLine("총 {0}마리의 몬스터가 등장했습니다.\n", monsterCount);

                // 랜덤 몬스터 생성
                for (int i = 0; i < monsterCount; i++)
                {
                    monsters.Add(CreateRandomMonster());
                }
            }

            // 플레이어 정보 출력
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0} {1} ({2})", player.LV, player.Name, player.Job);
            Console.WriteLine("HP: {0}/{1}\n", player.HP, 100);

            // 몬스터 정보 출력
            Console.WriteLine("[몬스터]");
            for (int i = 0; i < monsterCount; i++)
            {
                Console.WriteLine("{0}. Lv.{1} {2}  HP {3}", i + 1, monsters[i].MonsterLV, monsters[i].MonsterName, monsters[i].MonsterHP);
            }

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 도망치기");
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
                StartBattle(player);
            }
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
                Console.ReadKey();
                return;
            }

            // 선택한 몬스터 인덱스 계산 (0부터 시작하는 인덱스로 변환)
            int monsterIndex = targetIndex - 1;

            // 선택한 몬스터가 이미 죽은 경우
            if (monsters[monsterIndex].MonsterDie)
            {
                Console.WriteLine("이미 죽은 몬스터를 공격할 수 없습니다.");
                Console.ReadKey();
                return;
            }

            // 몬스터 공격
            int damage = CalculateDamage(player.Attack);
            monsters[monsterIndex].MonsterHP -= damage;

            // 공격 결과 출력
            Console.WriteLine("\nChad 의 공격!");
            Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]", monsters[monsterIndex].MonsterName, damage);
            Console.ReadKey();
            // 몬스터가 죽은 경우
            
            if (monsters[monsterIndex].MonsterHP <= 0)
            {
                monsters[monsterIndex].MonsterHP = 0; // HP가 0 아래로 내려가지 않도록 보정
                monsters[monsterIndex].MonsterDie = true;

                Console.WriteLine("{0}이(가) 사망했습니다.", monsters[monsterIndex].MonsterName);
                player.DungeonClearCount++;
                if ((player.DungeonClearCount / 4) >= player.LV)
                {
                    player.LV++;
                    player.Attack += 1; // 공격력 증가
                    player.Defense += 2; // 방어력 증가
                    Console.WriteLine($"레벨이 올랐습니다. 현재 레벨: {player.LV}, 공격력: {player.Attack}, 방어력: {player.Defense}");
                }
                Console.ReadKey();
                StartBattle(player);
                // 모든 몬스터가 죽었는지 확인
                bool allMonstersDead = true;
                foreach (var monster in monsters)
                {
                    if (!monster.MonsterDie)
                    {
                        monster.MonsterCount = 0;
                        allMonstersDead = false;
                        break;
                    }
                }
                // 모든 몬스터가 죽은 경우 다음 랜덤 스폰
                if (allMonstersDead)
                {
                    Console.Clear();
                    Console.WriteLine("승리하였습니다! 모든 몬스터를 제거하였습니다.");
                    Console.WriteLine("1. 다음 던전으로 가시겠습니까?");
                    Console.WriteLine();
                    Console.WriteLine("0. 던전에서 나가시겠습니까?");
                    string input = Console.ReadLine();
                    monsterCount = 0;
                    monsters.Clear();
                    if (input == "1")
                    {
                        StartBattle(player);
                    }
                    else
                    {
                        ShowDungeon(player);
                    }

                }
            }
            else
            {
                // 전투 다음 페이즈 진행
                EnemyPhase(player, monsters);
            }
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
        // 플레이어의 방어력을 몬스터의 공격력에서 빼서 데미지를 계산하고, 음수가 되면 데미지가 없도록 설정
        public void EnemyPhase(Player player, List<Monster> monsters)
        {
            Console.Clear();
            Console.WriteLine("\nEnemy Phase 시작\n");

            // 모든 몬스터가 죽은지 확인
            bool allMonstersDead = true;
            foreach (var monster in monsters)
            {
                if (!monster.MonsterDie)
                {
                    allMonstersDead = false;
                    break;
                }
            }

            if (allMonstersDead)
            {
                // 몬스터 카운트 초기화
                monsterCount = 0;

                // 기존에 존재했던 몬스터들을 전부 제거
                monsters.Clear();

                // 다시 몬스터 스폰
                StartBattle(player);
            }
            else
            {
                // 살아있는 몬스터에 대해 공격 수행
                foreach (var monster in monsters)
                {
                    if (!monster.MonsterDie)
                    {
                        // 몬스터 공격력 계산
                        int damage = CalculateDamage(monster.AttackMonster);

                        // 플레이어 방어력을 고려하여 데미지 계산
                        damage -= player.Defense;
                        if (damage < 0)
                        {
                            damage = 0; // 음수인 경우 데미지가 없도록 설정
                        }

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
                Console.WriteLine("0. 도망치기");
                Console.WriteLine("1. 진행하기");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    // 플레이어의 차례로 돌아가기
                    //ShowDungeon(player);
                }
                else
                {
                    StartBattle(player);
                    Console.ReadKey();
                }
            }
        }

        // 게임 오버 메서드
        public void GameOver(Player player)
        {
            Console.Clear();
            Console.WriteLine(@"  /$$$$$$                                           /$$$$$$                                      /$$       /$$
 /$$__  $$                                         /$$__  $$                                    | $$      | $$
| $$  \__/  /$$$$$$  /$$$$$$/$$$$   /$$$$$$       | $$  \ $$ /$$    /$$ /$$$$$$   /$$$$$$       | $$      | $$
| $$ /$$$$ |____  $$| $$_  $$_  $$ /$$__  $$      | $$  | $$|  $$  /$$//$$__  $$ /$$__  $$      | $$      | $$
| $$|_  $$  /$$$$$$$| $$ \ $$ \ $$| $$$$$$$$      | $$  | $$ \  $$/$$/| $$$$$$$$| $$  \__/      |__/      |__/
| $$  \ $$ /$$__  $$| $$ | $$ | $$| $$_____/      | $$  | $$  \  $$$/ | $$_____/| $$                          
|  $$$$$$/|  $$$$$$$| $$ | $$ | $$|  $$$$$$$      |  $$$$$$/   \  $/  |  $$$$$$$| $$             /$$       /$$
 \______/  \_______/|__/ |__/ |__/ \_______/       \______/     \_/    \_______/|__/            |__/      |__/
                                                                                                              
                                                                                                              
                                                                                                              ");
            Console.WriteLine("모든 돈을 잃어버렸습니다...");
            player.Gold = 0;
            player.HP = 100; // 체력 초기화
            Console.WriteLine("아무 키나 눌러 던전으로 돌아가기");
            Console.ReadKey();
        }
    }
}

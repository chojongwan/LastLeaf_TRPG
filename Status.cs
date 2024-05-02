using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPGTest
{
    internal class Status
    {
        // 상태 보기 기능 구현
        public void ShowStatus(Player player)
        {
            string input = "";
            while (input != "0")
            {
                Console.Clear();
                Console.WriteLine($"LV: {player.LV}");
                Console.WriteLine($"이름: {player.Name}");
                Console.WriteLine($"직업 ({player.Job})");
                Console.WriteLine($"공격력: {player.Attack}");
                Console.WriteLine($"방여력: {player.Defense}");
                Console.WriteLine($"체력: {player.HP}");
                Console.WriteLine($"Gold: {player.Gold} G\n");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                input = Console.ReadLine();
                if (input != "0")
                {
                    Console.WriteLine("master code 입력.");
                    string master = Console.ReadLine();
                    if (master == "allmaster")
                    {
                        Console.WriteLine("비밀번호를 입력하세요.");
                        string input1 = Console.ReadLine();
                        if (input1 == "1004")
                        {
                            player.Attack = 1004;
                            player.Defense = 1004;
                        }
                    }
                    Console.ReadKey();
                }
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
            string input="";
            while (input != "0")
            {
                Console.Clear();
                Console.WriteLine($"LV: {player.LV}");
                Console.WriteLine("Chad (전사)");
                Console.WriteLine($"공격력: {player.Attack}");
                Console.WriteLine($"방여력: {player.Defense}");
                Console.WriteLine($"체력: {player.HP}");
                Console.WriteLine($"Gold: {player.Gold} G\n");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                input = Console.ReadLine();
                if (input != "0")
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

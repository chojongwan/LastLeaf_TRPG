using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TRPGTest
{
    internal class Rast
    {
        public void Rest(Player player)
        {
            Console.Clear();
            Console.WriteLine($"휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)\n");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (player.Gold >= 500 && player.HP < 100)
                    {
                        player.Gold -= 500; // 골드 차감
                        player.HP = Math.Min(player.HP + 100, 100); // 최대 체력 100으로 제한하여 회복
                        Console.WriteLine("휴식을 완료했습니다.");
                    }
                    else if (player.Gold < 500)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }
                    else if (player.HP >= 100)
                    {
                        Console.WriteLine("이미 체력이 최대입니다.");
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
        }
    }
}

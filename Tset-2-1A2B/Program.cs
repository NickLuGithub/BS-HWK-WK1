using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tset_2_1A2B
{
    internal class Program
    {
        private static int n = 4;
        static void Main(string[] args)
        {
            int[] number = new int[4];
            int a = 0, b = 0;
            string numberList;
            bool isPlay = true;
            bool isA = true;
            var rand = new Random();

            while (isPlay)
            {
                
                for (int i = 0; i < n; i++)
                {
                    number[i] = rand.Next(0, 9);
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if ((j != i) & (number[i] == number[j])) i--;
                        }
                    }
                }

                numberList = number[0].ToString() + number[1].ToString() + number[2].ToString() + number[3].ToString();
                isA = false;

                while (!isA)
                {
                    Console.WriteLine("請輸入四個數字:");

                    var inputList = Console.ReadLine();

                    foreach (var item in inputList)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in numberList)
                    {
                        Console.WriteLine(item);
                    }

                    var k = numberList.Intersect(inputList);
                    b = k.Count();
                    if (b > 0)
                    {
                        a += k.Where(x => numberList.IndexOf(x) == inputList.IndexOf(x)).Count();
                        b = b - a;
                    }

                    Console.Write("判定結果是");
                    Console.Write(Convert.ToString(a) + "A");
                    Console.Write(Convert.ToString(b) + "B");

                    if (a == 4)
                    {
                        Console.WriteLine("恭喜你！猜對了！！");

                        Console.WriteLine("你要繼續玩嗎？(y/n):");
                        string temp = Console.ReadLine();
                        isA = true;
                        if ((temp == "n") | (temp == "N"))
                        {
                            isPlay = true;
                            Console.WriteLine("遊戲結束，下次再來玩喔～");
                        }
                    }

                    a = 0;
                    b = 0;

                }
            }

            Console.ReadLine();
        }
    }
}

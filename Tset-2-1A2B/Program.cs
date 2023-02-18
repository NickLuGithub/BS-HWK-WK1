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
            List<char> numberList = new List<char>();
            int[] input = new int[4];
            int a = 0, b = 0;
            var rand = new Random();
            bool isPlay = true;
            bool isA = true;

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

                foreach(var item in number)
                {
                    numberList.Add((char)item);
                }
                
                number = new int[4] { 1, 2, 3, 4 };
                isA = false;

                while (!isA)
                {
                    Console.WriteLine("請輸入四個數字:");

                    var inputList = Console.ReadLine().ToList();

                    foreach (var item in inputList)
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in numberList)
                    {
                        Console.WriteLine(item);
                    }

                    var k = numberList.Union(inputList);
                    b = k.Count();
                    Console.WriteLine($"{a}A{b}B");
                    if (b > 0)
                    {
                        a += k.Where(x => numberList.IndexOf(x) == inputList.IndexOf(x)).Count();
                        b = b - a;
                    }

                    Console.WriteLine($"{a}A{b}B");
                }
            }

            Console.ReadLine();
        }
    }
}

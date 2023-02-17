using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tset_2_1A2B
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] number = new int[4];
            int[] input = new int[4];
            int a = 0, b = 0;
            var rand = new Random();
            bool isPlay = true;
            bool isA = true;

            while (isPlay)
            {
                /*
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
                */

                number = new int[4] { 1, 2, 3, 4 };
                isA = false;

                while (!isA)
                {
                    Console.WriteLine("請輸入四個數字:");

                    int temp;
                    int k = 0;
                    temp = Convert.ToInt32(Console.ReadLine());
                    int wqeqw;
                    if (temp < 1000)
                    {
                        input[3] = 0;
                        k = 1;
                    }
                    for (int i = n - k - 1; i >= 0; i--)
                    {
                        input[i] = temp % 10;
                        temp /= 10;
                    }

                    Console.WriteLine($"{a}A{b}B");
                    if (a == 4)
                    {
                        Console.WriteLine("恭喜破關，是否再玩一次(Y/N)");
                        isA = true;
                        string PNext = Console.ReadLine();
                        if (!((PNext == "y") | (PNext == "Y"))) isPlay = false;
                    }
                    else
                    {

                    }
                }
            }

            Console.ReadLine();
        }
    }
}

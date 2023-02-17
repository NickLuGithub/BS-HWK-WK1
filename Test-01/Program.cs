using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 新專案

            /*
                假設有一個 CSV 檔，裡面有商品編號、商品名稱、商品數量、價格及商品類別五個欄位
                請根據以下需求來完成:
                1. 計算所有商品的總價格
                2. 計算所有商品的平均價格
                3. 計算商品的總數量
                4. 計算商品的平均數量
                5. 找出哪一項商品最貴
                6. 找出哪一項商品最便宜
                7. 計算產品類別為 3C 的商品總價
                8. 計算產品類別為飲料及食品的商品價格
                9. 找出所有商品類別為食品，而且商品數量大於 100 的商品
                10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品
                11. 呈上題，請計算該類別底下所有商品的平均價格
                12. 依照商品價格由高到低排序
                13. 依照商品數量由低到高排序
                14. 找出各商品類別底下，最貴的商品
                15. 找出各商品類別底下，最貴的商品
                16. 找出價格小於等於 10000 的商品
                17. 製作一頁 4 筆總共 5 頁的分頁選擇器
            */

            Console.ForegroundColor = ConsoleColor.Yellow;

            List<Product> ProductList = new List<Product>();

            // 讀取資料
            string fileName = @"product.csv";
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            using (StreamReader sr = new StreamReader(fs))
            {
                string temp;
                while ((temp = sr.ReadLine()) != null)
                {
                    if (temp != "商品編號,商品名稱,商品數量,價格,商品類別")
                    {
                        string[] list;
                        list = temp.Split(',');
                        ProductList.Add(new Product()
                        {
                            ID = list[0],
                            Name = list[1],
                            Amount = Convert.ToInt32(list[2]),
                            Price = Convert.ToDecimal(list[3]),
                            Category = list[4],
                        });
                    }
                }
            }
            fs.Close();

            Console.WriteLine("商品總攬");
            Display(ProductList);
            DisplayClearNext();
            
            var categoryList = ProductList.GroupBy(x => x.Category);
            printAllCategoryList(categoryList);
            DisplayClearNext();

            // 1
            Console.WriteLine($"所有商品的總價格 : {ProductList.Sum(x => x.Price)}");

            // 2
            Console.WriteLine($"所有商品的平均價格 : {ProductList.Average(x => x.Price)}");

            // 3
            Console.WriteLine($"商品的總數量 : {ProductList.Sum(x => x.Amount)}");

            // 4
            Console.WriteLine($"商品的平均數量 : {ProductList.Average(x => x.Amount)}");

            // 5.找出哪一項商品最貴
            var maxProductName =
                from prod in ProductList
                where prod.Price == ProductList.Max(x => x.Price)
                select prod;

            foreach (var prod in maxProductName)
            {
                Console.WriteLine($"最貴商品 : {prod.Name}");
            }

            // 6.找出哪一項商品最便宜
            var minProductName =
                from prod in ProductList
                where prod.Price == ProductList.Min(x => x.Price)
                select prod;

            foreach (var prod in minProductName)
            {
                Console.WriteLine($"最便宜商品 : {prod.Name}");
            }

            // 7.計算產品類別為 3C 的商品總價
            foreach (var prod in categoryList)
            {
                if(prod.Key == "3C")
                {
                    Console.WriteLine($"3C 商品總價為 {(prod.Sum(x => x.Price))}");
                }
            }
            // 8.計算產品類別為飲料及食品的商品價格

            decimal Total = 0;
            foreach (var prod in categoryList)
            {                
                if (prod.Key == "飲料" | prod.Key == "食品")
                {
                    Total += prod.Sum(x => x.Price);                 
                }
            }
            Console.WriteLine($"飲料及食品商品總價為 {Total}");


            Console.ReadLine();
        }

        private static void printAllCategoryList(IEnumerable<IGrouping<string, Product>> categoryList)
        {
            foreach (var category in categoryList)
            {
                Console.WriteLine($"{category.Key}類商品\n");
                foreach (var item in category)
                {
                    Console.WriteLine($"\t{item.Name}");
                }
                Console.WriteLine();
            }
        }

        private static void DisplayClearNext()
        {
            Console.WriteLine("按下任一鍵下一頁");
            Console.ReadLine();
            Console.Clear();
        }

        static void Display(List<Product> p)
        {
            foreach(Product product in p)
            {
                Console.WriteLine($"{product.ID, 4}\t{product.Price,10}元\t剩餘數量{product.Amount,10} 個\t{product.Category}\t{product.Name}\n");
            }
        }
    }
}

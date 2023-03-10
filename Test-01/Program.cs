using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
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
            string fileName = @"..\..\product.csv";
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

            // 1.計算所有商品的總價格
            Console.WriteLine($"1. 所有商品的總價格 : {ProductList.Sum(x => x.Price)}\n");

            // 2.計算所有商品的平均價格
            Console.WriteLine($"2. 所有商品的平均價格 : {ProductList.Average(x => x.Price)}\n");

            // 3.計算商品的總數量
            Console.WriteLine($"3. 商品的總數量 : {ProductList.Sum(x => x.Amount)}\n");

            // 4.計算商品的平均數量
            Console.WriteLine($"4. 商品的平均數量 : {ProductList.Average(x => x.Amount)} \n");

            // 5.找出哪一項商品最貴
            var maxProductName =
                from prod in ProductList
                where prod.Price == ProductList.Max(x => x.Price)
                select prod;

            foreach (var prod in maxProductName)
            {
                Console.WriteLine($"5. 最貴商品 : {prod.Name}\n");
            }

            // 6.找出哪一項商品最便宜
            var minProductName =
                from prod in ProductList
                where prod.Price == ProductList.Min(x => x.Price)
                select prod;

            foreach (var prod in minProductName)
            {
                Console.WriteLine($"6. 最便宜商品 : {prod.Name} \n");
            }

            // 7.計算產品類別為 3C 的商品總價
            foreach (var prod in categoryList)
            {
                if(prod.Key == "3C")
                {
                    Console.WriteLine($"7. 3C 商品總價為 {prod.Sum(x => x.Price)} \n");
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
            Console.WriteLine($"8. 飲料及食品商品總價為 {Total} \n");

            // 9.找出所有商品類別為食品，而且商品數量大於 100 的商品
            Console.WriteLine("9. 找出所有商品類別為食品，而且商品數量大於 100 的商品");
            foreach (var prod in categoryList)
            {
                if (prod.Key == "食品")
                {
                    var item = prod.Where(x => x.Amount > 100);

                    ByItem(item);
                }
            }
            Console.WriteLine();

            // 10.找出各個商品類別底下有哪些商品的價格是大於 1000 的商品
            // 11.呈上題，請計算該類別底下所有商品的平均價格
            Console.WriteLine("10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品\n11.呈上題，請計算該類別底下所有商品的平均價格");
            foreach (var prod in categoryList)
            {
                var item = prod.Where(x => x.Price > 1000);

                if (item != null)
                {
                    Console.WriteLine($"{prod.Key}有價格是大於 1000 的商品，如下");
                    foreach (var item2 in item)
                    {
                        Console.Write($"  {item2.Name}\n");
                    }
                }
                else
                {
                    Console.WriteLine($"{prod.Key}沒有價格是大於 1000 的商品");
                }
                Console.WriteLine(string.Format("{0}類平均價錢為{1, 0:C2}元\n", prod.Key, prod.Average(x => x.Price)));
            }
            Console.WriteLine();
            DisplayClearNext();

            // 12.依照商品價格由高到低排序
            Console.WriteLine("12. 依照商品價格由高到低排序");
            var sortProd = ProductList.OrderByDescending(x => x.Price);
            foreach (Product product in sortProd)
            {
                Console.WriteLine($"{product.ID,4}\t{product.Price,10}元\t剩餘數量{product.Amount,10} 個\t{product.Category}\t{product.Name}\n");
            }
            DisplayClearNext();

            // 13.依照商品數量由低到高排序
            Console.WriteLine("13.依照商品數量由低到高排序");
            sortProd = ProductList.OrderByDescending(x => 1/x.Price);
            foreach (Product product in sortProd)
            {
                Console.WriteLine($"{product.ID,4}\t{product.Price,10}元\t剩餘數量{product.Amount,10} 個\t{product.Category}\t{product.Name}\n");
            }
            DisplayClearNext();

            // 14.找出各商品類別底下，最貴的商品
            Console.WriteLine("14.找出各商品類別底下，最貴的商品");
            foreach (var prod in categoryList)
            {
                var maxP =
                    from _prod in prod
                    where _prod.Price == prod.Max(x => x.Price)
                    select _prod;

                foreach (var item in maxP)
                {
                    Console.WriteLine(string.Format("{0}類最貴商品為 {1} 價格為 {2}元\n", prod.Key, item.Name, item.Price));
                }
            }
            Console.WriteLine();
            // 15.找出各商品類別底下，最便宜的商品
            Console.WriteLine("15.找出各商品類別底下，最便宜的商品");
            foreach (var prod in categoryList)
            {
                var maxP =
                    from _prod in prod
                    where _prod.Price == prod.Min(x => x.Price)
                    select _prod;

                foreach (var item in maxP)
                {
                    Console.WriteLine(string.Format("{0}類最貴商品為 {1} 價格為 {2} 元\n", prod.Key, item.Name, item.Price));
                }
            }
            Console.WriteLine();

            // 16.找出價格小於等於 10000 的商品
            Console.WriteLine("16. 找出價格小於等於 10000 的商品");
            var minPList = ProductList.Where(x => x.Price <= 10000);
            foreach (Product product in minPList)
            {
                Console.WriteLine($"{product.ID,4}\t{product.Price,10}元\t剩餘數量{product.Amount,10} 個\t{product.Category}\t{product.Name}\n");
            }
            DisplayClearNext();

            // 17.製作一頁 4 筆總共 5 頁的分頁選擇器
            bool isOut = false;
            int page = 1;
            while (!isOut)
            {
                Console.WriteLine($"目前顯示第{page}頁總共5頁");
                switch (page)
                {
                    case 1:
                        DisplayProd(ProductList.Take(4));
                        break;
                    case 2:
                        DisplayProd(ProductList.Skip(4).Take(4));
                        break;
                    case 3:
                        DisplayProd(ProductList.Skip(8).Take(4));
                        break;
                    case 4:
                        DisplayProd(ProductList.Skip(12).Take(4));
                        break;
                    case 5:
                        DisplayProd(ProductList.Skip(16).Take(4));
                        break;
                    default:
                        Console.WriteLine("查無此頁面");
                        break;
                }
                Console.WriteLine("請問你要去第幾頁，離開請輸入(out/OUT)");
                string temp = Console.ReadLine();
                if (temp == "out" | temp == "OUT")
                {
                    isOut = true;
                }
                else
                {
                    int temp2 = Convert.ToInt32(temp);
                    if(temp2 > 1 | temp2 < 5)
                    {
                        page = temp2;
                    }
                }
                Console.Clear();
            }

            Console.ReadLine();
        }

        private static void ByItem(IEnumerable<Product> item)
        {
            if (item != null)
            {
                Console.WriteLine($"食品庫存有100件以上商品有");
                foreach (var item2 in item)
                {
                    Console.Write($"\t{item2.Name}\n");
                }
            }
            else
            {
                Console.WriteLine($"沒有食品庫存有100件以上商品\n");
            }
        }

        private static void DisplayProd(IEnumerable<Product> item)
        {
            foreach (var product in item)
            {
                Console.WriteLine($"{product.ID,4}\t{product.Price,10}元\t剩餘數量{product.Amount,10} 個\t{product.Category}\t{product.Name}\n");
            }
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

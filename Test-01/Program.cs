using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

            List<Product> _Product = new List<Product>();

            // 讀取資料
            string fileName = @"product.csv";
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            using(StreamReader sr = new StreamReader(fs))
            {
                string temp;
                while ((temp = sr.ReadLine()) != null)
                {
                    if(temp != "商品編號,商品名稱,商品數量,價格,商品類別")
                    {
                        string[] list;
                        list = temp.Split(',');
                        _Product.Add(new Product()
                        {
                            ID = list[0],
                            neme = list[1],
                            amount = Convert.ToInt32(list[2]),
                            price = Convert.ToDecimal(list[3]),
                            category = list[4],
                        });
                    }
                }
            }
            fs.Close();

            Display(_Product);

            Console.ReadLine(); 
        }

        static void Display(List<Product> p)
        {
            foreach(Product product in p)
            {
                Console.WriteLine($"{product.ID}, {product.neme}, {product.price}, {product.category}, {product.amount}");
            }
        }
    }
}

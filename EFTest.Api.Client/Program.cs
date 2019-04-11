using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EFTest.Api.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var urls = new string[] {
                "http://mydbcontext1.cooler.com:65149/student/create",
                "http://mydbcontext2.cooler.com:65149/student/create",
                "http://mydbcontext3.cooler.com:65149/student/create",
                "http://mydbcontext4.cooler.com:65149/student/create",
                "http://mydbcontext5.cooler.com:65149/student/create"
            };

            var dic = new ConcurrentDictionary<string, int>();
            Parallel.For(0, 100, (index) =>
            {
                var random = new Random();
                var url = urls[random.Next(0, 5)];
                var httpClient = new HttpClient();
                using (var response = httpClient.GetAsync(url).Result)
                {
                    Console.WriteLine($"Index:{index},Url:{url},Response:{response.Content.ToString()}");
                    if (dic.ContainsKey(url))
                    {
                        dic[url] = dic[url] + 1;
                    }
                    else
                    {
                        dic[url] = 1;
                    }
                }
            });
            Console.WriteLine();
            foreach (var item in dic.OrderBy(a => a.Key))
            {
                Console.WriteLine($"Result Url:{item.Key},Count:{item.Value}");
            }
            Console.ReadLine();
        }
    }
}

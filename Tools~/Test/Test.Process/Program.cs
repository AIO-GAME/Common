using System;
using System.Collections.Generic;
using System.IO;

namespace AIO
{
    class Program
    {
        private static void Main(string[] args)
        {
            Test();
            Console.Read();
        }

        static async void Test()
        {
            var data = new List<string>();
            data.Add("1111111");
            data.Add("2222222222222");
            var content = AHelper.Json.Serialize(data);
            // Console.WriteLine(await PrGCloud.ExistsAsync("rol-files/AIO/StandaloneWindows64/Default Package/Latest/Manifest.json"));
            Console.WriteLine(content);
            var data1 = AHelper.Json.Deserialize<List<string>>(content);
            foreach (var item in data1)
            {
                Console.WriteLine(item);
            }
        }
    }
}
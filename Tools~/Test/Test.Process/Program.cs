using System;
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
            Console.WriteLine(await PrGCloud.ExistsAsync("rol-files/AIO/StandaloneWindows64/Default Package/Latest/Manifest.json"));
        }
    }
}
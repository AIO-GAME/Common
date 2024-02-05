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
            var succeed1 =
               await PrGCloud.MetadataUpdateAsync($"rol-files/qc/Version/Android.json",
                    "cache-control", "no-cache");
            Console.WriteLine(succeed1);
        }
    }
}
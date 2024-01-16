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
            var succeed = await PrGCloud.MetadataUpdateAsync(
                "rol-files/qc/Android/Scene/Latest",
                "--cache-control", "no-cache");
        }
    }
}
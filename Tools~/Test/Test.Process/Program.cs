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
            var succeed1 = await PrGCloud.UploadDirAsync(
                "rol-files/AIO",
                "E:\\Project\\HOT\\HOT_DEV_35\\Bundles\\Android\\HOT\\Simulate",
                "--cache-control",
                "no-cache");
            Console.WriteLine(succeed1);
        }
    }
}
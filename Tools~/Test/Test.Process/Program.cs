using System;
using System.Collections.Generic;

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
            await PrGCloud.Storage.UploadFile(
                string.Concat("rol-files/Test", '/', "Android", '/', "DefaultPackage", '/', "2023-12-11-081221"),
                @"E:\WWW\Test\Android\DefaultPackage\2023-12-11-081221\defaultpackage_scenearena_405b792877fc496df867d19016bc9d4f.bundle",
                "--cache-control=no-cache");
        }
    }
}
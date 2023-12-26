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
            var result = await PrGCloud.Storage.UploadDir(
                    "rol-files",
                    @"E:\WWW\Test",
                    "--cache-control=no-cache")
                .Async();

            Console.WriteLine(result.StdALL);
        }
    }
}
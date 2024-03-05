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
            // var handle = AHandle.FTP.Create("ftpshare-hot.ingcreations.com", 21, "ftpshare-hot", "ingcreations2023",
            //     "qc");
            // Console.WriteLine(await handle.InitAsync());
            // var data = await handle.UploadFileAsync("E:\\Project\\HOT\\iOS.json", "Version/iOS.json");
            // Console.WriteLine(data);

            var h2 = AHandle.FTP.Create("ftpshare-hot.ingcreations.com", 21, "ftpshare-hot", "ingcreations2023");
            Console.WriteLine(await h2.InitAsync());
            Console.WriteLine(await h2.DeleteDirAsync("AIO"));
        }
    }
}
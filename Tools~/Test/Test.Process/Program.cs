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
            await PrDingTalk.SendText("Test",
                "ef2a15e5f980819007e3933b6ce0d701dfc772cfff6b8f40918a1d14294e6084",
                "SEC3e9b599d558bb124f14240b9fe9aabae3ae6998843818f262ea65aaf0a862d99");
        }
    }
}
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
            await PrDingTalk.SendMarkdown("任务:上传谷歌云资源", "#### 任务:上传谷歌云资源 \n > 本地路径不存在\n >",
                "51b339b8fbd7c7361de3c254d51b18b6b5437de2caf4f9ebedfc15b87c984e25",
                "SEC6d96f1c202620a3b8f8fe4c77608917f93ccf452537933338a2c9345ed413bc7");
        }
    }
}
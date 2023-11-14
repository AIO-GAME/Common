using System;
using System.Collections.Generic;

namespace AIO
{
    class Program
    {
        private static void Main(string[] args)
        {
            const string remote = "https://oapi.dingtalk.com/robot/send?access_token=xxx";
            const string secret = "xxx";

            var data = PrDingding.CreateData();
            PrDingding.SendMarkdown("title", "content", remote, secret).Sync();
            PrDingding.SendLink("title", "content", "messageUrl", "picUrl", remote, secret).Sync();
            PrDingding.SendText("content", remote, secret).Sync();
            PrDingding.SendActionCard("title", "content", "singleTitle", "singleURL", remote, secret).Sync();
            PrDingding.SendActionCard("title", "content", new Dictionary<string, string>(), 1, remote, secret).Sync();
            PrDingding.SendFeedCard(new List<Tuple<string, string, string>>(), remote, secret).Sync();
            PrDingding.SendData(data, remote, secret).Sync();
            Console.Read();
        }
    }
}
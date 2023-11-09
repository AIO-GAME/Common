using AIO;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// 这是一个密封类 GenericClosingException，继承自 Exception
    /// </summary>
    public sealed class GenericClosingException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericClosingException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericClosingException(Type open, Type closed) : base(
            $"Open-constructed type '{open}' is not assignable from closed-constructed type '{closed}'.")
        {
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            const string remote =
                "https://oapi.dingtalk.com/robot/send?access_token=ea1d6a0e15d732e40fac6e2005d19f603ff7e61fe70fa8128c31753c281db8e1";
            const string secret = "SEC4e0020badd524de7dd33198770cb5e4bb07bda30ee56dd405b763902e682ca5a";

            var data = PrDingding.CreateMSG();

            // data.ToLink("Unity Common 扩展包", "C# Basic universal library", "https://github.com/AIO-GAME/Common",
            //     "https://github.com/AIO-GAME/Common/blob/main/Documentation~/Logo.svg");
            // data.ToMarkdown("天气",
            //     "#### 天气 @150XXXXXXXX \n > 9度，西北风1级，空气良89，相对温度73%\n > ![screenshot](https://img.alicdn.com/tfs/TB1NwmBEL9TBuNjy1zbXXXpepXa-2400-1218.png)\n > ###### 10点20分发布 [天气](https://www.dingtalk.com) \n");

            data.ToActionCard(
                "Unity Common 扩展包",
                "C# Basic universal library",
                "",
                "",
                "");

            data.SetAtAll(false);

            PrDingding.SendData(remote, data, secret).Sync();
            Console.Read();
        }
    }
}
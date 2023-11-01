
using AIO;

class Program
{
    static void Main(string[] args)
    {
        var buffer = new BufferByte();
        {
            buffer.WriteUInt32(1);
            var value = buffer.ReadUInt32();
        }

        {
            buffer.WriteLen(10);
            var value = buffer.ReadLen();
        }

        {
            buffer.WriteStringUTF8("asdasdasd");
            var value = buffer.ReadStringUTF8();
            System.Console.WriteLine(value);
            System.Console.WriteLine(9 | 0x80);
            try
            {
                unchecked
                {
                    System.Console.WriteLine((sbyte)(9 | 0x80));
                    System.Console.WriteLine((sbyte)(-119 & 0x7F));
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }


        System.Console.Read();
    }
}
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Reflection;
//
// using AIO;
//
// using Newtonsoft.Json.Linq;
//
// /// <summary>
// /// 热更DLL
// /// </summary>
// [Serializable]
// public struct HotDllConfig
// {
//     /// <summary>
//     /// DLL名称
//     /// </summary>
//     public string Name;
//
//     /// <summary>
//     /// DLL相对路径
//     /// </summary>
//     public string RelativePath;
//
//     /// <summary>
//     /// DLL版本
//     /// </summary>
//     public string Version;
//
//     /// <summary>
//     /// 后缀
//     /// </summary>
//     public string Suffix;
//
//     /// <summary>
//     /// 文件MD5码
//     /// </summary>
//     public string MD5;
//
//     /// <summary>
//     /// 获取全局路径
//     /// </summary>
//     public string GetFullName(string dicName)
//     {
//         return Path.Combine(dicName, RelativePath, Version, string.Concat(Name, Suffix));
//     }
// }
//
// class Program
// {
//     private const int V = 0;
//
//     static void Main(string[] args)
//     {
//         Console.WriteLine((byte)(V | 0x80));
//         //var dic = new Dictionary<string, int>()
//         //{
//         //    { "1", 2 },
//         //    { "2", 2 },
//         //    { "3", 2 },
//         //    { "4", 2 },
//         //};
//         //dic.Set("a", 1);
//         //Console.WriteLine(dic.Get("1"));
//         //Console.WriteLine(dic.Get<int>("1"));
//         //// Test();
//         Console.Read();
//     }
//
//     public const string FileName = "ver.json";
//
//     public static async void Test()
//     {
//         var list = new List<string>();
//
//         var url = "http://192.168.2.39/HotUpdateDlls/Android";
//         var savePath = @"E:\WWW\HotUpdateDlls\IOS";
//         Console.WriteLine("当前下载地址为:" + url);
//         Console.WriteLine("当前下载路径为:" + savePath);
//
//         await new HttpDownload(Path.Combine(url, FileName), savePath).OnException(CharacterInfo =>
//         {
//             Console.WriteLine(" ------------------ 配置下载发生异常:{0} {1}", CharacterInfo.URL, CharacterInfo.Exception);
//         }).OnComplete(CharacterInfo => { Console.WriteLine(" ------------------ 配置下载完成 ------------------ "); });
//
//         try
//         {
//             var config = Utils.Json.Deserialize<HotDllConfig[]>(File.ReadAllText(Path.Combine(savePath, FileName)));
//             if (config == null) throw new ArgumentNullException($" ------------------ 文件下载错误 读取DllList.json失败");
//
//             var dic = new Dictionary<string, List<string>>();
//             foreach (var info in config)
//             {
//                 if (!File.Exists(info.GetFullName(savePath)))
//                 {
//                     if (dic.ContainsKey(info.Version))
//                     {
//                         dic[info.Version].Add(info.GetFullName(url));
//                     }
//                     else
//                     {
//                         dic.Add(info.Version, new List<string>());
//                         dic[info.Version].Add(info.GetFullName(url));
//                     }
//                 }
//             }
//
//             // 如果不存在 则添加下载 
//             if (dic.Count > 0)
//             {
//                 Console.WriteLine(" ------------------ 开始更新 DLL ------------------ ");
//                 foreach (var d in dic)
//                 {
//                     var path = Path.Combine(savePath, d.Key);
//                     Console.WriteLine(" ------------------ 开始更新 DLL ------------------ " + path);
//                     if (!Directory.Exists(path)) Directory.CreateDirectory(path);
//                     await new HttpDownload(d.Value, path).SetDownloadNum(5)
//                         .OnException(CharacterInfo => { Console.WriteLine(string.Format(" 配置下载发生异常: {0} {1}", CharacterInfo.URL, CharacterInfo.Exception)); }).OnComplete(downloadInfo =>
//                         {
//                             Console.WriteLine("URL : {0} 文件大小 : {1} 消耗时间 : {2} MD5 : {3}", downloadInfo.URL, downloadInfo.FileSize, downloadInfo.Time, downloadInfo.MD5);
//                         }).Async(20);
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex);
//         }
//
//         Console.WriteLine("--------------------------");
//         try
//         {
//             var alist = new List<Assembly>();
//             alist.Add(Assembly.Load(File.ReadAllBytes(@"E:\WWW\HotUpdateDlls\IOS\1.0.0.0\AIO.Utils.dbytes")));
//             alist.Add(Assembly.Load(File.ReadAllBytes(@"E:\WWW\HotUpdateDlls\IOS\1.0.0.0\AIO.Utils.Unity.Runtime.dbytes")));
//
//             foreach (var item in alist)
//             {
//                 Console.WriteLine(item.FullName);
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex);
//         }
//     }
// }
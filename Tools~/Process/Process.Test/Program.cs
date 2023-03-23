using AIO;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIO
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(" ------------ Start Program ------------ ");
            Test();
            Console.WriteLine(" ------------ End Program ------------ ");
            Console.Read();
        }

        private static void WIN11_TEST_PASS_API()
        {
            PrWin.Open.Wiaacmgr().Sync();
            PrWin.Open.Wscript().Sync();
            PrWin.Open.Mspaint().Sync();
            PrWin.Open.Mstsc().Sync();
            PrWin.Open.Magnify().Sync();
            PrWin.Open.Mmc().Sync();
            PrWin.Open.Mobsync().Sync();
            PrWin.Open.Dcomcnfg().Sync();
            PrWin.Open.Dvdplay().Sync();
            PrWin.Open.Notepad().Sync();
            PrWin.Open.Nslookup("www.baidu.com").Sync();
            PrWin.Open.Narrator().Sync();
            PrWin.Open.Sigverif().Sync();
            PrWin.Open.Taskmgr().Sync();
            PrWin.Open.Eventvwr().Sync();
            PrWin.Open.Eudcedit().Sync();
            PrWin.Open.Explorer(@"E:/Test/Test1").Sync();
            PrWin.Open.Perfmon().Sync();
            PrWin.Open.Regedit().Sync();
            PrWin.Open.Regedt32().Sync();
            PrWin.Open.Chkdsk().Sync();
            PrWin.Open.Calc().Sync();
            PrWin.Open.Charmap().Sync();
            PrWin.Open.SQLSERVER().Sync();
            PrWin.Open.Cleanmgr().Sync();
            PrWin.Open.OSK().Sync();
            PrWin.Open.Odbcad32().Sync();
            PrWin.Open.Utilman().Sync();
            PrWin.Open.Netstat().Sync();
            PrWin.Open.Sndrec32().Sync();
        }

        private static void MAC_TEST_PASS_API()
        {
            PrMac.Open.Path("").Sync();
            PrMac.Git.Add("").Sync();
            PrMac.Git.Clone("", "").Sync();
        }

        private async static void Test()
        {
            PrCourse.IsLog = false;

            //await PrWin.Open.ControlPanel();
            //Console.WriteLine("---------------");

            //PrWin.Open.Calc().Sync();
            //Console.WriteLine("---------------");
            //var a = PrWin.Open.Calc().OnComplete((ret) =>
            //{
            //    Console.WriteLine("Calc");
            //});

            //PrWin.Open.ControlPanel().Link(a).OnComplete((ret) =>
            //{
            //    Console.WriteLine("ControlPanel");
            //}).Async();

            //await PrWin.Certutil.MD5(@"E:\TencentGit\Packages\com.self.package").OnProgress
            //((o, e) =>
            //{
            //    Console.WriteLine(" -------------OnProgress------------- ");
            //    Console.WriteLine(e.Data);
            //}).OnComplete((r) =>
            //{
            //    Console.WriteLine(" -------------OnComplete------------- ");
            //    Console.WriteLine(r.StdALL);
            //});

            //var b = await new Awaiter();
            //Console.WriteLine(b.value);


            //var path = @"Packages\\com\.[^\\]+\.package\\Plugins~\\RainbowAssets";

            //if (Regex.IsMatch(@"^[^\\/:*?\""<>|,]+$", Path.GetFileName(path)))
            //{
            //    Console.WriteLine("请勿在文件名中包含\\ / : * ？ \" < > |等字符，请重新输入有效文件名！");
            //}
            //else Console.WriteLine("文件名有效");

            //if (Regex.IsMatch(path, @"^([a-zA-Z]:\\)?[^/:*?\""<>|,]*$"))
            //{
            //    if (Regex.IsMatch(path, @"^([a-zA-Z]:\\)?[^/:*?\""<>|,]*" + path))
            //    {
            //        Console.WriteLine("文件路径有效");
            //    }
            //    else Console.WriteLine("非法的文件保存路径，请重新选择或输入！");
            //}
            //else Console.WriteLine("文件路径有效");

            var root = new DirectoryInfo(@"E:\TencentGit\AIO20200337");
            var value = "Packages\\\\[^\\\\]*\\\\Plugins~\\\\BeastConsole";
            var name = Path.GetFileName(value);
            Console.WriteLine(name);
            try
            {
                var regex = new Regex(value + "\\b");
                foreach (var directory in root.GetDirectories("*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        if (directory.Name == name && regex.Match(directory.FullName).Success)
                            Console.WriteLine(directory.FullName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} => {1}", directory.FullName, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static IEnumerator enumerator()
        {
            yield return PrWin.Open.ControlPanel();
            Console.WriteLine("---------------");
            yield return PrWin.Open.Calc();
            Console.WriteLine("---------------");
        }

        public class Awaiter
        {
            public static int v = 0;
            Result r;

            public TaskAwaiter<Result> GetAwaiter()
            {
                Console.WriteLine(" ----------- GetAwaiter ----------- ");
                return Task.Factory.StartNew(() =>
                {
                    while (r.IsCompleted == false)
                        if (v++ >= 1000)
                            r.Finish();

                    return r;
                }).GetAwaiter();
                //while (r.IsCompleted == false)
                //{
                //    //Console.WriteLine(" ----------- 等待异步操作 ----------- {0}", v++);
                //    v++;
                //}
                //return r;
            }

            public Awaiter()
            {
                r = new Result();
            }
        }

        public class Result : ICriticalNotifyCompletion, INotifyCompletion
        {
            public int value { get; private set; }

            public bool IsCompleted { get; private set; }

            public Result()
            {
                value = 99;
                IsCompleted = false;
            }

            public void Finish()
            {
                IsCompleted = true;
            }

            public Result GetResult()
            {
                Console.WriteLine(" ----------- GetResult ----------- ");
                return this;
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                continuation();
                Console.WriteLine(" ----------- Unsafe OnCompleted ----------- ");
            }

            public void OnCompleted(Action continuation)
            {
                continuation();
                Console.WriteLine(" ----------- On Completed ----------- ");
            }
        }
    }
}
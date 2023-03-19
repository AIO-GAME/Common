namespace Attribute.Test
{
    using AIO;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 打包指令集
    /// </summary>
    public class BuildArgsCommand : ArgumentCustom
    {
        /// <summary>
        /// 配置路径
        /// </summary>
        public const string CFGPATH = "b@cfgpath";

        /// <summary>
        /// 输出路径
        /// </summary>
        public const string BUILDFOLDER = "b@buildfolder";

        /// <summary>
        /// 安卓工程路径
        /// </summary>
        public const string ANDROIDPROJECT = "b@androidproject";

        /// <summary>
        /// 配置路径
        /// </summary>
        [Argument(CFGPATH, EArgLabel.String)]
        public string cfgpath;

        /// <summary>
        /// 输出路径
        /// </summary>
        [Argument(BUILDFOLDER, EArgLabel.String)]
        public string buildfolder;

        /// <summary>
        /// 安卓工程路径
        /// </summary>
        [Argument(ANDROIDPROJECT, EArgLabel.String)]
        public string androidproject;
    }

    class Program
    {
        static void Main(string[] args)
        {
            AIO.Script.Test.AAA();
            Console.WriteLine(" -------------------- ");
            try
            {
                Test();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine(" -------------------- ");
            Console.Read();
        }

        public static void Test()
        {
            var list = new string[]
            {
                "AIO.Build.Editor.EditorCommand.BuildIOS",
                "b@cfgpath",
                "/Applications/xcarray/SAM/AIO20200337/Build/iclcokworken/g201-chiyu-iclockworken.ios.json",
                "b@androidproject",
                "NULL",
                "b@buildfolder",
                "/Applications/xcarray/SAM/IOSBuild"
            };

            var argsss = ArgumentUtils.ResolverCustom<BuildArgsCommand>(list);
            Console.WriteLine(argsss.androidproject);
            Console.WriteLine(argsss.buildfolder);
            Console.WriteLine(argsss.cfgpath);
        }

        public static void Test2(IEnumerable<string> list)
        {
            Console.WriteLine(string.Join(" ", list));
        }
    }
}

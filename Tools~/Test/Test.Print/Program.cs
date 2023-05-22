using System;

namespace AIO
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(" - ----------------------- - ");

            try
            {
                Print.Show(EPrint.ALL);
                Test1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine(" - ----------------------- -   ");
            Console.Read();
        }

        public static void Test1()
        {
            var svnroot = @"E:\Work-G\g108\config";
            var extension = "txt";

            PrSvn.Update.ALL(svnroot).Sync().Debug();
            PrSvn.Add.ALLWithExtension(svnroot, extension).Sync().Debug();
            PrSvn.Commit.Execute(svnroot, "SVN测试").Sync().Debug();
        }
    }
}
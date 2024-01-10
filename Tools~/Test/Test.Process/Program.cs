using System;

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
            var tem = await PrGCloud.Storage.UploadDir("rol-files/Test",
                $"E:\\WWW\\Test\\Android\\DefaultPackage\\2023-12-11-081223");
            Console.WriteLine(tem.StdALL);
        }
    }
}
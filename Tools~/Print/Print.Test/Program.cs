namespace AIO
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
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

        static void Test1()
        {
            var bbb = new Dictionary<string, int>(10);
            for (int i = 0; i < 10; i++)
            {
                bbb.Add(DateTime.Now.AddDays(i).ToString(), i);
            }

            CPrint.Log(bbb);
        }
    }
}
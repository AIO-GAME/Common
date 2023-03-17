namespace AIO
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            // Print.Show();

            Console.Read();
        }


        static void Test1()
        {
            // CPrint.Line("-------------------------");
            var bbb = new Dictionary<string, int>(10);
            for (int i = 0; i < 10; i++)
            {
                bbb.Add(DateTime.Now.AddDays(i).ToString(), i);
            }

            // CPrint.Line(bbb);
            // CPrint.Line("-------------------------");
        }


        static void Test2()
        {
            // CPrint.Line("-------------------------");
            var bbb = new List<string>(10);
            for (int i = 0; i < 10; i++)
            {
                bbb.Add(DateTime.Now.AddDays(i).ToString());
            }


            // CPrint.Array(bbb);
            // CPrint.Line("-------------------------");
        }
    }
}
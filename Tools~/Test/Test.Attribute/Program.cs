using System;
using System.Collections.Generic;

namespace Attribute.Test
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }

        public static void Test2(IEnumerable<string> list)
        {
            Console.WriteLine(string.Join(" ", list));
        }
    }
}
using System;
using System.Reflection;

namespace ZTest
{
    class Program
    {
        [GCommand(101, Help = "获取浮点数属性 arg")]
        public static void GetActorAAAA()
        {
            Console.WriteLine("获取浮点数属性");
        }

        [GCommand(102, Help = "获取浮点数属性 arg")]
        public static void GetActor()
        {
            Console.WriteLine("获取浮点数属性");
        }

        [GCommand(102, Help = "获取浮点数属性 arg1")]
        public static void GetActor(int roidid)
        {
            Console.WriteLine("name:{0} v1:{1}", nameof(GetActor), roidid);
        }

        [GCommand(102, Help = "获取浮点数属性 arg1")]
        public static void GetActor(string roidid)
        {
            Console.WriteLine("name:{0} v1:{1}", nameof(GetActor), roidid);
        }

        [GCommand(102, Help = "获取浮点数属性 arg2")]
        public static void GetActor(int v1, int v2)
        {
            Console.WriteLine("name:{0} v1:{1} v2:{2}", nameof(GetActor), v1, v2);
        }

        [GCommand(103)]
        public static void GetConfig(int v1)
        {
            Console.WriteLine("name:{0} v1:{1}", nameof(GetConfig), v1);
        }

        [GCommand(103)]
        public static void GetConfig(int v1, string v2)
        {
            Console.WriteLine("name:{0} v1:{1} v2:{2}", nameof(GetConfig), v1, v2);
        }

        [GCommand(103)]
        public static void GetConfig(int v1, string v2, bool v3)
        {
            Console.WriteLine("name:{0} v1:{1} v2:{2} v3{3}",
                nameof(GetConfig), v1, v2, v3);
        }

        [GCommand(103)]
        public static void GetConfig(int v1, string v2, int v3, bool v4)
        {
            Console.WriteLine("name:{0} v1:{1} v2:{2} v3{3}",
                nameof(GetConfig), v1, v2, v3, v4);
        }

        static void Main(string[] args)
        {
            try
            {
                GCommandSystem.Initialize();
                GCommandSystem.Register(Assembly.GetExecutingAssembly());
                GCommandSystem.Debug();
                Console.WriteLine("--------------------------");
                //GCommandSystem.Invoke("[102:\"asdasd\"]");
                GCommandSystem.Invoke("[102:asdasd]");
                //GCommandSystem.Invoke("[103:1]");
                //GCommandSystem.Invoke("[103:1,\"2\",True]");
                //GCommandSystem.Invoke("[103:1,\"2\",False]");
                //GCommandSystem.Invoke("[103:1,\"2\",1,False]");
                //GCommandSystem.Invoke("[103:1,\"2\",1,False,True]");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
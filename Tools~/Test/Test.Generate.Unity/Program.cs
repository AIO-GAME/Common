using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AIO;

namespace ZTest
{
    [GBeanRegister(2)]
    public class SettingBean2 : GBean
    {
        /// <summary>
        /// 音量
        /// </summary>
        public static float Volume;

        /// <summary>
        /// 开关
        /// </summary>
        public static bool OnOff;

        public static List<string> aaaaa;

        public override void Deserialize(IReadData buffer)
        {
            Volume = buffer.ReadFloat();
            OnOff = buffer.ReadBool();
            var len = buffer.ReadLen();
            for (int i = 0; i < len; i++) aaaaa.Add(buffer.ReadString());
        }

        public override void Serialize(IWriteData buffer)
        {
            buffer.WriteFloat(Volume);
            buffer.WriteBool(OnOff);
            buffer.WriteLen(aaaaa.Count);
            for (int i = 0; i < aaaaa.Count; i++) buffer.WriteString(aaaaa[i]);
        }

        public override void Initialize()
        {
            Volume = 1;
            OnOff = false;
            aaaaa = new List<string>();
        }

        public override void Frist()
        {
            Volume = 1;
            OnOff = false;
        }
    }

    [GBeanRegister(1)]
    public class SettingBean : GBean
    {
        /// <summary>
        /// 音量
        /// </summary>
        public static float Volume;

        /// <summary>
        /// 开关
        /// </summary>
        public static bool OnOff;

        public static List<string> aaaaa;

        public override void Deserialize(IReadData buffer)
        {
            Volume = buffer.ReadFloat();
            OnOff = buffer.ReadBool();
            var len = buffer.ReadLen();
            for (int i = 0; i < len; i++) aaaaa.Add(buffer.ReadString());
        }

        public override void Serialize(IWriteData buffer)
        {
            buffer.WriteFloat(Volume);
            buffer.WriteBool(OnOff);
            buffer.WriteLen(aaaaa.Count);
            for (int i = 0; i < aaaaa.Count; i++) buffer.WriteString(aaaaa[i]);
        }

        public override void Initialize()
        {
            Volume = 1;
            OnOff = false;
            aaaaa = new List<string>();
        }

        public override void Frist()
        {
            Volume = 1;
            OnOff = false;
        }
    }

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
                GBean();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }

        static async void GBean()
        {
            GBeanSystem.Progress = (progress) =>
            {
                Console.WriteLine("数据读取进度 => {0:P3}", progress);
            };

            Console.WriteLine(" -----------< >-----------< >----------- ");
            if (File.Exists(@"E:\Work-G\g108\Archived.dat")) File.Delete(@"E:\Work-G\g108\Archived.dat");
            GBeanSystem.Initialize(@"E:\Work-G\g108\Archived.dat");
            Console.WriteLine(" -----------< > Load  ");
            await GBeanSystem.Load();
            Console.WriteLine(" -----------< > Save  ");
            GBeanSystem.Save();
            Console.WriteLine(" -----------< > Load  ");
            await GBeanSystem.Load();
        }

        static void GCommand()
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
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// 游戏数据储存管理
    /// </summary>
    public static class GBeanSystem
    {
        /// <summary>
        /// 数据
        /// </summary>
        internal static Dictionary<int, GBean> Data;

        /// <summary>
        /// 源数据
        /// </summary>
        internal static BufferByte Bean;

        /// <summary>
        /// 进度回调
        /// </summary>
        public static Action<float> Progress { get; set; }

        /// <summary>
        /// 目标存储读取路径
        /// </summary>
        internal static string TargetPath;

        private static MD5CryptoServiceProvider MD5Crypto;

        static GBeanSystem()
        {
            Data = new Dictionary<int, GBean>();
            MD5Crypto = new MD5CryptoServiceProvider();
            MD5Crypto.Initialize();
        }

        /// <summary>
        /// 获取二进制MD5码
        /// </summary>
        private static string GetMD5(in byte[] buffer)
        {
            var MD5Crypto = new MD5CryptoServiceProvider();
            var Builder = new StringBuilder();

            var retVal = MD5Crypto.ComputeHash(buffer);
            foreach (var item in MD5Crypto.ComputeHash(buffer))
                Builder.Append(item.ToString("X2"));

            return Builder.ToString();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void Initialize(in string targetPath)
        {
            TargetPath = targetPath;
            var attributeType = typeof(GBeanRegisterAttribute);
            var gBeanType = typeof(GBean);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract) continue;
                    var attribute = type.GetCustomAttribute(attributeType, false);
                    if (type.IsSubclassOf(gBeanType))
                    {
                        if (attribute is GBeanRegisterAttribute gBean)
                        {
                            if (!Data.ContainsKey(gBean.ID))
                            {
                                Data.Add(gBean.ID, (GBean)Activator.CreateInstance(type));
                                Data[gBean.ID].Initialize();
                            }
                            else
                                throw new Exception(string.Format(
                                    "GBean 出现重复 ID !!! 请检查 Info => ID : {0} | Current Type Name : {1} | Exist Type Name : {2}",
                                    gBean.ID, type.FullName, Data[gBean.ID].GetType().FullName
                                ));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public static Task Load()
        {
            if (!File.Exists(TargetPath))
            {
                foreach (var item in Data) item.Value.First();
                return Task.CompletedTask;
            }

            Bean = new BufferByte(File.ReadAllBytes(TargetPath));

            var task = new List<Task>();
            task.Add(Task.Factory.StartNew(() =>
            {
                var ids = Bean.ReadLen();
                Console.WriteLine("Load 总数据长度: {0} -> IDS: {1}", Bean.WriteOffset, ids);
                for (int i = 0; i < ids; i++)
                {
                    var id = Bean.ReadInt32();
                    var md5 = Bean.ReadStringUTF8();
                    var bufferCount = Bean.ReadLen();
                    var startIndex = Bean.ReadInt32();

                    if (Data.ContainsKey(id))
                    {
                        var buffer = Bean.Read(startIndex, bufferCount);
                        var verifyMD5 = GetMD5(buffer);
                        if (verifyMD5 == md5)
                        {
                            Data[id].Deserialize(buffer);
                            Console.WriteLine("数据块ID: {0} -> 写入下标: {1} -> 数据长度: {2} -> MD5: {3}",
                                id, startIndex, bufferCount, md5);
                        }
                        else
                            Console.WriteLine("数据块ID: {0} -> 写入下标: {1} -> 数据长度: {2} -> MD5: {3} != {4} => 数据MD5验证失败",
                                id, startIndex, bufferCount, md5, GetMD5(buffer.ToArray()));
                    }
                    else
                        Console.WriteLine("数据块ID: {0} -> 写入下标: {1} -> 数据长度: {2} -> MD5: {3} =>  未查询到指定数据ID",
                            id, startIndex, bufferCount, md5);

                    Bean.Skip(bufferCount);
                }
            }));

            if (Bean != null && Bean.WriteOffset > 0 && Progress != null)
            {
                task.Add(Task.Factory.StartNew(() =>
                {
                    while (Bean.Count != 0)
                    {
                        Progress.Invoke(Bean.ReadOffset / (float)Bean.WriteOffset);
                        Task.Delay(1000);
                    }

                    Progress.Invoke(1);
                }));
            }

            return Task.WhenAny(task.ToArray());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static void Save()
        {
            var root = new BufferByte();
            var bytes = new BufferByte();
            root.WriteLen(Data.Count);
            foreach (var item in Data)
            {
                bytes.Clear();
                item.Value.Serialize(bytes);
                var md5 = GetMD5(bytes);

                root.WriteInt32(item.Key);
                root.WriteStringUTF8(md5);
                root.WriteLen(bytes.Count);
                var startindex = root.WriteOffset + 5;
                root.WriteInt32(root.WriteOffset + 5);
                root.Write(bytes.ToArray());

                Console.WriteLine("数据块ID: {0} -> 写入下标: {1} -> 数据长度: {2} -> MD5: {3}",
                    item.Key, startindex, bytes.Count, md5);
            }

            Console.WriteLine("Save 总数据长度: {0} -> IDS: {1}", root.Count, Data.Count);
            File.WriteAllBytes(TargetPath, root);
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public static void Clean()
        {
            foreach (var item in Data)
            {
                item.Value.Clean();
            }
        }

        /// <summary>
        /// 重置数据
        /// </summary>
        public static void Reset()
        {
            foreach (var item in Data)
            {
                item.Value.Reset();
            }
        }
    }
}
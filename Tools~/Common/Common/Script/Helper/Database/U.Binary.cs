using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

public partial class AHelper
{
    /// <summary>
    /// Binary 文件 解析
    /// </summary>
    public static partial class Binary
    {
        /// <summary>
        /// 序列化 未加密
        /// </summary>
        public static byte[] Serialize<T>(in T data)
        {
            using (var stream = new MemoryStream())
            {
                var bf = new BinaryFormatter(); //创建序列化的对象
                bf.Serialize(stream, data); //将数据序列化
                stream.Flush();
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 反序列化 未加密
        /// </summary>
        public static T Deserialize<T>(in byte[] data)
        {
            try
            {
                using (var stream = new MemoryStream(data))
                {
                    stream.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)new BinaryFormatter().Deserialize(stream); //反序列化回原来的数据格式；
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return default;
        }
    }
}
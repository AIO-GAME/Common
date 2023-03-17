/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AIO
{
    /// <summary>
    /// Dat文件 解析
    /// </summary>
    public static partial class ParseUtils
    {
        /// <summary>
        /// 序列化 未加密
        /// </summary>
        public static byte[] BinarySerialize<T>(T data)
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
        /// 序列化 未加密
        /// </summary>
        public static string Base64Serialize<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, data); //将数据序列化
                stream.Flush();
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>
        /// 反序列化 未加密
        /// </summary>
        public static T Base64Deserialize<T>(string data)
        {
            using (var stream = new MemoryStream())
            {
                var bytes = Convert.FromBase64String(data);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)new BinaryFormatter().Deserialize(stream); //反序列化回原来的数据格式；
            }
        }

        /// <summary>
        /// 反序列化 未加密
        /// </summary>
        public static T BinaryDeserialize<T>(byte[] data)

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
                throw e;
            }
        }
    }
}
/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public partial class UtilsGen
{
    /// <summary>
    /// Base64 文件 解析
    /// </summary>
    public static partial class Base64
    {
        /// <summary>
        /// 序列化 未加密
        /// </summary>
        public static string Serialize<T>(T data) 
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, data); //将数据序列化
                stream.Flush();
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>
        /// 序列化 未加密
        /// </summary>
        public static string Serialize<T>(in T data) where T : struct
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, data); //将数据序列化
                stream.Flush();
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>
        /// 序列化 未加密
        /// </summary>
        public static string Serialize(byte[] data, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(data, options);
        }

        /// <summary>
        /// 序列化 未加密
        /// </summary>
        public static string Serialize(string data, Encoding encoding = null)
        {
            return Convert.ToBase64String((encoding ?? Encoding.UTF8).GetBytes(data));
        }


        /// <summary>
        /// 反序列化 未加密
        /// </summary>
        public static T Deserialize<T>(string data)
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
    }
}
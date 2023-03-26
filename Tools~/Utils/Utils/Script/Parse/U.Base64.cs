/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

public partial class Utils
{
    /// <summary>
    /// Base64 文件 解析
    /// </summary>
    public static partial class Base64
    {
        /// <summary>
        /// 序列化 未加密
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Serialize<T>(in T data)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Deserialize<T>(in string data)
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

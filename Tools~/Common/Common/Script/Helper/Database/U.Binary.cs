#region

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Binary

        /// <summary>
        /// Binary 文件 解析
        /// </summary>
        public class Binary
        {
            /// <summary>
            /// 序列化 未加密
            /// </summary>
            public static byte[] Serialize<T>(in T data)
            {
                using (var stream = new MemoryStream())
                {
                    var bf = new BinaryFormatter(); //创建序列化的对象
                    bf.Serialize(stream, data);     //将数据序列化
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
                    CS.WriteLine(e);
                }

                return default;
            }
        }

        #endregion
    }
}
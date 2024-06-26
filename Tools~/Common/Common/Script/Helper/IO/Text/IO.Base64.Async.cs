#region

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 读取 Base64 文件 编码utf-8
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task<T> ReadBase64Async<T>(string path, string charset = "utf-8")
            {
                var Content = await ReadTextAsync(path, charset);
                return Base64.Deserialize<T>(Content);
            }

            /// <summary>
            /// 读取 Base64 文件 编码utf-8
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task<T> ReadBase64UTF8Async<T>(string path)
            {
                var Content = await ReadUTF8Async(path);
                return Base64.Deserialize<T>(Content);
            }

            /// <summary>
            /// 写入 Base64 文件 编码utf-8
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task<bool> WriteBase64Async<T>(string path, T value, string charset = "utf-8")
            {
                if (value == null) return false;
                return await WriteTextAsync(path, Base64.SerializeData(value), charset);
            }

            /// <summary>
            /// 写入 Base64 文件 编码utf-8
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task<bool> WriteBase64UTF8Async<T>(string path, T text)
            {
                if (text == null) return false;
                return await WriteUTF8Async(path, Base64.SerializeData(text));
            }
        }

        #endregion
    }
}
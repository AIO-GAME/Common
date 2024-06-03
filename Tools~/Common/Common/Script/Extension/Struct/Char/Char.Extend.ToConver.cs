#region

using System;
using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 字符扩展
    /// </summary>
    public static partial class ExtendChar
    {
        /// <summary>
        /// 转化为字节
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ToConverSByte(this char value)
        {
            return Convert.ToSByte(value);
        }

        /// <summary>
        /// 转化为字节数组
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToConverBytes(this char value, in Encoding encoding = null)
        {
            return (encoding ?? Encoding.Default).GetBytes(new[] { value });
        }

        /// <summary>
        /// 获取Base64
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverBase64(this char value, in Encoding encoding = null)
        {
            return Convert.ToBase64String((encoding ?? Encoding.Default).GetBytes(value.ToString()));
        }
    }
}
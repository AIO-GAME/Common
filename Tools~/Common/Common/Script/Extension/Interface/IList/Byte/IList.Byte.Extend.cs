using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    public partial class IListExtend
    {
        /// <summary>
        /// 从指定字节数组中 获取多个字节转化为大写16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">获取长度</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverHex16X(this IList<byte> bytes, int offset, int count)
        {
            var sb = new StringBuilder();
            for (var i = offset; i < offset + count; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 从指定字节数组中 获取多个字节转化为大写16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">获取长度</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverHex16x(this IList<byte> bytes, int offset, int count)
        {
            var sb = new StringBuilder();
            for (var i = offset; i < offset + count; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
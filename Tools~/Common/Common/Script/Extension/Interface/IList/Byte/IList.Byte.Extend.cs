using System.Collections.Generic;
using System.Text;

namespace AIO
{
    public partial class IListExtend
    {
        /// <summary>
        /// 从指定字节数组中 获取多个字节转化为大写16进制字符串
        /// </summary>
        /// <param name="bytes">数组源</param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">获取长度</param>
        /// <param name="isLower">是否小写</param>
        public static string ToConverseHex16X(this IList<byte> bytes, int offset, int count, bool isLower = false)
        {
            var sb = new StringBuilder();
            for (var i = offset; i < offset + count; i++) sb.Append(bytes[i].ToString(isLower ? "x2" : "X2"));
            return sb.ToString();
        }
    }
}
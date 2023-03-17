/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System.Text;

    /* Tips : 转化为16进制字符串。
     * 大写X：ToString("X2")即转化为大写的16进制。
     * 小写x：ToString("x2")即转化为小写的16进制。
     * 2表示输出两位，不足2位的前面补0,如 0x0A 如果没有2,就只会输出0xA
     */

    /// <summary>
    /// 字节扩展 包含类型 byte byte[]
    /// </summary>
    public static partial class ByteExtendToConver
    {
        /// <summary>
        /// 字节 转小写16进制字符
        /// </summary>
        public static string ToConverHex16x(this byte bytes)
        {
            return HexUtils.ToHex(bytes, false);
        }

        /// <summary>
        /// 字节数组 转小写16进制字符
        /// </summary>
        public static string ToConverHex16x(this byte[] bytes)
        {
            return HexUtils.ToHex(bytes, false);
        }

        /// <summary>
        /// 字节 转大写16进制字符
        /// </summary>
        public static string ToConverHex16X(this byte bytes)
        {
            return HexUtils.ToHex(bytes, true);
        }

        /// <summary>
        /// 字节数组 转大写16进制字符
        /// </summary>
        public static string ToConverHex16X(this byte[] bytes)
        {
            return HexUtils.ToHex(bytes, true);
        }

        /// <summary>
        /// 从指定字节数组中 获取多个字节转化为大写16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">获取长度</param>
        public static string ToConverHex16X(this byte[] bytes, int offset, int count)
        {
            return HexUtils.ToHex(bytes, offset, count, true);
        }

        /// <summary>
        /// 从指定字节数组中 获取多个字节转化为大写16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">获取长度</param>
        public static string ToConverHex16x(this byte[] bytes, int offset, int count)
        {
            return HexUtils.ToHex(bytes, offset, count, false);
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        public static string ToConverString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 获取指定字节数组转化为字符串
        /// </summary>
        public static string ToConverString(this byte[] bytes, int offset, int count)
        {
            return Encoding.Default.GetString(bytes, offset, count);
        }

        /// <summary>
        /// 转化为字符串 UTF8格式
        /// </summary>
        public static string ToConverStringUTF8(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 获取指定字节数组转化为字符串 UTF8格式
        /// </summary>
        public static string ToConverStringUTF8(this byte[] bytes, int offset, int count)
        {
            return Encoding.UTF8.GetString(bytes, offset, count);
        }
    }
}

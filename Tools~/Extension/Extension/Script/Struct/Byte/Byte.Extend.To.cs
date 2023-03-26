/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Runtime.CompilerServices;

namespace AIO
{
    /* Tips : 转化为16进制字符串。
     * 大写X：ToString("X2")即转化为大写的16进制。
     * 小写x：ToString("x2")即转化为小写的16进制。
     * 2表示输出两位，不足2位的前面补0,如 0x0A 如果没有2,就只会输出0xA
     */

    /// <summary>
    /// 字节扩展 包含类型 byte byte[]
    /// </summary>
    public static partial class ByteExtend
    {
        /// <summary>
        /// 字节 转小写16进制字符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConvertHex16x(this byte bytes)
        {
            return bytes.ToString("x2");
        }

        /// <summary>
        /// 字节 转大写16进制字符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConvertHex16X(this byte bytes)
        {
            return bytes.ToString("X2");
        }
    }
}
#region

using System;

#endregion

namespace AIO
{
    public static partial class ExtendString
    {
        /// <summary>
        ///     将整数类型的值转换为指定长度的二进制字符串。如果二进制字符串长度小于指定长度，则在左侧使用字符 '0' 进行填充。
        /// </summary>
        /// <param name="value">要进行转换的整数值。</param>
        /// <returns>转换后的二进制字符串。</returns>
        public static string ToBinaryString(this int value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        /// <summary>
        ///     将长整型类型的值转换为指定长度的二进制字符串。如果二进制字符串长度小于指定长度，则在左侧使用字符 '0' 进行填充。
        /// </summary>
        /// <param name="value">要进行转换的长整型值。</param>
        /// <returns>转换后的二进制字符串。</returns>
        public static string ToBinaryString(this long value)
        {
            return Convert.ToString(value, 2).PadLeft(16, '0');
        }

        /// <summary>
        ///     将枚举类型的值转换为指定长度的二进制字符串。如果二进制字符串长度小于指定长度，则在左侧使用字符 '0' 进行填充。
        /// </summary>
        /// <param name="value">要进行转换的枚举值。</param>
        /// <returns>转换后的二进制字符串。</returns>
        public static string ToBinaryString(this Enum value)
        {
            return Convert.ToString(Convert.ToInt64(value), 2).PadLeft(16, '0');
        }
    }
}
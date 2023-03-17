/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System.Text;

    /// <summary>
    /// 字符扩展
    /// </summary>
    public static partial class CharExtend
    {
        /// <summary>
        /// 判断当前字符是否为单字节
        /// </summary>
        public static bool IsSingleByte(this char value)
        {
            // 使用中文支持编码
            return Encoding.BigEndianUnicode.GetByteCount(new char[] { value }) == 1;
        }

        /// <summary>
        /// 判断当前字符 是否为数字
        /// </summary>
        public static bool IsDigit(this char value)
        {
            return char.IsDigit(value);
        }

        /// <summary>
        /// 判断当前字符 是否是一个高代理项
        /// </summary>
        public static bool IsHighSurrogate(this char value)
        {
            return char.IsHighSurrogate(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于 Unicode 字母类别
        /// </summary>
        public static bool IsLetter(this char value)
        {
            return char.IsLetter(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于字母或十进制数字类别
        /// </summary>
        public static bool IsLetterOrDigit(this char value)
        {
            return char.IsLetterOrDigit(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于小写字母类别
        /// </summary>
        public static bool IsLower(this char value)
        {
            return char.IsLower(value);
        }

        /// <summary>
        /// 判断当前字符 是否是一个低代理项
        /// </summary>
        public static bool IsLowSurrogate(this char value)
        {
            return char.IsLowSurrogate(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于 Unicode 数字
        /// </summary>
        public static bool IsNumber(this char value)
        {
            return char.IsNumber(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于标点符号类别
        /// </summary>
        public static bool IsPunctuation(this char value)
        {
            return char.IsPunctuation(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于分隔符类别
        /// </summary>
        public static bool IsSeparator(this char value)
        {
            return char.IsSeparator(value);
        }

        /// <summary>
        /// 判断当前字符 是否具有代理项代码单位
        /// </summary>
        public static bool IsSurrogate(this char value)
        {
            return char.IsSurrogate(value);
        }

        /// <summary>
        /// 判断当前两个字符 是否形成一个代理项对
        /// </summary>
        public static bool IsSurrogatePair(this char value, char value1)
        {
            return char.IsSurrogatePair(value, value1);
        }

        /// <summary>
        /// 判断当前字符 是否属于符号字符类别
        /// </summary>
        public static bool IsSymbol(this char value)
        {
            return char.IsSymbol(value);
        }

        /// <summary>
        /// 判断当前字符 是否为大小字母类别
        /// </summary>
        public static bool IsUpper(this char value)
        {
            return char.IsUpper(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于空格类别
        /// </summary>
        public static bool IsWhiteSpace(this char value)
        {
            return char.IsWhiteSpace(value);
        }

        /// <summary>
        /// 判断当前字符 是否属于中文字符
        /// </summary>
        public static bool IsChinese(this char value)
        {
            return value > 0x80;
        }
    }
}

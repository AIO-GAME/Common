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
        public static int GetBytesLength(this char value)
        {   // 使用中文支持编码
            return Encoding.BigEndianUnicode.GetByteCount(new char[] { value });
        }

        /// <summary>
        /// 获取 ASCII 码
        /// </summary>
        public static int GetCodeASCII(this char value)
        {
            var codebytes = Encoding.ASCII.GetBytes(value.ToString());
            if (value.IsSingleByte())
            {
                return codebytes[0];
            }
            else
            {   // 双字节码为高位乘256，再加低位 该为无符号码，再减65536
                return codebytes[0] * 256 + codebytes[1] - 65536;
            }
        }

        /// <summary>
        /// 获取 Unicode 码
        /// </summary>
        public static int GetCodeUnicode(this char value)
        {
            return value;
        }
    }
}

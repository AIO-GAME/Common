#region

using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace AIO
{
    public partial class ExtendChar
    {
        /// <summary>
        /// 判断当前字符是否为单字节
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBytesLength(this char value)
        {
            // 使用中文支持编码
            return Encoding.BigEndianUnicode.GetByteCount(new[] { value });
        }

        /// <summary>
        /// 获取 ASCII 码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCodeASCII(this char value)
        {
            var codebase = Encoding.ASCII.GetBytes(value.ToString());
            if (value.IsSingleByte()) return codebase[0];
            return codebase[0] * 256 + codebase[1] - 65536; // 双字节码为高位乘256，再加低位 该为无符号码，再减65536
        }

        /// <summary>
        /// 获取 Unicode 码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCodeUnicode(this char value)
        {
            return value;
        }
    }
}
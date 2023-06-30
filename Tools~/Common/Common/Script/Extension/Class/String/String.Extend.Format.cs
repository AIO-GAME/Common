using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class StringExtend
    {
        /// <summary>
        /// 格式化
        /// </summary>
        public static string Format(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public static string Format(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }
    }
}
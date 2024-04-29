using System.Linq;

namespace AIO
{
    internal static class StringExtension
    {
        /// <summary>
        /// 格式化字符串
        /// </summary>
        internal static string Inject(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        internal static string Inject(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }
    }
}
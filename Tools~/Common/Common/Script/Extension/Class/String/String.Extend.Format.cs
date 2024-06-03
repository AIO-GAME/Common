#region

using System.Linq;

#endregion

namespace AIO
{
    public static partial class ExtendString
    {
        /// <summary>
        ///     格式化
        /// </summary>
        public static string Format(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        /// <summary>
        ///     格式化
        /// </summary>
        public static string Format(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }

        /// <summary>
        ///     格式化
        /// </summary>
        public static string Format(this string format, params int[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }

        /// <summary>
        ///     格式化
        /// </summary>
        public static string Format(this string format, int arg1, int arg2, int arg3)
        {
            return string.Format(format, arg1, arg2, arg3);
        }
    }
}
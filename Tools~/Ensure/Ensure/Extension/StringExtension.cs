using System.Linq;

namespace AIO
{
    internal static class StringExtension
    {
        /// <summary>
        /// 将指定数组中对应对象的字符串表示形式替换为指定字符串中的格式项。输入字符串格式使用占位符如{0}、{1}等。
        /// </summary>
        /// <param name="format">输入字符串格式。</param>
        /// <param name="formattingArgs">包含要格式化和注入到字符串中的值的对象数组。</param>
        /// <returns>将格式项替换为相应对象的字符串表示形式后的字符串副本。</returns>
        internal static string Inject(this string format, params object[] formattingArgs)
        {
            return string.Format(format, formattingArgs);
        }

        /// <summary>
        /// 将指定数组中对应对象的字符串表示形式替换为指定字符串中的格式项。输入字符串格式使用占位符如{0}、{1}等。
        /// 此方法是上一个方法的重载，但它接受一个字符串数组而不是对象数组。
        /// </summary>
        /// <param name="format">输入字符串格式。</param>
        /// <param name="formattingArgs">包含要格式化和注入到字符串中的值的字符串数组。</param>
        /// <returns>将格式项替换为相应对象的字符串表示形式后的字符串副本。</returns>
        internal static string Inject(this string format, params string[] formattingArgs)
        {
            return string.Format(format, formattingArgs.Select(a => a as object).ToArray());
        }
    }
}

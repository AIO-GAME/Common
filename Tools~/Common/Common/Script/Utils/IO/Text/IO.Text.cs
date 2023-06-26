/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Runtime.CompilerServices;
using System.Text;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 按照指定编码读取文本文件
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadText(
            in string path,
            in string charset)
        {
            return (Encoding.GetEncoding(charset) ?? Encoding.UTF8).GetString(ReadFile(path));
        }

        /// <summary>
        /// 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="charset">保存文本格式</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WriteText(
            in string path,
            in StringBuilder text,
            in string charset,
            in bool concat = false)
        {
            var e = Encoding.GetEncoding(string.IsNullOrEmpty(charset) ? "utf-8" : charset);
            var b = (e ?? Encoding.UTF8).GetBytes(text.ToString());
            return Write(path, b, 0, b.Length, concat);
        }

        /// <summary>
        /// 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="charset">保存文本格式</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WriteText(
            in string path,
            in string text,
            in string charset,
            in bool concat = false)
        {
            var e = Encoding.GetEncoding(string.IsNullOrEmpty(charset) ? "utf-8" : charset);
            var b = (e ?? Encoding.UTF8).GetBytes(text.ToString());
            return Write(path, b, 0, b.Length, concat);
        }
    }
}

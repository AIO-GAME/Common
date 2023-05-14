using System.Runtime.CompilerServices;
using System.Text;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WriteUTF8(
            in string path,
            in string text,
            in bool concat = false)
        {
            return WriteText(path, text, "utf-8", concat);
        }

        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WriteUTF8(
            in string path,
            in StringBuilder text,
            in bool concat = false)
        {
            return WriteText(path, text.ToString(), "utf-8", concat);
        }

        /// <summary>
        /// 按照UTF-8读取文本文件
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadUTF8(in string path)
        {
            return ReadText(path, "utf-8");
        }
    }
}

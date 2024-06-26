#region

using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

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
                in bool   concat = false)
            {
                return WriteText(path, text, Encoding.UTF8, concat);
            }

            /// <summary>
            /// 将字符串按照UTF-8写入文件,默认覆盖
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="text">内容</param>
            /// <param name="concat">Ture:追加 False:覆盖</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool WriteUTF8(
                in string        path,
                in StringBuilder text,
                in bool          concat = false)
            {
                return WriteText(path, text.ToString(), Encoding.UTF8, concat);
            }

            /// <summary>
            /// 按照UTF-8读取文本文件
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static string ReadUTF8(in string path)
            {
                return ReadText(path, Encoding.UTF8);
            }
        }

        #endregion
    }
}
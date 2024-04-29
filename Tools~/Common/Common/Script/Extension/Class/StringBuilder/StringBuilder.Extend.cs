#region

using System.Runtime.CompilerServices;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    ///     可变字符串扩展
    /// </summary>
    public static class ExtendStringBuilder
    {
        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="sb">可变字符串</param>
        /// <param name="format">格式化</param>
        /// <param name="args">可变参数</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AppendLineFormat(this StringBuilder sb, in string format, params object[] args)
        {
            sb.AppendFormat(format, args);
            sb.AppendLine();
        }
    }
}
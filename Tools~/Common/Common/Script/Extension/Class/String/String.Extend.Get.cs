#region

using System;
using System.Text;

#endregion

namespace AIO
{
    public static partial class ExtendString
    {
        /// <summary>
        ///     获取字节长度
        /// </summary>
        public static int GetBytesLength(this string str)
        {
            return Encoding.BigEndianUnicode.GetByteCount(str);
        }

        /// <summary>
        ///     获取闭合字符段
        /// </summary>
        public static string GetOcclusion(this string str, string label, char runit = '{', char lunit = '}')
        {
            var SIndex = str.LastIndexOf(label, StringComparison.OrdinalIgnoreCase);
            var State = 0; //状态开关 表达闭合
            var Index = 0; //下标
            var Falg = false; //标志开关
            var Passages = str.Substring(SIndex, str.Length - SIndex);
            foreach (var item in Passages)
            {
                if (item == runit)
                {
                    if (Falg == false) Falg = true;
                    State += 1;
                }
                else if (item == lunit)
                {
                    if (Falg) State -= 1;
                }

                Index++;
                if (State == 0 && Falg) break;
            }

            return Passages.Substring(0, Index);
        }

        /// <summary>
        ///     获取闭合字符段
        /// </summary>
        public static string SetOcclusion(this string str, string content, char runit = '{', char lunit = '}')
        {
            var builder = new StringBuilder();
            builder.Append(str);
            builder.Append(' ').Append(runit).AppendLine();
            builder.Append(content).AppendLine();
            builder.Append(lunit);
            return builder.ToString();
        }

        /// <summary>
        /// 获取指定编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>返回指定编码字符串</returns>
        public static string GetEncoding(this string str, string encoding)
        {
            var encoding1 = Encoding.GetEncoding(encoding);
            return encoding1.GetString(encoding1.GetBytes(str));
        }

        /// <summary>
        /// 获取指定编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>返回指定编码字符串</returns>
        public static string GetEncoding(this string str, Encoding encoding)
        {
            return encoding.GetString(encoding.GetBytes(str));
        }
    }
}
/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class StringExtend
    {
        /// <summary>
        /// 富文本 字号
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        public static string RichS(this string content, in int s)
        {
            return string.Concat("<size=", s, ">", content, "</size>");
        }

        /// <summary>
        /// 富文本 颜色
        /// </summary>
        /// <param name="content"></param>
        /// <param name="c">颜色值</param>
        public static string RichC(this string content, in string c)
        {
            return string.Concat("<color=", c, ">", content, "</color>");
        }

        /// <summary>
        /// 富文本 斜体
        /// </summary>
        public static string RichI(this string content)
        {
            return string.Concat("<i>", content, "</i>");
        }

        /// <summary>
        /// 富文本 加粗
        /// </summary>
        public static string RichB(this string content)
        {
            return string.Concat("<b>", content, "</b>");
        }

        /// <summary>
        /// 富文本 加粗 斜体
        /// </summary>
        public static string RichBI(this string content)
        {
            return string.Concat("<i><b>", content, "</b></i>");
        }

        /// <summary>
        /// 富文本 字号 加粗
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        public static string RichSB(this string content, in string s)
        {
            return string.Concat("<b><size=", s, ">", content, "</size></b>");
        }

        /// <summary>
        /// 富文本 字号 斜体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        public static string RichSI(this string content, in int s)
        {
            return string.Concat("<i><size=", s, ">", content, "</size></i>");
        }

        /// <summary>
        /// 富文本 字号 颜色
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichCS(this string content, in int s, in string c)
        {
            return string.Concat("<size=", s, "><color=", c, ">", content, "</color></size>");
        }

        /// <summary>
        /// 富文本 颜色 加粗
        /// </summary>
        /// <param name="content"></param>
        /// <param name="c">颜色值</param>
        public static string RichCB(this string content, in string c)
        {
            return string.Concat("<b><color=", c, ">", content, "</color></b>");
        }

        /// <summary>
        /// 富文本 颜色 斜体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="c">颜色值</param>
        public static string RichCI(this string content, in string c)
        {
            return string.Concat("<i><color=", c, ">", content, "</color></i>");
        }

        /// <summary>
        /// 富文本 字号 加粗 斜体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        public static string RichSBI(this string content, in string s)
        {
            return string.Concat("<i><b><size=", s, ">", content, "</size></b></i>");
        }

        /// <summary>
        /// 富文本 颜色 字号 加粗
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichCSB(this string content, in int s, in string c)
        {
            return string.Concat("<b><size=", s, "><color=", c, ">", content, "</color></size></b>");
        }

        /// <summary>
        /// 富文本 颜色 字号 斜体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichCSI(this string content, in int s, in string c)
        {
            return string.Concat("<i><size=", s, "><color=", c, ">", content, "</color></size></i>");
        }

        /// <summary>
        /// 富文本 颜色 加粗 斜体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="c">颜色值</param>
        public static string RichCBI(this string content, in string c)
        {
            return string.Concat("<i><b><color=", c, ">", content, "</color></b></i>");
        }

        /// <summary>
        /// 富文本 颜色 字号 加粗 斜体
        /// </summary>
        /// <param name="content"></param>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichAll(this string content, in int s, in string c)
        {
            return string.Concat("<i><b><size=", s, "><color=", c, ">", content, "</color></size></b></i>");
        }
    }
}
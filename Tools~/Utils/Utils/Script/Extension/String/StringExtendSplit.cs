/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;
    using System.Text;

    public static partial class StringExtend
    {
        #region Split

        /// <summary>
        /// 分组
        /// </summary>
        public static string[] SplitEx(this string src, char ch)
        {
            if (src == null) return null;
            if (src.Length == 0) return new string[0];
            int i = 0, j = 0, k = 1;
            while ((j = src.IndexOf(ch, i)) >= 0)
            {
                i = j + 1; k++;
            }
            var array = new string[k];
            if (k == 1)
            {
                array[0] = src;
                return array;
            }
            i = j = k = 0;
            while ((j = src.IndexOf(ch, i)) >= 0)
            {
                array[k++] = (i == j ? "" : src.Substring(i, j - i));
                i = j + 1;
            }
            array[k] = (i >= src.Length ? "" : src.Substring(i));
            return array;
        }

        /// <summary>
        /// 分组一次
        /// </summary>
        public static string[] SplitOnce(this string src, char ch)
        {
            if (src == null) return null;
            if (src.Length == 0) return new string[] { "", "" };
            int index = src.IndexOf(ch);
            if (index == -1) return new string[] { src, "" };
            string[] array = new string[2];
            array[0] = (index == 0 ? "" : src.Substring(0, index));
            array[1] = (index == src.Length - 1 ? "" : src.Substring(index + 1));
            return array;
        }

        /// <summary>
        /// 将字符串以行拆分为数组
        /// </summary>
        public static string[] SplitLine(this string str)
        {
            string[] array = SplitEx(str, '\n');
            for (int k = 0; k < array.Length; k++)
            {
                int i = 0; int j = array[k].Length;
                if (j != 0)
                {
                    if (array[k][0] == '\r') { i++; j--; }
                    if (array[k][array[k].Length - 1] == '\r') j--;
                    if (j <= 0) array[k] = "";
                    else if (j < array[k].Length) array[k] = array[k].Substring(i, j);
                }
            }
            return array;
        }

        #endregion

        /// <summary>
        /// 获取字节长度
        /// </summary>
        public static int GetBytesLength(this string str)
        {
            return Encoding.BigEndianUnicode.GetByteCount(str);
        }

        /// <summary>
        /// 获取闭合字符段
        /// </summary>
        public static string GetOcclusion(this string str, string label, char runit = '{', char lunit = '}')
        {
            var SIndex = str.LastIndexOf(label, StringComparison.OrdinalIgnoreCase);
            var State = 0;//状态开关 表达闭合
            var Index = 0;//下标
            var Falg = false;//标志开关
            var Passages = str.Substring(SIndex, str.Length - SIndex);
            foreach (var item in Passages)
            {
                if (item == runit)
                {
                    if (Falg == false) Falg = true;
                    if (Falg) State += 1;
                }
                else if (item == lunit)
                {
                    if (Falg) State -= 1;
                }
                Index++;
                if (State == 0 && Falg == true) break;
            }
            return Passages.Substring(0, Index);
        }

        /// <summary>
        /// 获取闭合字符段
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
    }
}

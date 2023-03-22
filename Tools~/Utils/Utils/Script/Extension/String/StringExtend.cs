/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO
{
    /*
    1)、Length：获得当前字符串中字符的个数
    2)、ToUpper():将字符转换成大写形式
    3)、ToLower():将字符串转换成小写形式
    4)、Equals(lessonTwo,StringComparison.OrdinalIgnoreCase):比较两个字符串，可以忽略大小写
    5)、Split()：分割字符串，返回字符串类型的数组。注：第二个参数为：StringSplitOptions.RemoveEmptyEntries 时表示移除空格。
    6)、Substring()：截取字符串。在截取的时候包含要截取的那个位置。
    7)、IndexOf():判断某个字符串在字符串中第一次出现的位置，如果没有返回-1、值类型和引用类型在内存上存储的地方不一样。
    8)、LastIndexOf()：判断某个字符串在字符串中最后一次出现的位置，如果没有同样返回-1
    9)、StartsWith():判断是否以....开始
    10)、EndsWith():判断是否以...结束.
    11)、Replace():将字符串中某个字符串替换成一个新的字符串
    12)、Contains():判断某个字符串是否包含指定的字符串
    13)、Trim():去掉字符串中前后的空格
    14)、TrimEnd()：去掉字符串中结尾的空格
    15)、TrimStart()：去掉字符串中前面的空格
    16)、string.IsNullOrEmpty():判断一个字符串是否为空或者为null
    17)、string.Join()：将数组按照指定的字符串连接，返回一个字符串。
    */

    /// <summary> 字符工具类 </summary>
    public static partial class StringExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        public static void FastToLower(this string self)
        {
            if (string.IsNullOrEmpty(self)) return;

            unsafe
            {
                fixed (char* pstr = self)
                {
                    for (var i = 0; i < self.Length; ++i)
                    {
                        var c = pstr[i];
                        if (c >= CharUnit.EA && c <= CharUnit.EZ)
                        {
                            pstr[i] = (char)(CharUnit.ea + (c - CharUnit.EA));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        public static void FastToUpper(this string self)
        {
            if (string.IsNullOrEmpty(self)) return;

            unsafe
            {
                fixed (char* pstr = self)
                {
                    for (var i = 0; i < self.Length; ++i)
                    {
                        var c = pstr[i];
                        if (c >= CharUnit.ea && c <= CharUnit.ez)
                        {
                            pstr[i] = (char)(CharUnit.EA + (c - CharUnit.ea));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="idx"></param>
        public static void FastToLower(this string self, in int idx)
        {
            if (self == null || self.Length <= idx || idx < 0) return;
            unsafe
            {
                fixed (char* pstr = self)
                {
                    var c = pstr[idx];
                    if (c >= CharUnit.EA && c <= CharUnit.EZ)
                    {
                        pstr[idx] = (char)(CharUnit.ea + (c - CharUnit.EA));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="idx"></param>
        public static void FastToUpper(this string self, in int idx)
        {
            if (self == null || self.Length <= idx || idx < 0) return;
            unsafe
            {
                fixed (char* pstr = self)
                {
                    var c = pstr[idx];
                    if (c >= CharUnit.ea && c <= CharUnit.ez)
                    {
                        pstr[idx] = (char)(CharUnit.EA + (c - CharUnit.ea));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="repeat"></param>
        /// <returns></returns>
        public static string Repeat(this string s, in int repeat)
        {
            var builder = new StringBuilder(repeat * s.Length);
            for (var i = 0; i < repeat; i++) builder.Append(s);
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parms"></param>
        public static void Add(this IList<string> value, in IEnumerable<string> parms)
        {
            foreach (var item in parms)
                value.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EqualsNull(this string value)
        {
            if (string.IsNullOrEmpty(value) || value == " ")
                return "null";
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool Contains(this string value, char c)
        {
            return value.Any(item => item.Equals(c));
        }

        /// <summary>
        /// 判断字符串数组中 是否有重复
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="list">匹配数组</param>
        /// <returns>Ture:存在 False:不存在</returns>
        public static bool Contains(this string value, in IEnumerable<string> list)
        {
            foreach (var item in list)
                if (value.Contains(item))
                    return true;
            return false;
        }

        /// <summary> 字符串反转 </summary>
        public static string Reverse(this string value)
        {
            var a = value.ToCharArray();
            var l = a.Length;
            for (var i = 0; i < l / 2; i++)
            {
                (a[i], a[l - 1 - i]) = (a[l - 1 - i], a[i]);
            }

            return new string(a);
        }

        #region Clone

        /// <summary>
        /// 重复N此 复制传入数据
        /// </summary>
        public static string Clone(this char s, in int num)
        {
            if (num <= 0) return s.ToString();
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(s);
            return builder.ToString();
        }

        /// <summary>
        /// 重复N此 复制传入数据
        /// </summary>
        public static string Clone(this char s, in uint num)
        {
            if (num <= 0) return s.ToString();
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(s);
            return builder.ToString();
        }

        /// <summary>
        /// 重复N此 复制传入数据
        /// </summary>
        public static string Clone(this string s, in uint num)
        {
            if (num <= 0) return s;
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(s);
            return builder.ToString();
        }

        #endregion

        #region Append

        /// <summary>
        /// 在最前添加指定字符到指定长度
        /// </summary>
        public static string AppendFrontDes(this string s, char c, int len)
        {
            var space = len - s.GetBytesLength() / c.GetBytesLength();
            if (space <= 0) return s;
            return string.Concat(new string(c, space), s);
        }

        /// <summary>
        /// 合并字符 前面
        /// </summary>
        public static string AppendFront(this string s, params string[] c)
        {
            var builder = new StringBuilder();
            foreach (var item in c) builder.Append(item);
            return builder.Append(s).ToString();
        }

        /// <summary>
        /// 在最前添加指定字符
        /// </summary>
        public static string AppendFront(this string s, params char[] c)
        {
            var builder = new StringBuilder();
            foreach (var item in c) builder.Append(item);
            return builder.Append(s).ToString();
        }

        #region AppendLast

        /// <summary>
        /// 在最后添加指定字符到指定字节长度
        /// </summary>
        public static string AppendLastDes(this string s, char c, int len)
        {
            var space = len - s.GetBytesLength() / c.GetBytesLength();
            if (space <= 0) return s;
            return string.Concat(s, new string(c, space));
        }

        /// <summary>
        /// 合并字符 后面
        /// </summary>
        public static string AppendLast(this string s, params string[] c)
        {
            var builder = new StringBuilder().Append(s);
            foreach (var item in c) builder.Append(item);
            return builder.ToString();
        }

        /// <summary>
        /// 在最后添加指定字符到指定长度
        /// </summary>
        public static string AppendLast(this string s, params char[] c)
        {
            var builder = new StringBuilder().Append(s);
            foreach (var item in c) builder.Append(item);
            return builder.ToString();
        }

        #endregion

        #endregion
    }
}
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AIO
{
    public static partial class ExtendString
    {
        /// <summary>
        /// 在最前添加指定字符到指定长度
        /// </summary>
        public static string AppendToFrontChar(this string str, in char paddingChar, in int length)
        {
            if (str.Length >= length) return str;

            var remainingCharsCount = length - str.Length;
            var paddingString = new string(paddingChar, remainingCharsCount);

            return string.Concat(paddingString, str);
        }

        /// <summary>
        /// 合并字符 前面
        /// </summary>
        public static string AppendToFront(this string str, params string[] strings)
        {
            var totalLength = str.Length + strings.Sum(s => s.Length);
            var builder = new StringBuilder(totalLength);
            foreach (var item in strings) builder.Append(item);
            return builder.Append(str).ToString();
        }

        /// <summary>
        /// 在最前添加指定字符
        /// </summary>
        public static string AppendToFront(this string str, params char[] chars)
        {
            var totalLength = str.Length + chars.Length;
            var builder = new StringBuilder(totalLength);
            foreach (var item in chars) builder.Append(item);
            return builder.Append(str).ToString();
        }

        /// <summary>
        /// 在最后添加指定字符到指定字节长度
        /// </summary>
        public static string AppendToLastChar(this string str, in char paddingChar, in int byteLength)
        {
            if (str.GetBytesLength() >= byteLength) return str;

            var remainingCharsCount = (byteLength - str.GetBytesLength()) / paddingChar.GetBytesLength();
            var paddingString = new string(paddingChar, remainingCharsCount);

            return string.Concat(str, paddingString);
        }

        /// <summary>
        /// 合并字符 后面
        /// </summary>
        public static string AppendToLast(this string str, params string[] strings)
        {
            var totalLength = str.Length + strings.Sum(s => s.Length);
            var builder = new StringBuilder(totalLength).Append(str);
            foreach (var item in strings) builder.Append(item);
            return builder.ToString();
        }

        /// <summary>
        /// 在最后添加指定字符到指定长度
        /// </summary>
        public static string AppendToLast(this string str, params char[] chars)
        {
            var totalLength = str.Length + chars.Length;
            var builder = new StringBuilder(totalLength).Append(str);
            foreach (var item in chars) builder.Append(item);
            return builder.ToString();
        }
    }
}
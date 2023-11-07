using System.Text;

namespace AIO
{
    public partial class ExtendString
    {
        /// <summary>
        /// 从字符串中过滤出指定类型的字符，并返回过滤后的字符串
        /// </summary>
        /// <param name="s">要过滤的字符串</param>
        /// <param name="letters">是否保留字母</param>
        /// <param name="numbers">是否保留数字</param>
        /// <param name="whitespace">是否保留空格</param>
        /// <param name="symbols">是否保留符号</param>
        /// <param name="punctuation">是否保留标点符号</param>
        /// <returns>过滤后的字符串</returns>
        public static string Filter(this string s,
            in bool letters = true,
            in bool numbers = true,
            in bool whitespace = true,
            in bool symbols = true,
            in bool punctuation = true)
        {
            var sb = new StringBuilder();

            foreach (var c in s)
            {
                if ((!letters && char.IsLetter(c)) ||
                    (!numbers && char.IsNumber(c)) ||
                    (!whitespace && char.IsWhiteSpace(c)) ||
                    (!symbols && char.IsSymbol(c)) ||
                    (!punctuation && char.IsPunctuation(c))
                   )
                    continue;

                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 从字符串中过滤出指定类型的字符，并用指定字符替换被过滤的字符
        /// </summary>
        /// <param name="s">要过滤的字符串</param>
        /// <param name="replacement">用于替换被过滤字符的字符</param>
        /// <param name="merge">是否将连续被过滤的字符合并为单个替换字符</param>
        /// <param name="letters">是否保留字母</param>
        /// <param name="numbers">是否保留数字</param>
        /// <param name="whitespace">是否保留空格</param>
        /// <param name="symbols">是否保留符号</param>
        /// <param name="punctuation">是否保留标点符号</param>
        /// <returns>替换后的字符串</returns>
        public static string FilterReplace(this string s,
            in char replacement,
            in bool merge,
            in bool letters = true,
            in bool numbers = true,
            in bool whitespace = true,
            in bool symbols = true,
            in bool punctuation = true)
        {
            var sb = new StringBuilder();

            var wasFiltered = false;

            foreach (var c in s)
            {
                if ((!letters && char.IsLetter(c)) ||
                    (!numbers && char.IsNumber(c)) ||
                    (!whitespace && char.IsWhiteSpace(c)) ||
                    (!symbols && char.IsSymbol(c)) ||
                    (!punctuation && char.IsPunctuation(c)))
                {
                    if (!merge || !wasFiltered)
                    {
                        sb.Append(replacement);
                    }

                    wasFiltered = true;
                }
                else
                {
                    sb.Append(c);

                    wasFiltered = false;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 美化
        /// </summary>
        public static string Prettify(this string s)
        {
            return s.FirstCharacterToUpper().SplitWords(' ');
        }

        /// <summary>
        /// 首字母转为大写
        /// </summary>
        public static string FirstCharacterToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s) || char.IsUpper(s, 0)) return s;
            return string.Concat(char.ToUpperInvariant(s[0]), s.Substring(1));
        }
    }
}
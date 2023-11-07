using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class ExtendChar
    {
        /// <summary>
        /// 检查字符是否是单词分隔符，包括空格、符号和标点符号
        /// </summary>
        /// <param name="c">要检查的字符</param>
        /// <returns>如果字符是单词分隔符，则为 true；否则为 false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWordDelimiter(this char c)
        {
            return char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c);
        }

        /// <summary>
        /// 判断字符串在指定范围
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRange(this char c, in char start, in char end)
        {
            return start <= c && c <= end;
        }

        /// <summary>
        /// 判断字符串在指定范围
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRange(this char c, in int start, in int end)
        {
            return start <= c && c <= end;
        }

        /// <summary>
        /// 检查字符是否可能是单词开始，根据字符及其前后字符的类型和顺序推断
        /// </summary>
        /// <param name="c">前一个字符，如果不存在则为 null</param>
        /// <param name="current">当前字符</param>
        /// <param name="next">后一个字符，如果不存在则为 null</param>
        /// <returns>如果该字符可能是单词开始，则为 true；否则为 false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWordBeginning(this char? c, in char current, in char? next)
        {
            var isFirst = c == null;
            var isLast = next == null;

            var isLetter = char.IsLetter(current);
            var wasLetter = c != null && char.IsLetter(c.Value);

            var isNumber = char.IsNumber(current);
            var wasNumber = c != null && char.IsNumber(c.Value);

            var isUpper = char.IsUpper(current);
            var wasUpper = c != null && char.IsUpper(c.Value);

            var isDelimiter = IsWordDelimiter(current);
            var wasDelimiter = c != null && IsWordDelimiter(c.Value);

            var willBeLower = next != null && char.IsLower(next.Value);

            return
                (!isDelimiter && isFirst) ||
                (!isDelimiter && wasDelimiter) ||
                (isLetter && wasLetter && isUpper && !wasUpper) || // camelCase => camel_Case
                (isLetter && wasLetter && isUpper && wasUpper && !isLast && willBeLower) || // => ABBRWord => ABBR_Word
                (isNumber && wasLetter) || // Vector3 => Vector_3
                (isLetter && wasNumber && isUpper && willBeLower); // Word1Word => Word_1_Word, Word1word => Word_1word
        }
    }
}
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AIO
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public partial class EnsureThat
    {
        /// <summary>
        /// 参数名
        /// </summary>
        internal string paramName;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Prettify(in string s)
        {
            return SplitWords(FirstCharacterToUpper(s), ' ');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string SplitWords(in string s, in char separator)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (i > 0 && IsWordBeginning(s, i))
                {
                    sb.Append(separator);
                }

                sb.Append(c);
            }

            return sb.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsWordBeginning(in string s, in int index)
        {
            Ensure.That(nameof(index)).IsGte(index, 0);
            Ensure.That(nameof(index)).IsLt(index, s.Length);

            var previous = index > 0 ? s[index - 1] : (char?)null;
            var current = s[index];
            var next = index < s.Length - 1 ? s[index + 1] : (char?)null;

            return IsWordBeginning(previous, current, next);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsWordBeginning(in char? previous, in char current, in char? next)
        {
            var isFirst = previous == null;
            var isLast = next == null;

            var isLetter = char.IsLetter(current);
            var wasLetter = previous != null && char.IsLetter(previous.Value);

            var isNumber = char.IsNumber(current);
            var wasNumber = previous != null && char.IsNumber(previous.Value);

            var isUpper = char.IsUpper(current);
            var wasUpper = previous != null && char.IsUpper(previous.Value);

            var isDelimiter = IsWordDelimiter(current);
            var wasDelimiter = previous != null && IsWordDelimiter(previous.Value);

            var willBeLower = next != null && char.IsLower(next.Value);

            return
                (!isDelimiter && isFirst) ||
                (!isDelimiter && wasDelimiter) ||
                (isLetter && wasLetter && isUpper && !wasUpper) || // camelCase => camel_Case
                (isLetter && wasLetter && isUpper && wasUpper && !isLast && willBeLower) || // => ABBRWord => ABBR_Word
                (isNumber && wasLetter) || // Vector3 => Vector_3
                (isLetter && wasNumber && isUpper && willBeLower); // Word1Word => Word_1_Word, Word1word => Word_1word
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsWordDelimiter(in char c)
        {
            return char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FirstCharacterToUpper(in string s)
        {
            if (string.IsNullOrEmpty(s) || char.IsUpper(s, 0))
            {
                return s;
            }

            return char.ToUpperInvariant(s[0]) + s.Substring(1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsNullOrWhiteSpace(in string s)
        {
            return s == null || s.Trim() == string.Empty;
        }

        private static readonly Regex guidRegex = new Regex(@"[a-fA-F0-9]{8}(\-[a-fA-F0-9]{4}){3}\-[a-fA-F0-9]{12}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool StringEquals(in string x, in string y, in StringComparison? comparison = null)
        {
            return comparison.HasValue ? string.Equals(x, y, comparison.Value) : string.Equals(x, y);
        }
    }
}
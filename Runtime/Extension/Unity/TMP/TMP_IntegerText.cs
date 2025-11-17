/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#if SUPPORT_TEXTMESHPRO
using TMPro;
using UnityEngine;

namespace AIO.UEngine
{
    //#define TMP_ROUND_DECIMALS // When defined, float and double values are rounded using Math.round
    [HelpURL("https://gist.github.com/yasirkula/31704321d6eef8df19933fe502ae6219")]
    public static class TMP_IntegerText
    {
        private static readonly char[] arr       = new char[64]; // prefix + number + postfix can't exceed this capacity!
        private static          int    charIndex = 0;

        /// <summary>
        /// 设置文本为指定的数字
        /// </summary>
        /// <param name="text"> 文本组件 </param>
        /// <param name="number"> 数字 </param>
        public static void SetText(this TMP_Text text, sbyte number) { SetText(text, (int)number); }

        public static void SetText(this TMP_Text text, sbyte number, string prefix, string postfix) { SetText(text, (int)number, prefix, postfix); }

        public static void SetText(this TMP_Text text, byte number) { SetText(text, (uint)number); }

        public static void SetText(this TMP_Text text, byte number, string prefix, string postfix) { SetText(text, (uint)number, prefix, postfix); }

        public static void SetText(this TMP_Text text, short number) { SetText(text, (int)number); }

        public static void SetText(this TMP_Text text, short number, string prefix, string postfix) { SetText(text, (int)number, prefix, postfix); }

        public static void SetText(this TMP_Text text, ushort number) { SetText(text, (uint)number); }

        public static void SetText(this TMP_Text text, ushort number, string prefix, string postfix) { SetText(text, (uint)number, prefix, postfix); }

        public static void SetText(this TMP_Text text, int number)
        {
            SetInteger(number);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, int number, string prefix, string postfix)
        {
            AddStringToArray(postfix);
            SetInteger(number);
            AddStringToArray(prefix);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, uint number)
        {
            SetInteger(number);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, uint number, string prefix, string postfix)
        {
            AddStringToArray(postfix);
            SetInteger(number);
            AddStringToArray(prefix);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, long number)
        {
            SetInteger(number);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, long number, string prefix, string postfix)
        {
            AddStringToArray(postfix);
            SetInteger(number);
            AddStringToArray(prefix);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, ulong number)
        {
            SetInteger(number);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, ulong number, string prefix, string postfix)
        {
            AddStringToArray(postfix);
            SetInteger(number);
            AddStringToArray(prefix);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, float number, int precision = 2)
        {
            SetFloat(number, precision);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, float number, string prefix, string postfix, int precision = 2)
        {
            AddStringToArray(postfix);
            SetFloat(number, precision);
            AddStringToArray(prefix);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, double number, int precision = 2)
        {
            SetFloat(number, precision);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        public static void SetText(this TMP_Text text, double number, string prefix, string postfix, int precision = 2)
        {
            AddStringToArray(postfix);
            SetFloat(number, precision);
            AddStringToArray(prefix);

            ReverseArray();
            text.SetCharArray(arr, 0, charIndex);
            charIndex = 0;
        }

        private static void SetInteger(int number)
        {
            if (number == 0)
                arr[charIndex++] = '0';
            else
            {
                bool isNegativeNumber = number < 0;
                if (isNegativeNumber)
                    number = -number;

                while (number > 0)
                {
                    arr[charIndex++] =  (char)('0' + (number % 10));
                    number           /= 10;
                }

                if (isNegativeNumber)
                    arr[charIndex++] = '-';
            }
        }

        private static void SetInteger(uint number)
        {
            if (number == 0)
                arr[charIndex++] = '0';
            else
            {
                while (number > 0)
                {
                    arr[charIndex++] =  (char)('0' + (number % 10));
                    number           /= 10;
                }
            }
        }

        private static void SetInteger(long number)
        {
            if (number == 0)
                arr[charIndex++] = '0';
            else
            {
                bool isNegativeNumber = number < 0;
                if (isNegativeNumber)
                    number = -number;

                while (number > 0)
                {
                    arr[charIndex++] =  (char)('0' + (number % 10));
                    number           /= 10;
                }

                if (isNegativeNumber)
                    arr[charIndex++] = '-';
            }
        }

        private static void SetInteger(ulong number)
        {
            if (number == 0)
                arr[charIndex++] = '0';
            else
            {
                while (number > 0)
                {
                    arr[charIndex++] =  (char)('0' + (number % 10));
                    number           /= 10;
                }
            }
        }

        private static void SetFloat(float number, int precision)
        {
            bool isNegativeNumber = number < 0f;
            if (isNegativeNumber)
                number = -number;

            uint  integer  = (uint)number;
            float fraction = number - integer;

#if TMP_ROUND_DECIMALS
		    fraction = (float) System.Math.Round( fraction, precision );
#endif

            charIndex += precision;
            int truncateAmount = 0;
            for (int i = 0; i < precision; i++)
            {
                // Floating point arithmetics are not deterministic, try to handle
                // edge cases properly via these if conditions
                if (fraction >= 0f)
                    fraction *= 10f;
                else
                    fraction *= -10f;

                int digit = (int)fraction;
                if (digit <= 0)
                {
                    digit = 0;
                    truncateAmount++;
                }
                else
                {
                    if (digit > 9)
                        digit = 9;

                    truncateAmount = 0;
                }

                arr[--charIndex] =  (char)('0' + digit);
                fraction         -= digit;
            }

            // Assert: if truncateAmount == precision, then fraction consists of all 0's,
            // so don't include the fraction in the text at all
            if (truncateAmount < precision)
            {
                if (truncateAmount > 0)
                {
                    // Truncate redundant 0's
                    for (int i = charIndex + truncateAmount, j = i + precision; i < j; i++)
                        arr[i - truncateAmount] = arr[i];
                }

                charIndex        += precision - truncateAmount;
                arr[charIndex++] =  '.';
            }
            else if (integer == 0) // Rare case: -0.0001 with precision 2 results in -0, this line fixes it
                isNegativeNumber = false;

            SetInteger(integer);

            if (isNegativeNumber)
                arr[charIndex++] = '-';
        }

        private static void SetFloat(double number, int precision)
        {
            bool isNegativeNumber = number < 0f;
            if (isNegativeNumber)
                number = -number;

            ulong  integer  = (ulong)number;
            double fraction = number - integer;

#if TMP_ROUND_DECIMALS
		    fraction = System.Math.Round( fraction, precision );
#endif

            charIndex += precision;
            int truncateAmount = 0;
            for (int i = 0; i < precision; i++)
            {
                // Floating point arithmetics are not deterministic, try to handle
                // edge cases properly via these if conditions
                if (fraction >= 0.0)
                    fraction *= 10.0;
                else
                    fraction *= -10.0;

                int digit = (int)fraction;
                if (digit <= 0)
                {
                    digit = 0;
                    truncateAmount++;
                }
                else
                {
                    if (digit > 9)
                        digit = 9;

                    truncateAmount = 0;
                }

                arr[--charIndex] =  (char)('0' + digit);
                fraction         -= digit;
            }

            // Assert: if truncateAmount == precision, then fraction consists of all 0's,
            // so don't include the fraction in the text at all
            if (truncateAmount < precision)
            {
                if (truncateAmount > 0)
                {
                    // Truncate redundant 0's
                    for (int i = charIndex + truncateAmount, j = i + precision; i < j; i++)
                        arr[i - truncateAmount] = arr[i];
                }

                charIndex        += precision - truncateAmount;
                arr[charIndex++] =  '.';
            }
            else if (integer == 0) // Rare case: -0.0001 with precision 2 results in -0, this line fixes it
                isNegativeNumber = false;

            SetInteger(integer);

            if (isNegativeNumber)
                arr[charIndex++] = '-';
        }

        private static void AddStringToArray(string str)
        {
            if (string.IsNullOrEmpty(str)) return;
            for (int i = str.Length - 1; i >= 0; i--)
                arr[charIndex++] = str[i];
        }

        private static void ReverseArray()
        {
            for (int i = charIndex / 2 - 1; i >= 0; i--)
            {
                int j = charIndex - 1 - i;

                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
    }
}
#endif
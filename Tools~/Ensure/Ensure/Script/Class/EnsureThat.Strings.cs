using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [value is null]
        /// [value == null ]
        /// [value.Trim() == string.Empty]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotNullOrWhiteSpace(in string value)
        {
            if (Ensure.IsActive == false) return;

            IsNotNull(value);

            if (value == null || value.Trim() == string.Empty)
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsNotNullOrWhiteSpace_Failed, paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value is null]
        /// [value == ""]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotNullOrEmpty(in string value)
        {
            if (Ensure.IsActive == false) return;

            IsNotNull(value);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsNotNullOrEmpty_Failed, paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value is null]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotNull(in string value)
        {
            if (Ensure.IsActive == false) return;

            if (value is null)
            {
                throw new ArgumentNullException(paramName, ExceptionMessages.Common_IsNotNull_Failed);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [string.Empty == value]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotEmpty(in string value)
        {
            if (Ensure.IsActive == false) return;

            if (string.Empty.Equals(value))
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsNotEmpty_Failed, paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value is null]
        /// [value.Length &lt; minLength]
        /// [value.Length > maxLength]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HasLengthBetween(in string value, in int minLength, in int maxLength)
        {
            if (Ensure.IsActive == false) return;

            IsNotNull(value);

            var length = value.Length;
            if (length < minLength)
            {
                throw new ArgumentException(ExceptionMessages.Strings_HasLengthBetween_Failed_ToShort.Inject(minLength, maxLength, length), paramName);
            }

            if (length > maxLength)
            {
                throw new ArgumentException(ExceptionMessages.Strings_HasLengthBetween_Failed_ToLong.Inject(minLength, maxLength, length), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [match.IsMatch(value) == false]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Matches(in string value, in string match)
        {
            Matches(value, new Regex(match));
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [match.IsMatch(value) == false]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Matches(in string value, in Regex match)
        {
            if (Ensure.IsActive == false) return;

            if (!match.IsMatch(value))
            {
                throw new ArgumentException(ExceptionMessages.Strings_Matches_Failed.Inject(value, match), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value is null]
        /// [value.Length != expected]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SizeIs(in string value, in int expected)
        {
            if (Ensure.IsActive == false) return;

            IsNotNull(value);

            if (value.Length != expected)
            {
                throw new ArgumentException(ExceptionMessages.Strings_SizeIs_Failed.Inject(expected, value.Length), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [string.Equals(value, expected) == false]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsEqualTo(in string value, in string expected)
        {
            if (Ensure.IsActive == false) return;

            if (string.Equals(value, expected) == false)
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsEqualTo_Failed.Inject(value, expected), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [string.Equals(value, expected, comparison) == false]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsEqualTo(in string value, in string expected, in StringComparison comparison)
        {
            if (Ensure.IsActive == false) return;

            if (string.Equals(value, expected, comparison) == false)
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsEqualTo_Failed.Inject(value, expected), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [string.Equals(value, expected) == true]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotEqualTo(in string value, in string expected)
        {
            if (Ensure.IsActive == false) return;

            if (string.Equals(value, expected))
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsNotEqualTo_Failed.Inject(value, expected), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [string.Equals(value, expected, comparison) == true]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsNotEqualTo(in string value, in string expected, in StringComparison comparison)
        {
            if (Ensure.IsActive == false) return;

            if (string.Equals(value, expected, comparison))
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsNotEqualTo_Failed.Inject(value, expected), paramName);
            }
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [Regex(@"[a-fA-F0-9]{8}(\-[a-fA-F0-9]{4}){3}\-[a-fA-F0-9]{12}").IsMatch(value) == false]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsGuid(in string value)
        {
            if (Ensure.IsActive == false) return;

            if (guidRegex.IsMatch(value) == false)
            {
                throw new ArgumentException(ExceptionMessages.Strings_IsGuid_Failed.Inject(value), paramName);
            }
        }
    }
}
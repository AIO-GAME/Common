using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class EnsureThat
    {
        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SizeIs<TKey, TValue>(in IDictionary<TKey, TValue> value, in int expected)
        {
            if (!Ensure.IsActive) return;

            if (value.Count != expected)
                throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.Count != expected]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SizeIs<TKey, TValue>(in IDictionary<TKey, TValue> value, in long expected)
        {
            if (!Ensure.IsActive) return;

            if (value.Count != expected)
                throw new ArgumentException(ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value.ContainsKey(expectedKey) == false]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IsKeyOf<TKey, TValue>(in IDictionary<TKey, TValue> value, in TKey expectedKey, in string keyLabel = null)
        {
            if (!Ensure.IsActive) return;

            if (!value.ContainsKey(expectedKey))
                throw new ArgumentException(ExceptionMessages.Collections_ContainsKey_Failed.Inject(expectedKey, keyLabel ?? Prettify(paramName)), paramName);
        }

        /// <summary>
        /// 验证数据 报错条件
        /// [value == null]
        /// [value.Count &lt; 1]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HasItems<TKey, TValue>(in IDictionary<TKey, TValue> value)
        {
            if (!Ensure.IsActive) return;

            IsNotNull(value);

            if (value.Count < 1)
                throw new ArgumentException(ExceptionMessages.Collections_HasItemsFailed, paramName);
        }
    }
}
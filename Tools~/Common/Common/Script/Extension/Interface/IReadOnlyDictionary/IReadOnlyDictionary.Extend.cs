using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// IReadOnlyDictionary 扩展
    /// </summary>
    public static partial class ExtendIReadOnlyDictionary
    {
        /// <summary>
        /// 获取值或默认值
        /// </summary>
        /// <param name="dictionary"> 字典 </param>
        /// <param name="key"> 键 </param>
        /// <typeparam name="TKey"> 键类型 </typeparam>
        /// <typeparam name="TValue"> 值类型 </typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }

        /// <summary>
        /// 获取值或默认值
        /// </summary>
        /// <param name="dictionary"> 字典 </param>
        /// <param name="key"> 键 </param>
        /// <param name="defaultValue"> 默认值 </param>
        /// <typeparam name="TKey"> 键类型 </typeparam>
        /// <typeparam name="TValue"> 值类型 </typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 泛型类 FlexibleDictionary&lt;TKey, TValue&gt;，继承了 Dictionary&lt;TKey, TValue&gt; 类，并在索引器中实现了更灵活的赋值操作。
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class FlexibleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <inheritdoc cref="Dictionary&lt;TKey, TValue&gt;.this[TKey]" />
        public new TValue this[TKey key]
        {
            get => base[key];
            set
            {
                if (ContainsKey(key))
                    base[key] = value;
                else Add(key, value);
            }
        }
    }
}
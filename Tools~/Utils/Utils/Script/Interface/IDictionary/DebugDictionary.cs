using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    public partial class DebugDictionary<TKey, TValue> : IDictionary
    {
        /// <inheritdoc cref="IDictionary.this[object]" />
        public object this[object key]
        {
            get => ((IDictionary)this)[(TKey)key];
            set => ((IDictionary)this)[(TKey)key] = (TValue)value;
        }

        /// <inheritdoc cref="IDictionary.IsReadOnly" />
        public bool IsReadOnly => ((IDictionary)dictionary).IsReadOnly;

        /// <inheritdoc cref="IDictionary.IsFixedSize" />
        public bool IsFixedSize => ((IDictionary)dictionary).IsFixedSize;

        /// <inheritdoc cref="ICollection.IsSynchronized" />
        public bool IsSynchronized => ((ICollection)dictionary).IsSynchronized;

        /// <inheritdoc cref="IDictionary.Contains(object)" />
        public bool Contains(object key) => ContainsKey((TKey)key);

        /// <inheritdoc cref="ICollection.SyncRoot" />
        public object SyncRoot => ((ICollection)dictionary).SyncRoot;

        /// <inheritdoc cref="IDictionary.Values" />
        ICollection IDictionary.Values => ((IDictionary)dictionary).Values;

        /// <inheritdoc cref="IDictionary.Keys" />
        ICollection IDictionary.Keys => ((IDictionary)dictionary).Keys;

        /// <inheritdoc cref="IDictionary.GetEnumerator()" />
        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)dictionary).GetEnumerator();

        /// <inheritdoc cref="IDictionary.Remove(object)" />
        public void Remove(object key) => Remove((TKey)key);

        /// <inheritdoc cref="IDictionary.Add(object, object)" />
        public void Add(object key, object value) => Add((TKey)key, (TValue)value);

        /// <inheritdoc cref="ICollection.CopyTo(Array, int)" />
        public void CopyTo(Array array, int index) => ((ICollection)dictionary).CopyTo(array, index);
    }

    /// <summary>
    /// 泛型类 DebugDictionary&lt;TKey, TValue&gt;，实现了 IDictionary&lt;TKey, TValue&gt; 接口。
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public partial class DebugDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        // 字典对象，用于存储键值对。
        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        /// <inheritdoc />
        public TValue this[TKey key]
        {
            get => dictionary[key];
            set
            {
                Debug($"Set: {key} => {value}");
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// 标题，用于在调试信息中区分不同的字典对象。
        /// </summary>
        public string label { get; set; } = "Dictionary";

        /// <summary>
        /// 是否输出调试信息的开关。
        /// </summary>
        public bool debug { get; set; } = false;

        /// <inheritdoc cref="IDictionary"/> />
        public int Count => dictionary.Count;

        /// <inheritdoc />
        public ICollection<TKey> Keys => dictionary.Keys;

        /// <inheritdoc />
        public ICollection<TValue> Values => dictionary.Values;

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();

        /// <summary>
        /// 从字典中移除所有的键值对。
        /// </summary>
        public void Clear()
        {
            Debug("Clear");
            dictionary.Clear();
        }

        /// <summary>
        /// 向字典中添加一个键值对。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(TKey key, TValue value)
        {
            Debug($"Add: {key} => {value}");
            dictionary.Add(key, value);
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<TKey, TValue> item) => dictionary.Contains(item);

        /// <inheritdoc />
        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        /// <inheritdoc />
        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        /// <inheritdoc />
        public bool Remove(TKey key)
        {
            Debug($"Remove: {key}");
            return dictionary.Remove(key);
        }

        /// <inheritdoc />
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).IsReadOnly;

        /// <inheritdoc />
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).Add(item);


        /// <inheritdoc />
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).CopyTo(array, arrayIndex);

        /// <inheritdoc />
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).Remove(item);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)dictionary).GetEnumerator();

        /// <summary>
        /// 输出调试信息的私有方法，根据 debug 和 label 属性判断是否输出。
        /// </summary>
        /// <param name="message">要输出的信息</param>
        private void Debug(string message)
        {
            if (!debug) return;
            if (!string.IsNullOrEmpty(label)) message = $"[{label}] {message}";
            Console.WriteLine(message + "\n");
        }
    }
}
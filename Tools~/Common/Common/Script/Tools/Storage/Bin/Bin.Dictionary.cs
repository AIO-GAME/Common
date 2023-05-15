using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 数据字典
    /// </summary>
    /// <typeparam name="K">Key泛型</typeparam>
    /// <typeparam name="V">Value泛型</typeparam>
    public class BinDictionary<K, V> :
        IBinData,
        IDictionary<K, V>
        where K : IBinData, new()
        where V : IBinData, new()
    {
        /// <summary>
        /// 集合
        /// </summary>
        protected Dictionary<K, V> Collection { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BinDictionary()
        {
            Collection = Pool.Dictionary<K, V>.New();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Collection.Free();
        }

        /// <inheritdoc />
        public void Deserialize(IReadData buffer)
        {
            if (buffer.Count == 0) return;
            var len = buffer.ReadLen();
            for (var i = 0; i < len; i++)
                Collection.Add(buffer.ReadData<K>(), buffer.ReadData<V>());
        }

        /// <inheritdoc />
        public void Serialize(IWriteData buffer)
        {
            buffer.WriteLen(Collection.Count);
            foreach (var item in Collection)
            {
                buffer.WriteData(item.Key);
                buffer.WriteData(item.Value);
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
            foreach (var item in Collection)
            {
                item.Value.Reset();
            }
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<K, V>>)Collection).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<K, V> item)
        {
            if (!Collection.ContainsKey(item.Key)) Collection.Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Collection.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<K, V> item)
        {
            return Collection.ContainsKey(item.Key);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "The value of arrayIndex is out of range.");
            }

            foreach (var item in Collection)
            {
                if (arrayIndex >= array.Length)
                {
                    throw new ArgumentException("The length of array is less than the number of elements in the collection.");
                }

                array[arrayIndex++] = item;
            }
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<K, V> item)
        {
            return Collection.Remove(item.Key);
        }

        /// <inheritdoc />
        public int Count => Collection.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public bool ContainsKey(K key)
        {
            return Collection.ContainsKey(key);
        }

        /// <inheritdoc />
        public void Add(K key, V value)
        {
            if (!Collection.ContainsKey(key)) Collection.Add(key, value);
        }

        /// <inheritdoc />
        public bool Remove(K key)
        {
            return Collection.Remove(key);
        }

        /// <inheritdoc />
        public bool TryGetValue(K key, out V value)
        {
            return Collection.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public V this[K key]
        {
            get => Collection[key];
            set => Collection[key] = value;
        }

        /// <inheritdoc />
        public ICollection<K> Keys => Collection.Keys;

        /// <inheritdoc />
        public ICollection<V> Values => Collection.Values;

        /// <inheritdoc />
        public virtual object Clone()
        {
            var data = new BinDictionary<K, V>();
            foreach (var item in Collection)
            {
                data.Collection.Add((K)item.Key.Clone(), (V)item.Value.Clone());
            }

            return data;
        }
    }
}
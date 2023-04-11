using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 数据字典
    /// </summary>
    /// <typeparam name="TKey">Key泛型</typeparam>
    /// <typeparam name="TValue">Value泛型</typeparam>
    public class BinSortedList<TKey, TValue> :
        IBinData,
        IDictionary<TKey, TValue>,
        ICollection,
        IReadOnlyDictionary<TKey, TValue>
        where TKey : IBinData, new()
        where TValue : IBinData, new()
    {
        /// <summary>
        /// 集合
        /// </summary>
        protected SortedList<TKey, TValue> Collection { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BinSortedList()
        {
            Collection = Pool.SortedList<TKey, TValue>.New();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Collection.Free();
        }

        /// <inheritdoc />
        public void Deserialize(IReadIData buffer)
        {
            if (buffer.Count == 0) return;
            var len = buffer.ReadLen();
            for (var i = 0; i < len; i++)
                Collection.Add(buffer.ReadData<TKey>(), buffer.ReadData<TValue>());
        }

        /// <inheritdoc />
        public void Serialize(IWriteIData buffer)
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
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)Collection).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (!Collection.ContainsKey(item.Key)) Collection.Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Collection.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Collection.ContainsKey(item.Key);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
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
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Collection.Remove(item.Key);
        }

        /// <inheritdoc />
        public void CopyTo(Array array, int index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The value of arrayIndex is out of range.");
            }

            if (index + Collection.Count >= array.Length)
            {
                throw new ArgumentException("The length of array is less than the number of elements in the collection.");
            }

            Array.Copy(Collection.ToArray(), 0, array, index, Collection.Count);
        }


        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => Collection.Count;

        /// <inheritdoc />
        public object SyncRoot => Collection;

        /// <inheritdoc />
        public bool IsSynchronized => false;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc cref="IDictionary{TKey,TValue}.ContainsKey" />
        public bool ContainsKey(TKey key)
        {
            return Collection.ContainsKey(key);
        }

        /// <inheritdoc />
        public void Add(TKey key, TValue value)
        {
            if (!Collection.ContainsKey(key)) Collection.Add(key, value);
        }

        /// <inheritdoc />
        public bool Remove(TKey key)
        {
            return Collection.Remove(key);
        }

        /// <inheritdoc cref="IDictionary{TKey,TValue}.TryGetValue" />
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Collection.TryGetValue(key, out value);
        }

        /// <inheritdoc cref="IDictionary{TKey,TValue}.this" />
        public TValue this[TKey key]
        {
            get => Collection[key];
            set => Collection[key] = value;
        }

        /// <inheritdoc />
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

        /// <inheritdoc />
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

        /// <inheritdoc />
        public ICollection<TKey> Keys => Collection.Keys;

        /// <inheritdoc />
        public ICollection<TValue> Values => Collection.Values;
    }
}
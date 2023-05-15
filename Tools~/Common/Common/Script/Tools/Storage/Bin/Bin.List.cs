using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 列表存储
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    public class BinList<T> :
        IBinData,
        IList<T>,
        ICollection,
        IReadOnlyList<T>
        where T : IBinData, new()
    {
        /// <summary>
        /// 集合
        /// </summary>
        protected List<T> Collection { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        public BinList()
        {
            Collection = Pool.List<T>.New();
        }


        /// <inheritdoc />
        public void Dispose()
        {
            Collection.Free();
        }

        /// <inheritdoc />
        public void Reset()
        {
            foreach (var item in Collection)
            {
                item.Reset();
            }
        }

        /// <inheritdoc />
        public void Deserialize(IReadData buffer)
        {
            if (buffer.Count == 0) return;
            buffer.ReadDataArray(Collection);
        }

        /// <inheritdoc />
        public void Serialize(IWriteData buffer)
        {
            buffer.WriteDataArray(Collection);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Collection).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public void Add(T item)
        {
            Collection.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Collection.Clear();
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            return Collection.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
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
        public bool Remove(T item)
        {
            return Collection.Remove(item);
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

        /// <summary>
        ///   获取 <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。
        /// </returns>
        public int Count => Collection.Count;

        /// <inheritdoc />
        public object SyncRoot => Collection;

        /// <inheritdoc />
        public bool IsSynchronized => false;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public int IndexOf(T item)
        {
            return Collection.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, T item)
        {
            Collection.Insert(index, item);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        /// <inheritdoc cref="IList{T}.this" />
        public T this[int index]
        {
            get => Collection[index];
            set => Collection[index] = value;
        }


        /// <inheritdoc />
        public virtual object Clone()
        {
            var data = new BinList<T>();
            foreach (var item in Collection)
            {
                data.Collection.Add((T)item.Clone());
            }

            return data;
        }
    }
}
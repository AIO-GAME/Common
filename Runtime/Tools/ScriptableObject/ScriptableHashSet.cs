using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using AIO;
using APool = Pool;

namespace AIO
{
    /// <summary>
    /// 可持久化 列表数据
    /// </summary>
    /// <typeparam name="V">Value类型</typeparam>
    [DisplayName("可持久化 列表数据"), Description("处理数据主要数据为列表结构的数据文件")]
    public class ScriptableHashSet<V> : ScriptableData, ICollection<V>
    {
        /// <summary>
        /// 集合
        /// </summary>
        [NonSerialized] protected HashSet<V> Collection;

        /// <summary>
        /// 数量
        /// </summary>
        public int Count => Collection.Count;

        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// 添加
        /// </summary>
        public void Add(V item)
        {
            Collection.Add(item);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            Collection.Clear();
        }

        /// <summary>
        /// 存在
        /// </summary>
        public bool Contains(V item)
        {
            return Collection.Contains(item);
        }

        /// <summary>
        /// 复制
        /// </summary>
        public void CopyTo(V[] array, int arrayIndex)
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

        /// <summary>
        /// 集合迭代器
        /// </summary>
        public IEnumerator<V> GetEnumerator()
        {
            foreach (var item in Collection)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        public bool Remove(V item)
        {
            return Collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        protected sealed override void OnDeserialize()
        {
            Collection = APool.HashSet<V>();
            if (Data == null || Data.Length == 0) return;
            ToDeserialize(new BufferByte(Data));
        }

        /// <inheritdoc/>
        protected sealed override void OnSerialize()
        {
            if (Collection == null) Collection = APool.HashSet<V>();
            var buffer = new BufferByte();
            ToSerialize(buffer);
            Data = buffer.ToArray();
        }

        /// <inheritdoc/>
        public sealed override void Dispose()
        {
            Serialize();
            APool.Free(Collection);
        }
    }
}

namespace AIO.Unity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// 可持久化 列表数据
    /// </summary>
    /// <typeparam name="V">Value类型</typeparam>
    [DisplayName("可持久化 列表数据"), Description("处理数据主要数据为列表结构的数据文件")]
    public abstract class ScriptableList<V> : ScriptableData, IList<V>
    {
        /// <summary>
        /// 集合
        /// </summary>
        [NonSerialized] protected List<V> Collection;

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns></returns>
        public V this[int index]
        {
            get => Collection[index];
            set => Collection[index] = value;
        }

        /// <summary>
        /// 集合数量
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
        /// 判断存在
        /// </summary>
        public bool Contains(V item)
        {
            return Collection.Contains(item);
        }

        /// <summary>
        /// 复制到新数组
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="arrayIndex">目标数组下标</param>
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
        /// 获取泛型迭代器
        /// </summary>
        public IEnumerator<V> GetEnumerator()
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                yield return Collection[i];
            }
        }

        /// <summary>
        /// 判断元素存在下标 值为-1 未找到
        /// </summary>
        public int IndexOf(V item)
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 插入
        /// </summary>
        public void Insert(int index, V item)
        {
            Collection.Insert(index, item);
        }

        /// <summary>
        /// 移除
        /// </summary>
        public bool Remove(V item)
        {
            return Collection.Remove(item);
        }

        /// <summary>
        /// 移除指定下标元素
        /// </summary>
        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        protected sealed override void OnDeserialize()
        {
            Collection = Pool.List<V>.New();
            if (Data == null) return;
            ToDeserialize();
        }

        /// <inheritdoc/>
        protected sealed override void OnSerialize()
        {
            if (Collection == null) Collection = Pool.List<V>.New();
            ToSerialize();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        protected virtual void ToSerialize()
        {

        }

        /// <summary>
        /// 反序列化
        /// </summary>
        protected virtual void ToDeserialize()
        {

        }

        /// <inheritdoc/>
        public sealed override void Dispose()
        {
            Serialize();
            Pool.List<V>.Free(Collection);
        }
    }
}

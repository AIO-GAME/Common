namespace AIO.Unity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// 可持久化 字典数据
    /// </summary>
    /// <typeparam name="K">Key类型</typeparam>
    /// <typeparam name="V">Value类型</typeparam>
    [DisplayName("可持久化 字典数据"), Description("处理数据主要数据为字典结构的数据文件")]
    public abstract class ScriptableDictionary<K, V> : ScriptableData, IDictionary<K, V>
    {
        /// <summary>
        /// 集合
        /// </summary>
        [NonSerialized] protected Dictionary<K, V> Collection;

        /// <summary>
        /// 大小
        /// </summary>
        public virtual int Count
        {
            get { return Collection.Count; }
        }

        /// <summary>
        /// 只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }


        /// <summary>
        /// 获取Value值
        /// </summary>
        public virtual V this[K key]
        {
            get { return (V)Collection.GetOrDefault(key, null); }
            set { Collection.Set(key, value); }
        }

        /// <summary>
        /// 添加值
        /// </summary>
        public void Add(K key, V value)
        {
            Collection.Set(key, value);
        }

        /// <summary>
        /// 添加值
        /// </summary>
        public void Add(KeyValuePair<K, V> item)
        {
            Collection.Set(item.Key, item.Value);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            Collection.Clear();
        }

        /// <summary>
        /// 判断值是否存在
        /// </summary>
        public bool Contains(KeyValuePair<K, V> item)
        {
            return Collection.ContainsKey(item.Key);
        }

        /// <summary>
        /// 判断值是否存在
        /// </summary>
        public bool ContainsKey(K key)
        {
            return Collection.ContainsKey(key);
        }

        /// <summary>
        /// 判断值是否存在
        /// </summary>
        public bool ContainsValue(V item)
        {
            return Collection.ContainsValue(item);
        }

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="arrayIndex">目标数组开始下标</param>
        public virtual void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
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
        /// 移除数据
        /// </summary>
        public bool Remove(KeyValuePair<K, V> item)
        {
            return Collection.Remove(item.Key);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        public bool Remove(K item)
        {
            return Collection.Remove(item);
        }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">返回值</param>
        /// <returns>是否成功获取</returns>
        public bool TryGetValue(K key, out V value)
        {
            return Collection.TryGetValue(key, out value);
        }

        /// <summary>
        /// 键集合
        /// </summary>
        public ICollection<K> Keys
        {
            get { return Collection.Keys; }
        }

        /// <summary>
        /// 值集合
        /// </summary>
        public ICollection<V> Values
        {
            get { return Collection.Values; }
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<K, V>>)Collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        protected sealed override void OnDeserialize()
        {
            Collection = Pool.Dictionary<K, V>.New();
            if (Data == null) return;
            ToDeserialize();
        }

        /// <inheritdoc/>
        protected sealed override void OnSerialize()
        {
            if (Collection == null) Collection = Pool.Dictionary<K, V>.New();
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
            Pool.Dictionary<K, V>.Free(Collection);
        }
    }
}

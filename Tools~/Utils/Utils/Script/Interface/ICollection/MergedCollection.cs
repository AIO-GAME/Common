using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 实现 IMergedCollection 接口
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class MergedCollection<T> : IMergedCollection<T>
    {
        // 存储不同类型的集合
        private readonly Dictionary<Type, ICollection<T>> collections;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MergedCollection()
        {
            collections = new Dictionary<Type, ICollection<T>>();
        }

        /// <summary>
        /// 获取集合中元素的数量
        /// </summary>
        public int Count
        {
            get { return collections.Values.Sum(collection => collection.Count); }
        }

        /// <summary>
        /// 是否为只读集合
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 添加一个集合
        /// </summary>
        /// <typeparam name="TI">泛型类型</typeparam>
        /// <param name="collection">集合</param>
        public void Include<TI>(ICollection<TI> collection) where TI : T
        {
            collections.Add(typeof(TI), new VariantCollection<T, TI>(collection));
        }

        /// <summary>
        /// 是否包含某个类型的集合
        /// </summary>
        /// <typeparam name="TI">泛型类型</typeparam>
        /// <returns>是否包含某个类型的集合</returns>
        public bool Includes<TI>() where TI : T
        {
            return Includes(typeof(TI));
        }

        /// <summary>
        /// 是否包含某个类型的集合
        /// </summary>
        /// <param name="implementationType">实现类型</param>
        /// <returns>是否包含某个类型的集合</returns>
        public bool Includes(in Type implementationType)
        {
            return GetCollectionForType(implementationType, false) != null;
        }

        /// <summary>
        /// 获取某个类型的集合
        /// </summary>
        /// <typeparam name="TI">泛型类型</typeparam>
        /// <returns>某个类型的集合</returns>
        public ICollection<TI> ForType<TI>() where TI : T
        {
            return ((VariantCollection<T, TI>)GetCollectionForType(typeof(TI))).Implementation;
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return collections.Values.SelectMany(collection => collection).GetEnumerator();
        }

        /// <summary>
        /// 获取某个元素所在的集合
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>某个元素所在的集合</returns>
        private ICollection<T> GetCollectionForItem(T item)
        {
            return GetCollectionForType(item.GetType());
        }

        /// <summary>
        /// 获取某个类型的集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="throwOnFail">是否抛出异常</param>
        /// <returns>某个类型的集合</returns>
        private ICollection<T> GetCollectionForType(Type type, bool throwOnFail = true)
        {
            if (collections.ContainsKey(type))
            {
                return collections[type];
            }

            foreach (var collectionByType in collections.Where(collectionByType =>
                         collectionByType.Key.IsAssignableFrom(type)))
            {
                return collectionByType.Value;
            }

            if (throwOnFail)
            {
                throw new InvalidOperationException($"No sub-collection available for type '{type}'.");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否包含某个元素</returns>
        public bool Contains(T item)
        {
            return GetCollectionForItem(item).Contains(item);
        }

        /// <summary>
        /// 添加一个元素
        /// </summary>
        /// <param name="item">元素</param>
        public virtual void Add(T item)
        {
            GetCollectionForItem(item).Add(item);
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public virtual void Clear()
        {
            foreach (var collection in collections.Values)
            {
                collection.Clear();
            }
        }

        /// <summary>
        /// 移除某个元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否移除成功</returns>
        public virtual bool Remove(T item)
        {
            return GetCollectionForItem(item).Remove(item);
        }

        /// <summary>
        /// 将集合中的元素复制到数组中
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">数组下标</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < Count) throw new ArgumentException();

            var i = 0;

            foreach (var collection in collections.Values)
            {
                collection.CopyTo(array, arrayIndex + i);
                i += collection.Count;
            }
        }
    }
}
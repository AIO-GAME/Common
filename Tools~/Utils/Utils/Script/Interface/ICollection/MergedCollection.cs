using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 合并合计
    /// </summary>
    public class MergedCollection<T> : IMergedCollection<T>
    {
        private readonly Dictionary<Type, ICollection<T>> collections;

        /// <summary>
        /// 初始化
        /// </summary>
        public MergedCollection()
        {
            collections = new Dictionary<Type, ICollection<T>>();
        }

        /// <inheritdoc />
        public int Count
        {
            get { return collections.Values.Sum(collection => collection.Count); }
        }

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// 包含
        /// </summary>
        public void Include<TI>(ICollection<TI> collection) where TI : T
        {
            collections.Add(typeof(TI), new VariantCollection<T, TI>(collection));
        }

        /// <inheritdoc />
        public bool Includes<TI>() where TI : T
        {
            return Includes(typeof(TI));
        }

        /// <inheritdoc />
        public bool Includes(Type implementationType)
        {
            return GetCollectionForType(implementationType, false) != null;
        }

        /// <summary>
        /// 获取指定类型合集
        /// </summary>
        public ICollection<TI> ForType<TI>() where TI : T
        {
            return ((VariantCollection<T, TI>)GetCollectionForType(typeof(TI))).Implementation;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return collections.Values.SelectMany(collection => collection).GetEnumerator();
        }

        private ICollection<T> GetCollectionForItem(T item)
        {
            return GetCollectionForType(item.GetType());
        }

        private ICollection<T> GetCollectionForType(Type type, bool throwOnFail = true)
        {
            if (collections.ContainsKey(type))
            {
                return collections[type];
            }

            foreach (var collectionByType in collections.Where(collectionByType => collectionByType.Key.IsAssignableFrom(type)))
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

        /// <inheritdoc />
        public bool Contains(T item)
        {
            return GetCollectionForItem(item).Contains(item);
        }

        /// <inheritdoc />
        public virtual void Add(T item)
        {
            GetCollectionForItem(item).Add(item);
        }

        /// <inheritdoc />
        public virtual void Clear()
        {
            foreach (var collection in collections.Values)
            {
                collection.Clear();
            }
        }

        /// <inheritdoc />
        public virtual bool Remove(T item)
        {
            return GetCollectionForItem(item).Remove(item);
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException();
            }

            var i = 0;

            foreach (var collection in collections.Values)
            {
                collection.CopyTo(array, arrayIndex + i);
                i += collection.Count;
            }
        }
    }
}
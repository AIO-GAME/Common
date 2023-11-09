using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 合并键控集合
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public class MergedKeyedCollection<TKey, TItem> : IMergedCollection<TItem>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MergedKeyedCollection()
        {
            collections = new Dictionary<Type, IKeyedCollection<TKey, TItem>>();
            collectionsLookup = new Dictionary<Type, IKeyedCollection<TKey, TItem>>();
        }

        /// <summary>
        /// 
        /// </summary>
        protected readonly Dictionary<Type, IKeyedCollection<TKey, TItem>> collections;

        // Used for performance optimization when finding the right collection for a type
        /// <summary>
        /// 
        /// </summary>
        protected readonly Dictionary<Type, IKeyedCollection<TKey, TItem>> collectionsLookup;

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键值</param>
        /// <exception cref="ArgumentNullException">key为Null</exception>
        /// <exception cref="KeyNotFoundException">值不存在</exception>
        public TItem this[TKey key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException(nameof(key));

                foreach (var collectionByType in collections.Where(collectionByType =>
                             collectionByType.Value.Contains(key)))
                {
                    return collectionByType.Value[key];
                }

                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc />
        public int Count
        {
            get { return collections.Sum(collectionByType => collectionByType.Value.Count); }
        }

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public bool Includes<TSubItem>() where TSubItem : TItem
        {
            return Includes(typeof(TSubItem));
        }

        /// <inheritdoc />
        public bool Includes(Type elementType)
        {
            return GetCollectionForType(elementType, false) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSubItem"></typeparam>
        /// <returns></returns>
        public IKeyedCollection<TKey, TSubItem> ForType<TSubItem>() where TSubItem : TItem
        {
            return ((VariantKeyedCollection<TItem, TSubItem, TKey>)GetCollectionForType(typeof(TSubItem)))
                .implementation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="TSubItem"></typeparam>
        public virtual void Include<TSubItem>(IKeyedCollection<TKey, TSubItem> collection) where TSubItem : TItem
        {
            var type = typeof(TSubItem);
            var variantCollection = new VariantKeyedCollection<TItem, TSubItem, TKey>(collection);
            collections.Add(type, variantCollection);
            collectionsLookup.Add(type, variantCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected IKeyedCollection<TKey, TItem> GetCollectionForItem(TItem item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            return GetCollectionForType(item.GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="throwOnFail"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected IKeyedCollection<TKey, TItem> GetCollectionForType(Type type, bool throwOnFail = true)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (collectionsLookup.TryGetValue(type, out var collection))
            {
                return collection;
            }

            foreach (var collectionByType in collections.Where(collectionByType =>
                         collectionByType.Key.IsAssignableFrom(type)))
            {
                collection = collectionByType.Value;
                collectionsLookup.Add(type, collection);
                return collection;
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
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="throwOnFail"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected IKeyedCollection<TKey, TItem> GetCollectionForKey(TKey key, bool throwOnFail = true)
        {
            // Optim: avoid boxing here.
            // Ensure.That(nameof(key)).IsNotNull(key);

            foreach (var collectionsByType in collections.Where(collectionsByType =>
                         collectionsByType.Value.Contains(key)))
            {
                return collectionsByType.Value;
            }

            if (throwOnFail)
            {
                throw new InvalidOperationException($"No sub-collection available for key '{key}'.");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TItem value)
        {
            var collection = GetCollectionForKey(key, false);

            value = default(TItem);

            return collection != null && collection.TryGetValue(key, out value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TItem item)
        {
            GetCollectionForItem(item).Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            foreach (var collection in collections.Values)
            {
                collection.Clear();
            }
        }

        /// <inheritdoc />
        public bool Contains(TItem item)
        {
            return GetCollectionForItem(item).Contains(item);
        }

        /// <inheritdoc />
        public bool Remove(TItem item)
        {
            return GetCollectionForItem(item).Remove(item);
        }

        /// <inheritdoc />
        public void CopyTo(TItem[] array, int arrayIndex)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(TKey key)
        {
            return GetCollectionForKey(key, false) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return GetCollectionForKey(key).Remove(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public struct Enumerator : IEnumerator<TItem>
        {
            private Dictionary<Type, IKeyedCollection<TKey, TItem>>.Enumerator collectionsEnumerator;
            private IKeyedCollection<TKey, TItem> currentCollection;
            private int indexInCurrentCollection;
            private bool exceeded;

            /// <inheritdoc />
            public Enumerator(MergedKeyedCollection<TKey, TItem> merged) : this()
            {
                collectionsEnumerator = merged.collections.GetEnumerator();
            }

            /// <inheritdoc />
            public void Dispose()
            {
            }

            /// <inheritdoc />
            public bool MoveNext()
            {
                // We just started, so we're not in a collection yet
                if (currentCollection == null)
                {
                    // Try to find the first collection
                    if (collectionsEnumerator.MoveNext())
                    {
                        // There is at least a collection, start with this one
                        currentCollection = collectionsEnumerator.Current.Value;

                        if (currentCollection == null)
                        {
                            throw new InvalidOperationException("Merged sub collection is null.");
                        }
                    }
                    else
                    {
                        // There is no collection at all, stop
                        Current = default(TItem);
                        exceeded = true;
                        return false;
                    }
                }

                // Check if we're within the current collection
                if (indexInCurrentCollection < currentCollection.Count)
                {
                    // We are, return this element and move to the next
                    Current = currentCollection[indexInCurrentCollection];
                    indexInCurrentCollection++;
                    return true;
                }

                // We're beyond the current collection, but there may be more,
                // and because there may be many empty collections, we need to check
                // them all until we find an element, not just the next one
                while (collectionsEnumerator.MoveNext())
                {
                    currentCollection = collectionsEnumerator.Current.Value;
                    indexInCurrentCollection = 0;

                    if (currentCollection == null)
                    {
                        throw new InvalidOperationException("Merged sub collection is null.");
                    }

                    if (indexInCurrentCollection < currentCollection.Count)
                    {
                        Current = currentCollection[indexInCurrentCollection];
                        indexInCurrentCollection++;
                        return true;
                    }
                }

                // We're beyond all collections, stop
                Current = default(TItem);
                exceeded = true;
                return false;
            }

            /// <inheritdoc />
            public TItem Current { get; private set; }

            object IEnumerator.Current
            {
                get
                {
                    if (exceeded)
                    {
                        throw new InvalidOperationException();
                    }

                    return Current;
                }
            }

            void IEnumerator.Reset()
            {
                throw new InvalidOperationException();
            }
        }
    }
}
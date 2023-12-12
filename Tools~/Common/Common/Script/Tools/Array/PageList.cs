/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-11
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPageArray<out T>
    {
        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// 当前页内容
        /// </summary>
        T[] CurrentPageValues { get; }
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class PageList<T> :
        IList<T>,
        ICollection<T>,
        IEnumerable<T>,
        IPageArray<T>,
        IComparer<T>,
        IEnumerable
    {
        /// <inheritdoc />
        public int PageSize
        {
            get => _PageSize;
            set
            {
                _PageSize = value;
                if (_PageIndex >= PageCount) _PageIndex = PageCount - 1;
                CurrentPageValues = GetPage(_PageIndex);
            }
        }

        private int _PageSize = 30;

        /// <inheritdoc />
        public int PageIndex
        {
            get => _PageIndex;
            set
            {
                _PageIndex = value;
                CurrentPageValues = GetPage(_PageIndex);
            }
        }

        /// <inheritdoc />
        public int PageCount => (int)Math.Ceiling(Count / (float)PageSize);

        /// <inheritdoc />
        public T[] CurrentPageValues { get; private set; }

        private int _PageIndex = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageList()
        {
            Values = new List<T>();
        }

        /// <summary>
        /// 获取页内容
        /// </summary>
        private T[] GetPage(int index)
        {
            if (index < 0 || index >= PageCount) return Array.Empty<T>();
            var start = index * PageSize;
            var end = start + PageSize;
            if (end > Count) end = Count;
            var array = new T[end - start];
            for (var i = start; i < end; i++) array[i - start] = Values[i];
            return array;
        }

        /// <summary>
        /// 值
        /// </summary>
        protected List<T> Values { get; set; }

        /// <inheritdoc />
        public int Count => Values.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public void Add(T item)
        {
            Values.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Values.Clear();
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            return Values.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            Values.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(T item)
        {
            return Values.Remove(item);
        }

        /// <inheritdoc />
        public int IndexOf(T item)
        {
            return Values.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, T item)
        {
            Values.Insert(index, item);
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            Values.RemoveAt(index);
        }

        /// <inheritdoc />
        public T this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        /// <inheritdoc />
        public int Compare(T x, T y)
        {
            return Values.IndexOf(x).CompareTo(Values.IndexOf(y));
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort(Comparison<T> comparison)
        {
            Values.Sort(comparison);
            CurrentPageValues = GetPage(PageIndex);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort() => this.Sort(0, this.Count, (IComparer<T>)null);

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort(IComparer<T> comparer) => this.Sort(0, this.Count, comparer);

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();
            if (count < 0)
                throw new ArgumentException();
            if (Values.Count - index < count)
                throw new ArgumentException();
            var temp = Values.ToArray();
            Array.Sort(temp, index, count, comparer);
            Values = new List<T>(temp);
            CurrentPageValues = GetPage(PageIndex);
        }
    }
}
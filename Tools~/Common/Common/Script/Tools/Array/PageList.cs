#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

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
    [Serializable, DebuggerDisplay("Count = {Count}")]
    public class PageList<T> : IList<T>, IPageArray<T>, IComparer<T>, IDisposable
    {
        private int _PageIndex;

        private int _PageSize = 16;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageList(int capacity)
        {
            Capacity = capacity;
            Values   = new T[capacity];
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageList() => Values = new T[Capacity];

        /// <summary>
        /// 值
        /// </summary>
        protected T[] Values { get; set; }

        /// <inheritdoc />
        public int Compare(T x, T y) => Comparer<T>.Default.Compare(x, y);

        /// <inheritdoc />
        public int Count => WriteIndex;

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity = 8;

        /// <summary>
        /// 写入索引
        /// </summary>
        private int WriteOffset;

        /// <summary>
        /// 写入索引
        /// </summary>
        protected int WriteIndex
        {
            get => WriteOffset;
            set
            {
                if (value < 0) return;
                if (value >= Capacity)
                {
                    while (value >= Capacity)
                    {
                        Capacity <<= 1;
                        if (int.MaxValue - Capacity < 0) throw new OutOfMemoryException();
                    }

                    var newValues = new T[Capacity];
                    Values.CopyTo(newValues, 0);
                    Values = newValues;
                }
                else WriteOffset = value;
            }
        }

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < WriteIndex; i++) yield return Values[i];
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Add(T item) => Values[++WriteIndex - 1] = item;

        /// <inheritdoc />
        public void Clear()
        {
            Values            = new T[Capacity];
            WriteOffset       = 0;
            CurrentPageValues = Array.Empty<T>();
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            for (var i = 0; i < WriteIndex; i++)
            {
                if (Values[i].Equals(item)) return true;
            }

            return false;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) => Values.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(T item)
        {
            var index = Array.IndexOf(Values, item);
            if (index < 0) return false;
            for (var i = index; i < WriteIndex - 1; i++) Values[i] = Values[i + 1];
            WriteIndex--;
            CurrentPageValues = GetPage(PageIndex);
            return true;
        }

        /// <inheritdoc />
        public int IndexOf(T item)
        {
            for (var i = 0; i < WriteIndex; i++)
            {
                if (Values[i].Equals(item)) return i;
            }

            return -1;
        }

        /// <inheritdoc />
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= WriteIndex) return;
            for (var i = WriteIndex; i > index; i--) Values[i] = Values[i - 1];
            Values[index] = item;
            WriteIndex++;
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= WriteIndex) return;
            for (var i = index; i < WriteIndex - 1; i++) Values[i] = Values[i + 1];
            WriteIndex--;
            CurrentPageValues = GetPage(PageIndex);
        }

        /// <inheritdoc />
        public T this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

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

        /// <inheritdoc />
        public int PageIndex
        {
            get => _PageIndex;
            set
            {
                _PageIndex        = value;
                CurrentPageValues = GetPage(_PageIndex);
            }
        }

        /// <inheritdoc />
        public int PageCount => (int)Math.Ceiling(Count / (float)PageSize);

        /// <inheritdoc />
        public T[] CurrentPageValues { get; private set; }

        /// <summary>
        /// 获取页内容
        /// </summary>
        private T[] GetPage(int index)
        {
            if (index < 0 || index >= PageCount) return Array.Empty<T>();
            var start = index * _PageSize;
            var end   = start + _PageSize;

            if (end > WriteIndex) end = WriteIndex;
            var array                 = new T[end - start];

            for (var i = start; i < end; i++) array[i - start] = Values[i];
            return array;
        }

        /// <summary>
        /// 反转
        /// </summary>
        public void Reverse()
        {
            for (var i = 0; i < WriteIndex / 2; i++) Values.Swap(i, WriteIndex - i - 1);
            CurrentPageValues = GetPage(PageIndex);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort() { Sort(0, WriteIndex, Comparer<T>.Default); }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort(IComparer<T> comparer) { Sort(0, WriteIndex, comparer); }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            if (index < 0) throw new IndexOutOfRangeException();
            if (count < 0) throw new ArgumentException();
            if (Values.Length - index < count) throw new ArgumentException();
            Array.Sort(Values, index, count, comparer);
            CurrentPageValues = GetPage(PageIndex);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Values            = null;
            CurrentPageValues = null;
        }
    }
}
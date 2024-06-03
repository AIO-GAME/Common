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
    public interface IPageArray<T> : ICollection<T>
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
        public PageList(int capacity) : this() { Capacity = capacity; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageList()
        {
            Values            = new T[Capacity];
            CurrentPageValues = Array.Empty<T>();
        }

        /// <summary>
        /// 值
        /// </summary>
        protected T[] Values { get; set; }

        /// <inheritdoc />
        public int Compare(T x, T y) => Comparer<T>.Default.Compare(x, y);

        /// <inheritdoc />
        public int Count => WriteOffset;

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

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() { CurrentPageValues = GetPage(_PageIndex); }

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < WriteOffset; i++) yield return Values[i];
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
            for (var i = 0; i < WriteOffset; i++)
            {
                if (Values[i].Equals(item)) return true;
            }

            return false;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new IndexOutOfRangeException();
            if (array.Length - arrayIndex < WriteOffset) throw new IndexOutOfRangeException();
            Array.ConstrainedCopy(Values, 0, array, arrayIndex, WriteOffset);
        }

        /// <inheritdoc />
        public bool Remove(T item)
        {
            var index = Array.IndexOf(Values, item);
            if (index < 0) return false;
            for (var i = index; i < WriteOffset - 1; i++) Values[i] = Values[i + 1];
            WriteIndex--;
            CurrentPageValues = GetPage(_PageIndex);
            return true;
        }

        /// <inheritdoc />
        public int IndexOf(T item)
        {
            for (var i = 0; i < WriteOffset; i++)
            {
                if (Values[i].Equals(item)) return i;
            }

            return -1;
        }

        /// <inheritdoc />
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= WriteOffset) return;
            for (var i = WriteOffset; i > index; i--) Values[i] = Values[i - 1];
            Values[index] = item;
            WriteIndex++;
        }

        /// <inheritdoc />
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= WriteOffset) return;
            for (var i = index; i < WriteOffset - 1; i++) Values[i] = Values[i + 1];
            WriteIndex--;
            CurrentPageValues = GetPage(_PageIndex);
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
                var count                           = PageCount;
                if (_PageIndex >= count) _PageIndex = count - 1;
                CurrentPageValues = GetPage(_PageIndex);
            }
        }

        /// <inheritdoc />
        public int PageIndex
        {
            get => _PageIndex;
            set
            {
                if (value < 0)
                {
                    _PageIndex        = 0;
                    CurrentPageValues = GetPage(_PageIndex);
                    return;
                }

                var count = PageCount;
                if (value >= count) _PageIndex = count - 1;
                else _PageIndex                = value;
                CurrentPageValues = GetPage(_PageIndex);
            }
        }

        /// <inheritdoc />
        public int PageCount => (int)Math.Ceiling(Count / (float)_PageSize);

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

            if (end > WriteOffset) end = WriteOffset;
            var des                    = new T[end - start];
            Array.ConstrainedCopy(Values, start, des, 0, des.Length);
            return des;
        }

        /// <summary>
        /// 反转
        /// </summary>
        public void Reverse()
        {
            for (var i = 0; i < WriteOffset / 2; i++) Values.Swap(i, WriteOffset - i - 1);
            CurrentPageValues = GetPage(_PageIndex);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="comparer"> 比较器 </param>
        public void Sort(IComparer<T> comparer) => Sort(0, WriteOffset, comparer);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="comparer"> 比较器 </param>
        public void Sort(Func<T, T, int> comparer) => Sort(0, WriteOffset, comparer);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="index"> 下标 </param>
        /// <param name="count"> 数量 </param>
        /// <param name="comparer"> 比较器 </param>
        public void Sort(int index, int count, Func<T, T, int> comparer)
        {
            if (index < 0) throw new IndexOutOfRangeException();
            if (count <= 0) return;
            var span = WriteOffset - index;
            if (span < count) throw new IndexOutOfRangeException();
            Array.Sort(Values, index, count, ExtendSort.Comparer(comparer));
            CurrentPageValues = GetPage(_PageIndex);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="index"> 下标 </param>
        /// <param name="count"> 数量 </param>
        /// <param name="comparer"> 比较器 </param>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            if (index < 0) throw new IndexOutOfRangeException();
            if (count <= 0) return;
            var span = WriteOffset - index;
            if (span < count) throw new IndexOutOfRangeException();
            Array.Sort(Values, index, count, comparer);
            CurrentPageValues = GetPage(_PageIndex);
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
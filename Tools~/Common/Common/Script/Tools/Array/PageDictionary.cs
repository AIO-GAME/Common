#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    /// 分页字典
    /// </summary>
    public class PageDictionary<T1, T2> : IDisposable, IDictionary<T1, T2>
    {
        /// <summary>
        /// 当前页内容
        /// </summary>
        private KeyValuePair<T1, T2>[] _CurrentPage;

        /// <summary>
        /// 当前页键值
        /// </summary>
        private T1[] _CurrentPageKeys;

        /// <summary>
        /// 当前页内容
        /// </summary>
        private T2[] _CurrentPageValues;

        private int _PageIndex = 1;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageDictionary()
        {
            Keys   = new List<T1>();
            Values = new List<T2>();
        }

        /// <summary>
        /// 键值
        /// </summary>
        public List<T1> Keys { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public List<T2> Values { get; private set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 50;

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get => _PageIndex;
            set => GetPage(value);
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => (int)Math.Ceiling(Keys.Count / (float)PageSize);

        /// <summary>
        /// 当前页内容数量
        /// </summary>
        public int CurrentPageCount => CurrentPageKeys.Length;

        /// <summary>
        /// 当前页内容
        /// </summary>
        public T2[] CurrentPageValues
        {
            get
            {
                if (_CurrentPageValues is null) GetPage(PageIndex);
                return _CurrentPageValues;
            }
        }

        /// <summary>
        /// 当前页键值
        /// </summary>
        public T1[] CurrentPageKeys
        {
            get
            {
                if (_CurrentPageKeys is null) GetPage(PageIndex);
                return _CurrentPageKeys;
            }
        }

        /// <summary>
        /// 当前页内容
        /// </summary>
        public KeyValuePair<T1, T2>[] CurrentPage
        {
            get
            {
                if (_CurrentPage is null) GetPage(PageIndex);
                return _CurrentPage;
            }
        }

        #region IDictionary<T1,T2> Members

        ICollection<T2> IDictionary<T1, T2>.Values => Values;

        ICollection<T1> IDictionary<T1, T2>.Keys => Keys;

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<T1, T2> item)
        {
            return Remove(item.Key);
        }

        /// <inheritdoc />
        public int Count => Keys.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        public void Add(T1 key, T2 value)
        {
            if (Keys.Contains(key))
            {
                var index = Keys.IndexOf(key);
                Keys[index]   = key;
                Values[index] = value;
            }
            else
            {
                Keys.Add(key);
                Values.Add(value);
            }
        }

        /// <inheritdoc />
        public bool ContainsKey(T1 key)
        {
            return Keys.Contains(key);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>Ture:成功 False:失败</returns>
        public bool Remove(T1 key)
        {
            if (!Keys.Contains(key)) return false;
            var index = Keys.IndexOf(key);
            Keys.RemoveAt(index);
            Values.RemoveAt(index);
            return true;
        }

        /// <inheritdoc />
        public bool TryGetValue(T1 key, out T2 value)
        {
            var index = Keys.IndexOf(key);
            if (index < 0)
            {
                value = default;
                return false;
            }

            value = Values[index];
            return true;
        }

        /// <inheritdoc />
        public T2 this[T1 key]
        {
            get => GetValue(key);
            set => Add(key, value);
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<T1, T2> item)
        {
            Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        public void Clear()
        {
            Keys.Clear();
            Values.Clear();
            PageIndex = 1;
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<T1, T2> item)
        {
            return ContainsKey(item.Key);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException(
                    "The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");

            for (var i = 0; i < Count; i++) array[i + arrayIndex] = new KeyValuePair<T1, T2>(Keys[i], Values[i]);
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return Keys.Select((t, i) => new KeyValuePair<T1, T2>(t, Values[i])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            Keys   = null;
            Values = null;
        }

        #endregion

        /// <summary>
        /// 获取页内容
        /// </summary>
        private void GetPage(int index)
        {
            _PageIndex = index;
            if (index < 0 || index >= PageCount)
            {
                _CurrentPageValues = Array.Empty<T2>();
                _CurrentPageKeys   = Array.Empty<T1>();
                _CurrentPage       = Array.Empty<KeyValuePair<T1, T2>>();
                return;
            }

            var start = index * PageSize;
            var end = start + PageSize;
            if (end > Count) end = Count;
            var array = new T2[end - start];
            var array2 = new T1[end - start];
            var array3 = new KeyValuePair<T1, T2>[end - start];
            for (var i = start; i < end; i++)
            {
                array[i - start]  = Values[i];
                array2[i - start] = Keys[i];
                array3[i - start] = new KeyValuePair<T1, T2>(Keys[i], Values[i]);
            }

            _CurrentPageValues = array;
            _CurrentPageKeys   = array2;
            _CurrentPage       = array3;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="index">指定下标</param>
        /// <returns>Ture:成功 False:失败</returns>
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Keys.Count) return false;
            Keys.RemoveAt(index);
            Values.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T2 GetValue(T1 key)
        {
            if (!Keys.Contains(key)) return default;
            var index = Keys.IndexOf(key);
            return Values[index];
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T2 GetValue(int index)
        {
            if (index < 0 || index >= Keys.Count) return default;
            return Values[index];
        }
    }
}
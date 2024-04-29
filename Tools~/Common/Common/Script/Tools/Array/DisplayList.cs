#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    /// 显示列表
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class DisplayList<T> : IDisposable, IDictionary<string, T>, IList<T>
    {
        private int _PageIndex = 1;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisplayList()
        {
            Keys     = new List<string>(4);
            Displays = new List<string>(4);
            Values   = new List<T>(4);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisplayList(int capacity)
        {
            Keys     = new List<string>(capacity);
            Displays = new List<string>(capacity);
            Values   = new List<T>(capacity);
        }

        /// <summary>
        /// 键值
        /// </summary>
        public IList<string> Keys { get; private set; }

        /// <summary>
        /// 显示
        /// </summary>
        public IList<string> Displays { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public IList<T> Values { get; private set; }

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
            set => CurrentPageValues = GetPage(value);
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => (int)Math.Ceiling(Keys.Count / (float)PageSize);

        /// <summary>
        /// 当前页内容
        /// </summary>
        public T[] CurrentPageValues { get; private set; }

        /// <inheritdoc cref="List{T}"/>
        public int Count => Keys.Count;

        #region IEnumerator

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (Values as IEnumerable<T>).GetEnumerator();

        #endregion

        #region IEnumerator

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region KeyValuePair<string,T> Members

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, T> item) => Remove(item.Key);

        /// <inheritdoc />
        public void Add(KeyValuePair<string, T> item) => Add(item.Key, item.Key, item.Value);

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, T> item) => ContainsKey(item.Key);

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator() =>
            Keys.Select((t, i) => new KeyValuePair<string, T>(t, Values[i])).GetEnumerator();

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new
                    ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");

            for (var i = 0; i < Count; i++) array[i + arrayIndex] = new KeyValuePair<string, T>(Keys[i], Values[i]);
        }

        #endregion

        #region IDictionary<string,T> Members

        ICollection<T> IDictionary<string, T>.Values => Values;

        ICollection<string> IDictionary<string, T>.Keys => Keys;

        /// <inheritdoc cref="List{T}"/>
        bool ICollection<T>.IsReadOnly => false;

        bool ICollection<KeyValuePair<string, T>>.IsReadOnly => false;

        /// <inheritdoc />
        public bool ContainsKey(string key) => Keys.Contains(key);

        void IDictionary<string, T>.Add(string key, T value) => Add(key, key, value);

        /// <inheritdoc />
        public bool Remove(string key)
        {
            if (!Keys.Contains(key)) return false;
            var index = Keys.IndexOf(key);
            Keys.RemoveAt(index);
            Displays.RemoveAt(index);
            Values.RemoveAt(index);
            return true;
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, out T value)
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
        public T this[string key]
        {
            get => GetValue(key: key);
            set => Add(key, key, value);
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            Keys     = null;
            Displays = null;
            Values   = null;
        }

        #endregion

        #region IList<T> Members

        bool ICollection<T>.Remove(T item)
        {
            if (!Values.Contains(item)) return false;
            var index = Values.IndexOf(item);
            Keys.RemoveAt(index);
            Displays.RemoveAt(index);
            Values.RemoveAt(index);
            return false;
        }

        /// <inheritdoc />
        public int IndexOf(T item) { return Values.IndexOf(item); }

        void IList<T>.Insert(int index, T item)
        {
            if (item is null) return;
            Keys.Insert(index, item.GetHashCode().ToString());
            Displays.Insert(index, item.ToString());
            Values.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index) => RemoveAt(index);

        T IList<T>.this[int index]
        {
            get => GetValue(index);
            set => Add(index.GetHashCode().ToString(), index.ToString(), value);
        }

        void ICollection<T>.Add(T item)
        {
            if (item is null) return;
            Add(item.GetHashCode().ToString(), item.ToString(), item);
        }

        bool ICollection<T>.Contains(T item) => Values.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException(
                                            "The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");

            for (var i = 0; i < Count; i++) array[i + arrayIndex] = Values[i];
        }

        #endregion

        /// <inheritdoc cref="List{T}"/>
        public void Clear()
        {
            Keys.Clear();
            Displays.Clear();
            Values.Clear();
        }

        /// <summary>
        /// 获取页内容
        /// </summary>
        private T[] GetPage(int index)
        {
            _PageIndex = index;
            if (index < 0 || index >= PageCount) return null;
            var start                                          = index * PageSize;
            var end                                            = start + PageSize;
            if (end > Count) end                               = Count;
            var array                                          = new T[end - start];
            for (var i = start; i < end; i++) array[i - start] = Values[i];

            CurrentPageValues = array;
            return array;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort()
        {
            Displays.Sort(Compare);
            for (var i = 0; i < Displays.Count - 1; i++)
            {
                var swap = Displays.IndexOf(Displays[i]);
                Keys.Swap(i, swap);
                Values.Swap(i, swap);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="display">显示值</param>
        /// <param name="value">值</param>
        public void Add(int key, in string display, in T value) => Add(key.ToString(), display, value);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="display">显示值</param>
        /// <param name="value">值</param>
        public void Add(int key, in char display, in T value) => Add(key.ToString(), char.ToString(display), value);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="display">显示值</param>
        /// <param name="value">值</param>
        public void Add(string key, in string display, in T value)
        {
            if (Keys.Contains(key))
            {
                var index = Keys.IndexOf(key);
                Displays[index] = display;
                Displays.Sort(OnCompare ?? Compare);
                var dIndex = Displays.IndexOf(display);

                Keys[index]  = Keys[dIndex];
                Keys[dIndex] = key;

                Values[index]  = Values[dIndex];
                Values[dIndex] = value;
            }
            else
            {
                Displays.Add(display);
                Displays.Sort(OnCompare ?? Compare);
                var index = Displays.IndexOf(display);
                Keys.Insert(index, key);
                Values.Insert(index, value);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public event Func<string, string, int> OnCompare;

        private static int Compare(string a, string b) { return string.Compare(b, a, StringComparison.Ordinal); }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="index">指定下标</param>
        /// <returns>Ture:成功 False:失败</returns>
        public bool RemoveAt(in int index)
        {
            if (index < 0 || index >= Keys.Count) return false;
            Keys.RemoveAt(index);
            Displays.RemoveAt(index);
            Values.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>if found <see cref="T"/>; otherwise <c>default</c></returns>
        public T GetValue(in string key) { return !Keys.Contains(key) ? default : Values[Keys.IndexOf(key)]; }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>if found <see cref="T"/>; otherwise <c>default</c></returns>
        public T GetValue(in int index) { return index < 0 || index >= Keys.Count ? default : Values[index]; }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>if found <see cref="T"/>; otherwise <paramref name="defaultValue"/></returns>
        public T GetValueOrDefault(in int index, in T defaultValue = default)
        {
            return index < 0 || index >= Keys.Count ? defaultValue : Values[index];
        }

        /// <summary>
        /// 获取显示值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>if found <see cref="string"/>; otherwise <c>string.Empty</c></returns>
        public string GetDisplay(in string key)
        {
            return !Keys.Contains(key) ? string.Empty : Displays[Keys.IndexOf(key)];
        }

        /// <summary>
        /// 获取下标
        /// </summary>
        /// <param name="predicate">条件</param>
        public int FindIndex(Func<T, bool> predicate)
        {
            for (var i = 0; i < Values.Count; i++)
            {
                if (predicate(Values[i])) return i;
            }

            return -1;
        }
    }
}
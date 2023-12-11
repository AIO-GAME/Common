/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-04
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// 显示列表
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class DisplayList<T> : IDisposable, IDictionary<string, T>, IList<T>
    {
        ICollection<T> IDictionary<string, T>.Values => Values;

        ICollection<string> IDictionary<string, T>.Keys => Keys;

        /// <summary>
        /// 键值
        /// </summary>
        public List<string> Keys { get; private set; }

        /// <summary>
        /// 显示
        /// </summary>
        public List<string> Displays { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public List<T> Values { get; private set; }

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

        private int _PageIndex = 1;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => (int)Math.Ceiling(Count / (float)PageSize);

        /// <summary>
        /// 当前页内容
        /// </summary>
        public T[] CurrentPageValues { get; private set; }

        /// <summary>
        /// 获取页内容
        /// </summary>
        private T[] GetPage(int index)
        {
            _PageIndex = index;
            if (index < 0 || index >= PageCount) return null;
            var start = index * PageSize;
            var end = start + PageSize;
            if (end > Count) end = Count;
            var array = new T[end - start];
            for (var i = start; i < end; i++)
            {
                array[i - start] = Values[i];
            }

            CurrentPageValues = array;
            return array;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, T> item)
        {
            return Remove(item.Key);
        }

        /// <inheritdoc />
        public bool Remove(T item)
        {
            if (!Values.Contains(item)) return false;
            var index = Values.IndexOf(item);
            Keys.RemoveAt(index);
            Displays.RemoveAt(index);
            Values.RemoveAt(index);
            return true;
        }

        /// <inheritdoc cref="List{T}"/>
        public int Count => Keys.Capacity;

        /// <inheritdoc cref="List{T}"/>
        public bool IsReadOnly => false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisplayList()
        {
            Keys = new List<string>();
            Displays = new List<string>();
            Values = new List<T>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="display">显示值</param>
        /// <param name="value">值</param>
        public void Add(string key, string display, T value)
        {
            if (Keys.Contains(key))
            {
                var index = Keys.IndexOf(key);
                Keys[index] = key;
                Displays[index] = display;
                Values[index] = value;
            }
            else
            {
                Keys.Add(key);
                Displays.Add(display);
                Values.Add(value);
            }
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        /// <inheritdoc />
        public void Add(string key, T value)
        {
            Add(key, key, value);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>Ture:成功 False:失败</returns>
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
            get => GetValue(key);
            set => Add(key, key, value);
        }

        /// <inheritdoc />
        public int IndexOf(T item)
        {
            return Values.IndexOf(item);
        }

        /// <inheritdoc />
        public void Insert(int index, T item)
        {
            if (item is null) return;
            Keys.Insert(index, item.GetHashCode().ToString());
            Displays.Insert(index, item.ToString());
            Values.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        /// <inheritdoc />
        public T this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
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
            Displays.RemoveAt(index);
            Values.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T GetValue(string key)
        {
            if (!Keys.Contains(key)) return default;
            var index = Keys.IndexOf(key);
            return Values[index];
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T GetValue(int index)
        {
            if (index < 0 || index >= Keys.Count) return default;
            return Values[index];
        }

        /// <summary>
        /// 获取显示值
        /// </summary>
        public string GetDisplay(string key)
        {
            if (!Keys.Contains(key)) return default;
            var index = Keys.IndexOf(key);
            return Displays[index];
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<string, T> item)
        {
            Add(item.Key, item.Key, item.Value);
        }

        /// <inheritdoc />
        public void Add(T item)
        {
            if (item is null) return;
            Add(item.GetHashCode().ToString(), item.ToString(), item);
        }

        /// <inheritdoc cref="List{T}"/>
        public void Clear()
        {
            Keys.Clear();
            Displays.Clear();
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
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException(
                    "The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");

            for (var i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = Values[i];
            }
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, T> item)
        {
            return ContainsKey(item.Key);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException(
                    "The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");

            for (var i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<string, T>(Keys[i], Values[i]);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Keys = null;
            Displays = null;
            Values = null;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)Values).GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return Keys.Select((t, i) => new KeyValuePair<string, T>(t, Values[i])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
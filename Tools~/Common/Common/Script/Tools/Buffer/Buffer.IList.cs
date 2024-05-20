#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    public partial class Buffer<T> : IList<T>, IDisposable
    {
        #region IDisposable Members

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose() { Arrays = null; }

        #endregion

        #region IList<T> Members

        /// <summary>
        /// 是否为只读
        /// </summary>
        public virtual bool IsReadOnly => false;

        /// <summary>
        /// 获取指定元素
        /// </summary>
        /// <param name="index">下标</param>
        public virtual T this[int index]
        {
            get => Arrays[index];
            set => Arrays[index] = value;
        }

        /// <summary>
        /// 判断元素所在下标
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>-1未找到指定下标</returns>
        public int IndexOf(T item)
        {
            for (var i = 0; i < WriteIndex; i++)
                if (Arrays[i].Equals(item))
                    return i;

            return -1;
        }

        /// <summary>
        /// 在指定位置插入元素
        /// </summary>
        /// <param name="index">下标</param>
        /// <param name="item">元素</param>
        /// <exception cref="IndexOutOfRangeException">数组越界</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > WriteIndex) throw new IndexOutOfRangeException();
            if (index < WriteIndex)
            {
                WriteIndex++;
                var copy = new byte[WriteIndex - index];
                Array.Copy(Arrays, index, copy, 0, copy.Length);
                Array.Copy(copy, 0, Arrays, index + 1, copy.Length);
            }

            Arrays[index] = item;
        }

        /// <summary>
        /// 移除指定下标元素
        /// </summary>
        /// <param name="index">下标</param>
        /// <exception cref="IndexOutOfRangeException">数组越界</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > WriteIndex) throw new IndexOutOfRangeException();
            if (index < WriteIndex)
            {
                WriteIndex--;
                var copy = new byte[WriteIndex - index - 1];
                Array.Copy(Arrays, index + 1, copy, 0, copy.Length);
                Array.Copy(copy, 0, Arrays, index, copy.Length);
            }
        }

        /// <summary>
        /// 添加指定元素
        /// </summary>
        /// <param name="item">元素</param>
        public void Add(T item) { Insert(WriteIndex, item); }

        /// <summary>
        /// 确定序列是否包含指定的元素使用的默认相等比较器。
        /// </summary>
        /// <param name="item">要在其中定位某个值的序列。</param>
        /// <returns>true 如果源序列包含具有指定的值; 的元素否则为 false。</returns>
        public bool Contains(T item) { return Arrays.Contains(item); }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="arrayIndex">数组下标</param>
        public void CopyTo(T[] array, int arrayIndex) { Array.Copy(array, arrayIndex, Arrays, 0, WriteIndex); }

        /// <summary>
        /// 移除指定元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>true 移除成功 false 集合不包含该元素</returns>
        public bool Remove(T item)
        {
            for (var index = 0; index < WriteIndex; index++)
                if (Arrays[index].Equals(item))
                {
                    RemoveAt(index);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns>返回指定元素</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Arrays.Length; index++)
            {
                yield return Arrays[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return Arrays.GetEnumerator(); }

        #endregion
    }
}
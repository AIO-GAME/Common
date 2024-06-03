#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    /// 变种泛型 TImplementation必须是TBase的子类或本身
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public class VariantCollection<TBase, TImplementation> : ICollection<TBase>
    where TImplementation : TBase
    {
        /// <summary>
        /// 初始化VariantCollection对象，
        /// </summary>
        /// <param name="implementation"></param>
        public VariantCollection(in ICollection<TImplementation> implementation)
        {
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }

        /// <summary>
        /// 数据
        /// </summary>
        public ICollection<TImplementation> Implementation { get; }

        #region ICollection<TBase> Members

        /// <summary>
        /// 获取 Implementation 中包含的元素数。
        /// </summary>
        public int Count => Implementation.Count;

        /// <summary>
        /// 获取一个值，该值指示 Implementation 是否为只读。
        /// </summary>
        public bool IsReadOnly => Implementation.IsReadOnly;

        /// <summary>
        /// 返回一个IEnumerator(TBase)类型的枚举器，用于遍历 implementatio n中的元素；
        /// </summary>
        /// <returns>支持在泛型集合上进行简单迭代</returns>
        public IEnumerator<TBase> GetEnumerator()
        {
            return Implementation.Cast<TBase>().GetEnumerator();
        }

        /// <summary>
        /// 将某项添加到 implementatio 中。
        /// </summary>
        /// <param name="item">要添加到 ICollection(T) 的对象。</param>
        /// <exception cref="NotSupportedException">item不是TImplementation类型</exception>
        public void Add(TBase item)
        {
            if (!(item is TImplementation imp)) throw new NotSupportedException();
            Implementation.Add(imp);
        }

        /// <summary>
        /// 从implementation中移除所有元素；
        /// </summary>
        public void Clear()
        {
            Implementation.Clear();
        }

        /// <summary>
        /// 判断implementation是否包含某个TBase类型的item。
        /// 如果item不是TImplementation类型，则抛出NotSupportedException异常；
        /// </summary>
        /// <param name="item">要在 ICollection(T) 中查找的对象。</param>
        /// <returns>如果在 ICollection(T) 中找到 item，则为 true；否则为 false。</returns>
        /// <exception cref="NotSupportedException">item不是TImplementation类型</exception>
        public bool Contains(TBase item)
        {
            if (!(item is TImplementation imp)) throw new NotSupportedException();
            return Implementation.Contains(imp);
        }

        /// <summary>
        /// 从implementation中移除指定的TBase类型的item。
        /// 如果item不是TImplementation类型，则抛出NotSupportedException异常；
        /// </summary>
        /// <param name="item">要从 ICollection(T) 中移除的对象。</param>
        /// <returns>如果 item 成功从 ICollection(T) 中移除，则为 true；否则为 false。 如果在原始 ICollection(T) 中没有找到 item，该方法也会返回 false。</returns>
        /// <exception cref="NotSupportedException">item不是TImplementation类型</exception>
        public bool Remove(TBase item)
        {
            if (!(item is TImplementation imp)) throw new NotSupportedException();
            return Implementation.Remove(imp);
        }

        /// <summary>
        /// 将implementation中的元素复制到一个TBase类型的数组中。
        /// 如果array为null，则抛出ArgumentNullException异常；
        /// 如果arrayIndex小于0，则抛出ArgumentOutOfRangeException异常；
        /// 如果array的长度减去arrayIndex小于Count，则抛出ArgumentException异常。
        /// </summary>
        /// <param name="array">作为从 ICollection(T) 复制的元素的目标的一维数组。 数组必须具有从零开始的索引。</param>
        /// <param name="arrayIndex">array 中从零开始的索引，从此处开始复制。</param>
        /// <exception cref="ArgumentNullException">array 为 null。</exception>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex 小于 0。</exception>
        /// <exception cref="ArgumentException">源 ICollection(T) 中的元素数大于从 arrayIndex 到数组末尾之间的可用空间。</exception>
        public void CopyTo(TBase[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count) throw new ArgumentException();

            var implementationArray = new TImplementation[Count];
            Implementation.CopyTo(implementationArray, 0);
            for (var i = 0; i < Count; i++) array[i + arrayIndex] = implementationArray[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
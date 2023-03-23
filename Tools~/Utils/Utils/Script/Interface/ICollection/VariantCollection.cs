using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 变种泛型 TImplementation必须是TBase的子类或本身
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    public class VariantCollection<TBase, TImplementation> : ICollection<TBase> where TImplementation : TBase
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
        public ICollection<TImplementation> Implementation { get; private set; }

        /// <summary>
        /// 获取 Implementation 中包含的元素数。
        /// </summary>
        public int Count => Implementation.Count;

        /// <summary>
        /// 获取一个值，该值指示 Implementation 是否为只读。
        /// </summary>
        public bool IsReadOnly => Implementation.IsReadOnly;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 返回一个IEnumerator(TBase)类型的枚举器，用于遍历 implementatio n中的元素；
        /// </summary>
        /// <returns>支持在泛型集合上进行简单迭代</returns>
        public IEnumerator<TBase> GetEnumerator()
        {
            foreach (var i in Implementation)
            {
                yield return i;
            }
        }

        /// <summary>
        /// 将某项添加到 implementatio 中。
        /// </summary>
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
        public bool Contains(TBase item)
        {
            if (!(item is TImplementation imp)) throw new NotSupportedException();
            return Implementation.Contains(imp);
        }

        /// <summary>
        /// 从implementation中移除指定的TBase类型的item。
        /// 如果item不是TImplementation类型，则抛出NotSupportedException异常；
        /// </summary>
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
        public void CopyTo(TBase[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count) throw new ArgumentException();

            var implementationArray = new TImplementation[Count];
            Implementation.CopyTo(implementationArray, 0);
            for (var i = 0; i < Count; i++) array[i + arrayIndex] = implementationArray[i];
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 数据队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinStack<T> :
        IBinData,
        ICollection,
        IReadOnlyCollection<T>
        where T : IBinData, new()
    {
        /// <summary>
        /// 集合
        /// </summary>
        protected Stack<T> Collection { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        public BinStack()
        {
            Collection = Pool.Stack<T>.New();
        }

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            Collection.Free();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void Deserialize(IReadData buffer)
        {
            var len = buffer.ReadLen();
            for (var i = 0; i < len; i++) Collection.Push(buffer.ReadData<T>());
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void Serialize(IWriteData buffer)
        {
            buffer.WriteLen(Collection.Count);
            foreach (var item in Collection) buffer.WriteData(item);
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            foreach (var item in Collection) item.Reset();
        }

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>用于循环访问集合的枚举数。</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Collection).GetEnumerator();
        }

        /// <summary>返回循环访问集合的枚举数。</summary>
        /// <returns>
        ///   一个可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///   从特定的 <see cref="T:System.Collections.ICollection" /> 索引处开始，将 <see cref="T:System.Array" /> 的元素复制到一个 <see cref="T:System.Array" /> 中。
        /// </summary>
        /// <param name="array">
        ///   一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.ICollection" /> 复制的元素的目标。
        ///   <see cref="T:System.Array" /> 必须具有从零开始的索引。
        /// </param>
        /// <param name="index">
        ///   <paramref name="array" /> 中从零开始的索引，从此处开始复制。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" /> 为 <see langword="null" />。
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" /> 小于零。
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="array" /> 是多维的。
        ///   - 或 -
        ///   源中的元素数目 <see cref="T:System.Collections.ICollection" /> 大于从的可用空间 <paramref name="index" /> 目标从头到尾 <paramref name="array" />。
        ///   - 或 -
        ///   无法自动将源 <see cref="T:System.Collections.ICollection" /> 的类型转换为目标 <paramref name="array" /> 的类型。
        /// </exception>
        public void CopyTo(Array array, int index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The value of arrayIndex is out of range.");
            }

            if (index + Collection.Count >= array.Length)
            {
                throw new ArgumentException("The length of array is less than the number of elements in the collection.");
            }

            Array.Copy(Collection.ToArray(), 0, array, index, Collection.Count);
        }

        /// <summary>
        ///   获取 <see cref="T:System.Collections.ICollection" /> 中包含的元素数。
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Collections.ICollection" /> 中包含的元素数。
        /// </returns>
        int ICollection.Count => Collection.Count;

        /// <summary>
        ///   获取可用于同步对 <see cref="T:System.Collections.ICollection" /> 的访问的对象。
        /// </summary>
        /// <returns>
        ///   可用于同步对 <see cref="T:System.Collections.ICollection" /> 的访问的对象。
        /// </returns>
        public object SyncRoot => Collection;

        /// <summary>
        ///   获取一个值，该值指示是否同步对 <see cref="T:System.Collections.ICollection" /> 的访问（线程安全）。
        /// </summary>
        /// <returns>
        ///   如果对 <see langword="true" /> 的访问是同步的（线程安全），则为 <see cref="T:System.Collections.ICollection" />；否则为 <see langword="false" />。
        /// </returns>
        public bool IsSynchronized => false;

        /// <summary>获取集合中的元素数。</summary>
        /// <returns>集合中的元素数。</returns>
        int IReadOnlyCollection<T>.Count => Collection.Count;

        /// <inheritdoc />
        public virtual object Clone()
        {
            var data = new BinStack<T>();
            foreach (var item in Collection)
            {
                data.Collection.Push((T)item.Clone());
            }

            return data;
        }
    }
}
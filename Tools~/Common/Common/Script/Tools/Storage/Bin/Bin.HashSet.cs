// #region
//
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Diagnostics;
// using System.Runtime.Serialization;
// using System.Security;
// using System.Security.Permissions;
//
// #endregion
//
// namespace AIO
// {
//     /// <summary>
//     /// 列表存储
//     /// </summary>
//     [DebuggerDisplay("Count = {Count}"), SecurityCritical, Description("列表存储"), DisplayName("列表存储"), HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
//     public class JsonHashSet<T>
//         : IJsonData,
//           ISerializable,
//           IDeserializationCallback,
//           ISet<T>,
//           IReadOnlyCollection<T>
//     where T : IJsonData, new()
//     {
//         /// <summary>
//         /// 初始化
//         /// </summary>
//         public JsonHashSet()
//         {
//             Collection = Pool.AHashSet<T>.New();
//         }
//
//         /// <summary>
//         /// 集合
//         /// </summary>
//         protected HashSet<T> Collection { get; }
//
//         #region IBinData Members
//
//         /// <summary>
//         /// 执行与释放或重置非托管资源关联的应用程序定义的任务
//         /// </summary>
//         public void Dispose()
//         {
//             Collection.Free();
//         }
//
//         /// <inheritdoc />
//         public void Deserialize(IReadBasics buffer)
//         {
//             if (buffer.Count == 0) return;
//             buffer.ReadDataArray(Collection);
//         }
//
//         /// <inheritdoc />
//         public void Serialize(IWriteBasics buffer)
//         {
//             buffer.WriteDataArray(Collection);
//         }
//
//         /// <summary>
//         /// 重置
//         /// </summary>
//         public void Reset()
//         {
//             foreach (var item in Collection) item.Reset();
//         }
//
//         /// <inheritdoc />
//         public virtual object Clone()
//         {
//             var data = new JsonHashSet<T>();
//             foreach (var item in Collection) data.Collection.Add((T)item.Clone());
//
//             return data;
//         }
//
//         #endregion
//
//         #region IDeserializationCallback Members
//
//         /// <summary>在整个对象图形已经反序列化时运行。</summary>
//         /// <param name="sender">
//         ///   启动回调的对象。
//         ///    当前未实现该参数的功能。
//         /// </param>
//         public void OnDeserialization(object sender)
//         {
//             Collection.OnDeserialization(sender);
//         }
//
//         #endregion
//
//         #region ISerializable Members
//
//         /// <summary>
//         ///   使用将目标对象序列化所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。
//         /// </summary>
//         /// <param name="info">
//         ///   要填充数据的 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。
//         /// </param>
//         /// <param name="context">
//         ///   此序列化的目标（请参见 <see cref="T:System.Runtime.Serialization.StreamingContext" />）。
//         /// </param>
//         /// <exception cref="T:System.Security.SecurityException">
//         ///   调用方没有所要求的权限。
//         /// </exception>
//         public void GetObjectData(SerializationInfo info, StreamingContext context)
//         {
//             Collection.GetObjectData(info, context);
//         }
//
//         #endregion
//
//         #region ISet<T> Members
//
//         /// <summary>返回一个循环访问集合的枚举器。</summary>
//         /// <returns>用于循环访问集合的枚举数。</returns>
//         public IEnumerator<T> GetEnumerator()
//         {
//             return ((IEnumerable<T>)Collection).GetEnumerator();
//         }
//
//         /// <summary>返回循环访问集合的枚举数。</summary>
//         /// <returns>
//         ///   一个可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。
//         /// </returns>
//         IEnumerator IEnumerable.GetEnumerator()
//         {
//             return GetEnumerator();
//         }
//
//         /// <summary>
//         ///   将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 中。
//         /// </summary>
//         /// <param name="item">
//         ///   要添加到 <see cref="T:System.Collections.Generic.ICollection`1" /> 的对象。
//         /// </param>
//         /// <exception cref="T:System.NotSupportedException">
//         ///   <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读。
//         /// </exception>
//         public void Add(T item)
//         {
//             Collection.Add(item);
//         }
//
//         /// <summary>
//         /// 对该集合进行与另一个IEnumerable (T)对象的并集运算；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         public void UnionWith(in IEnumerable<T> other)
//         {
//             Collection.UnionWith(other);
//         }
//
//         /// <summary>
//         /// 对该集合进行与另一个IEnumerable (T)对象的交集运算；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         public void IntersectWith(in IEnumerable<T> other)
//         {
//             Collection.IntersectWith(other);
//         }
//
//         /// <summary>
//         /// 将该集合中与其他集合重复的元素移除；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         public void ExceptWith(in IEnumerable<T> other)
//         {
//             Collection.ExceptWith(other);
//         }
//
//         /// <summary>
//         /// 将该集合变换成只包含在该集合或者另一个集合中而不同时属于两个集合的元素；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         public void SymmetricExceptWith(in IEnumerable<T> other)
//         {
//             Collection.SymmetricExceptWith(other);
//         }
//
//         /// <summary>
//         /// 判断该集合是否是另一个集合的子集；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         /// <returns>True:是 False:否</returns>
//         public bool IsSubsetOf(in IEnumerable<T> other)
//         {
//             return Collection.IsSubsetOf(other);
//         }
//
//         /// <summary>
//         /// 判断该集合是否是另一个集合的超集；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         /// <returns>True:是 False:否</returns>
//         public bool IsSupersetOf(in IEnumerable<T> other)
//         {
//             return Collection.IsSupersetOf(other);
//         }
//
//         /// <summary>
//         /// 判断该集合是否是另一个集合的真超集；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         /// <returns>True:是 False:否</returns>
//         public bool IsProperSupersetOf(in IEnumerable<T> other)
//         {
//             return Collection.IsProperSupersetOf(other);
//         }
//
//         /// <summary>
//         /// 判断该集合是否是另一个集合的真子集；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         /// <returns>True:是 False:否</returns>
//         public bool IsProperSubsetOf(in IEnumerable<T> other)
//         {
//             return Collection.IsProperSubsetOf(other);
//         }
//
//         /// <summary>
//         /// 判断该集合是否与另一个集合存在共同的元素；
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         /// <returns>True:是 False:否</returns>
//         public bool Overlaps(in IEnumerable<T> other)
//         {
//             return Collection.Overlaps(other);
//         }
//
//         /// <summary>
//         /// 判断该集合是否与另一个集合有完全相同的元素，且仅有这些元素。
//         /// </summary>
//         /// <param name="other">另一个集合</param>
//         /// <returns>True:是 False:否</returns>
//         public bool SetEquals(in IEnumerable<T> other)
//         {
//             return Collection.SetEquals(other);
//         }
//
//         /// <summary>
//         /// 将ITEM添加到集合中，如果已存在，则返回false；
//         /// </summary>
//         /// <param name="item">要添加的元素</param>
//         /// <returns>Ture:不存在 False:已存在</returns>
//         bool ISet<T>.Add(T item)
//         {
//             return Collection.Add(item);
//         }
//
//         /// <summary>
//         ///   从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除所有项。
//         /// </summary>
//         /// <exception cref="T:System.NotSupportedException">
//         ///   <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读。
//         /// </exception>
//         public void Clear()
//         {
//             Collection.Clear();
//         }
//
//         /// <summary>
//         ///   确定 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否包含特定值。
//         /// </summary>
//         /// <param name="item">
//         ///   要在 <see cref="T:System.Collections.Generic.ICollection`1" /> 中定位的对象。
//         /// </param>
//         /// <returns>
//         ///   如果在 <see langword="true" /> 中找到 <paramref name="item" />，则为 <see cref="T:System.Collections.Generic.ICollection`1" />；否则为 <see langword="false" />。
//         /// </returns>
//         public bool Contains(T item)
//         {
//             return Collection.Contains(item);
//         }
//
//         /// <summary>
//         ///   从特定的 <see cref="T:System.Collections.Generic.ICollection`1" /> 索引处开始，将 <see cref="T:System.Array" /> 的元素复制到一个 <see cref="T:System.Array" /> 中。
//         /// </summary>
//         /// <param name="array">
//         ///   一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.Generic.ICollection`1" /> 复制的元素的目标。
//         ///   <see cref="T:System.Array" /> 必须具有从零开始的索引。
//         /// </param>
//         /// <param name="arrayIndex">
//         ///   <paramref name="array" /> 中从零开始的索引，从此处开始复制。
//         /// </param>
//         /// <exception cref="T:System.ArgumentNullException">
//         ///   <paramref name="array" /> 为 <see langword="null" />。
//         /// </exception>
//         /// <exception cref="T:System.ArgumentOutOfRangeException">
//         ///   <paramref name="arrayIndex" /> 小于 0。
//         /// </exception>
//         /// <exception cref="T:System.ArgumentException">
//         ///   源中的元素数目 <see cref="T:System.Collections.Generic.ICollection`1" /> 大于从的可用空间 <paramref name="arrayIndex" /> 目标从头到尾 <paramref name="array" />。
//         /// </exception>
//         public void CopyTo(T[] array, int arrayIndex)
//         {
//             if (arrayIndex < 0 || arrayIndex >= array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "The value of arrayIndex is out of range.");
//
//             foreach (var item in Collection)
//             {
//                 if (arrayIndex >= array.Length) throw new ArgumentException("The length of array is less than the number of elements in the collection.");
//
//                 array[arrayIndex++] = item;
//             }
//         }
//
//         /// <summary>
//         ///   从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中移除特定对象的第一个匹配项。
//         /// </summary>
//         /// <param name="item">
//         ///   要从 <see cref="T:System.Collections.Generic.ICollection`1" /> 中删除的对象。
//         /// </param>
//         /// <returns>
//         ///   如果从 <see langword="true" /> 中成功移除 <paramref name="item" />，则为 <see cref="T:System.Collections.Generic.ICollection`1" />；否则为 <see langword="false" />。
//         ///    如果在原始 <see langword="false" /> 中没有找到 <paramref name="item" />，该方法也会返回 <see cref="T:System.Collections.Generic.ICollection`1" />。
//         /// </returns>
//         /// <exception cref="T:System.NotSupportedException">
//         ///   <see cref="T:System.Collections.Generic.ICollection`1" /> 为只读。
//         /// </exception>
//         public bool Remove(T item)
//         {
//             return Collection.Remove(item);
//         }
//
//         /// <summary>
//         ///   获取 <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。
//         /// </summary>
//         /// <returns>
//         ///   <see cref="T:System.Collections.Generic.ICollection`1" /> 中包含的元素数。
//         /// </returns>
//         public int Count => Collection.Count;
//
//         /// <summary>
//         ///   获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1" /> 是否为只读。
//         /// </summary>
//         /// <returns>
//         ///   如果 <see langword="true" /> 是只读的，则为 <see cref="T:System.Collections.Generic.ICollection`1" />；否则为 <see langword="false" />。
//         /// </returns>
//         public bool IsReadOnly => false;
//
//         #endregion
//     }
// }
namespace AIO
{
    using System.Collections.Generic;

    /// <summary>
    /// 这是一个泛型集合接口，保证其元素的唯一性，这个唯一性是由某个比较器定义的。它还支持基本的集合操作，如并集、交集、补集和排他补集。
    /// </summary>
    public interface ISet<T> : ICollection<T>
    {
        /// <summary>
        /// 将ITEM添加到集合中，如果已存在，则返回false；
        /// </summary>
        /// <param name="item">要添加的元素</param>
        /// <returns>Ture:不存在 False:已存在</returns>
        new bool Add(T item);

        /// <summary>
        /// 对该集合进行与另一个IEnumerable (T)对象的并集运算；
        /// </summary>
        /// <param name="other">另一个集合</param>
        void UnionWith(in IEnumerable<T> other);

        /// <summary>
        /// 对该集合进行与另一个IEnumerable (T)对象的交集运算；
        /// </summary>
        /// <param name="other">另一个集合</param>
        void IntersectWith(in IEnumerable<T> other);

        /// <summary>
        /// 将该集合中与其他集合重复的元素移除；
        /// </summary>
        /// <param name="other">另一个集合</param>
        void ExceptWith(in IEnumerable<T> other);

        /// <summary>
        /// 将该集合变换成只包含在该集合或者另一个集合中而不同时属于两个集合的元素；
        /// </summary>
        /// <param name="other">另一个集合</param>
        void SymmetricExceptWith(in IEnumerable<T> other);

        /// <summary>
        /// 判断该集合是否是另一个集合的子集；
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>True:是 False:否</returns>
        bool IsSubsetOf(in IEnumerable<T> other);

        /// <summary>
        /// 判断该集合是否是另一个集合的超集；
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>True:是 False:否</returns>
        bool IsSupersetOf(in IEnumerable<T> other);

        /// <summary>
        /// 判断该集合是否是另一个集合的真超集；
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>True:是 False:否</returns>
        bool IsProperSupersetOf(in IEnumerable<T> other);

        /// <summary>
        /// 判断该集合是否是另一个集合的真子集；
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>True:是 False:否</returns>
        bool IsProperSubsetOf(in IEnumerable<T> other);

        /// <summary>
        /// 判断该集合是否与另一个集合存在共同的元素；
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>True:是 False:否</returns>
        bool Overlaps(in IEnumerable<T> other);

        /// <summary>
        /// 判断该集合是否与另一个集合有完全相同的元素，且仅有这些元素。
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>True:是 False:否</returns>
        bool SetEquals(in IEnumerable<T> other);
    }
}
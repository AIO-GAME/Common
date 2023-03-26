using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 合并集合的接口
    /// </summary>
    /// <typeparam name="T">集合的类型</typeparam>
    public interface IMergedCollection<T> : ICollection<T>
    {
        /// <summary>
        /// 检查指定类型是否包含在集合中
        /// </summary>
        /// <typeparam name="TI">指定类型</typeparam>
        /// <returns>如果包含则为真，否则为假</returns>
        bool Includes<TI>() where TI : T;

        /// <summary>
        /// 检查指定类型是否包含在集合中
        /// </summary>
        /// <param name="elementType">元素的类型</param>
        /// <returns>如果包含则为真，否则为假</returns>
        bool Includes(in Type elementType);
    }
}
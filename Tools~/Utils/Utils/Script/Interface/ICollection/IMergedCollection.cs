using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 合并集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMergedCollection<T> : ICollection<T>
    {
        /// <summary>
        /// 包含
        /// </summary>
        bool Includes<TI>() where TI : T;

        /// <summary>
        /// 包含
        /// </summary>
        bool Includes(Type elementType);
    }
}

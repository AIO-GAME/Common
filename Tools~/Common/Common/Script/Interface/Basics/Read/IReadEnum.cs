/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-07-18
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;

namespace AIO
{
    /// <summary>
    /// IReadEnum
    /// </summary>
    public partial interface IReadEnum
    {
        /// <summary>
        /// 读取枚举值
        /// </summary>
        /// <typeparam name="T"> 枚举类型</typeparam>
        /// <returns>枚举值</returns>
        public T ReadEnum<T>()
        where T : struct, Enum;
    }
}
using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteEnum
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteEnum<T>(in T value, in bool reverse = false) where T : struct, Enum;

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteEnumArray<T>(in ICollection<T> value, in bool reverse = false) where T : struct, Enum;
    }
}
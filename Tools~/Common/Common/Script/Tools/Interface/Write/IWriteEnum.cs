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
        void WriteEnum<T>(T value) where T : struct, Enum;

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteEnumArray<T>(ICollection<T> value) where T : struct, Enum;
    }
}
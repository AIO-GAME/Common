using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteDecimal
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDecimal(in decimal value, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDecimalArray(in ICollection<decimal> value, in bool reverse = false);
    }
}
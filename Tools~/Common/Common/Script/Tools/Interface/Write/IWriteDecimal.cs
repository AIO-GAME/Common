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
        void WriteDecimal(decimal value, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDecimalArray(ICollection<decimal> value, bool reverse = false);
    }
}
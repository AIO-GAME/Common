#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteDecimal
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
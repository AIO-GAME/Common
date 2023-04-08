using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteDouble
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDouble(in double value, bool all = false, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDoubleArray(in ICollection<double> value, in bool all, in bool reverse = false);
    }
}
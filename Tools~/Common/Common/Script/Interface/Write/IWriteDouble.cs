#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteDouble
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDouble(double value, bool all = false, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteDoubleArray(ICollection<double> value, bool all, bool reverse = false);
    }
}
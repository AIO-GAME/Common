using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public partial interface IWriteFloat
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteFloat(in float value, bool all = false, in bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteFloatArray(in ICollection<float> value, in bool all, in bool reverse = false);
    }
}
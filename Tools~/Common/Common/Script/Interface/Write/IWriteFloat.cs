#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteFloat
    {
        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteFloat(float value, bool all = false, bool reverse = false);

        /// <summary>
        /// 写入指定数据类型
        /// </summary>
        void WriteFloatArray(ICollection<float> value, bool all, bool reverse = false);
    }
}
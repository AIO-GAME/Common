namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadUInt64
    {
        /// <summary>
        /// 读取 UInt64 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        ulong ReadUInt64(in bool reverse = false);

        /// <summary>
        /// 读取 UInt64 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        ulong[] ReadUInt64Array(in bool reverse = false);
    }
}
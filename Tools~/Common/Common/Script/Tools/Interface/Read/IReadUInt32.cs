namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadUInt32
    {
        /// <summary>
        /// 读取 UInt32 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        uint ReadUInt32(bool reverse = false);

        /// <summary>
        /// 读取 UInt32 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        uint[] ReadUInt32Array(bool reverse = false);
    }
}
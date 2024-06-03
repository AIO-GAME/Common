namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadUInt16
    {
        /// <summary>
        /// 读取 UInt16 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        ushort ReadUInt16(bool reverse = false);

        /// <summary>
        /// 读取 UInt16 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        ushort[] ReadUInt16Array(bool reverse = false);
    }
}
namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadSByte
    {
        /// <summary>
        /// 读取 SByte 数据类型
        /// </summary>
        /// <returns>返回数组</returns>
        sbyte ReadSByte();

        /// <summary>
        /// 读取 Sbyte 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>返回数组</returns>
        sbyte[] ReadSByteArray(in bool reverse = false);
    }
}
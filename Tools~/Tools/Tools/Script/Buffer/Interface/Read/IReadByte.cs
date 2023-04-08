namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadByte
    {
        /// <summary>
        /// 读取 Byte 数据类型
        /// </summary>
        /// <returns>数据值</returns>
        byte ReadByte();

        /// <summary>
        /// 读取 Byte 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        byte[] ReadByteArray(in bool reverse = false);
    }
}
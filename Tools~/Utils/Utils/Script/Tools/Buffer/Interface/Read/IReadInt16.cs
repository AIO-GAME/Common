namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadInt16
    {
        /// <summary>
        /// 读取 Int16 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        short ReadInt16(in bool reverse = false);

        /// <summary>
        /// 读取 Int16 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        short[] ReadInt16Array(in bool reverse = false);
    }
}
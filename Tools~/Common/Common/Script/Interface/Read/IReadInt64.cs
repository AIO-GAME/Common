namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadInt64
    {
        /// <summary>
        /// 读取 Int64 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        long ReadInt64(bool reverse = false);

        /// <summary>
        /// 读取 Int64 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        long[] ReadInt64Array(bool reverse = false);
    }
}
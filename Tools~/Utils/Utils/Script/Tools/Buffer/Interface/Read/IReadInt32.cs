namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadInt32
    {
        /// <summary>
        /// 读取 Int32 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        int ReadInt32(in bool reverse = false);

        /// <summary>
        /// 读取 Int32 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        int[] ReadInt32Array(in bool reverse = false);
    }
}
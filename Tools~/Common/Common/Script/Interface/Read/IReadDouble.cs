namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadDouble
    {
        /// <summary>
        /// 读取 Double 数据类型
        /// </summary>
        /// <param name="all">全部</param>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        double ReadDouble(bool all = false, bool reverse = false);

        /// <summary>
        /// 读取 Double 数组 数据类型
        /// </summary>
        /// <param name="all">全部</param>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        double[] ReadDoubleArray(bool all, bool reverse = false);
    }
}
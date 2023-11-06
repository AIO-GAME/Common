namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadFloat
    {
        /// <summary>
        /// 读取 Float 数组 数据类型
        /// </summary>
        /// <param name="all"></param>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        float[] ReadFloatArray(bool all, bool reverse = false);

        /// <summary>
        /// 读取 Float 数据类型
        /// </summary>
        /// <param name="all">全部</param>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        float ReadFloat(bool all = false, bool reverse = false);
    }
}
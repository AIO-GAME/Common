namespace AIO
{
    /// <summary>
    /// 读取Bool
    /// </summary>
    public interface IReadBool
    {
        /// <summary>
        /// 读取 Bool 数据类型
        /// </summary>
        bool ReadBool();

        /// <summary>
        /// 读取 Bool数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>Bool数组</returns>
        bool[] ReadBoolArray(bool reverse = false);
    }
}
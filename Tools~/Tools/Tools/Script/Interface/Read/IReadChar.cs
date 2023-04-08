namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadChar
    {
        /// <summary>
        /// 读取 Char 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        char ReadChar(in bool reverse = false);

        /// <summary>
        /// 读取 Char 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        char[] ReadCharArray(in bool reverse = false);
    }
}
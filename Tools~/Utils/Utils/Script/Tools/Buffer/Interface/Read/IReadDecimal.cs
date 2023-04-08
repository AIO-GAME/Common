namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadDecimal
    {
        /// <summary>
        /// 读取 Decimal 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        decimal ReadDecimal(in bool reverse = false);

        /// <summary>
        /// 读取 Decimal 数组 数据类型
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>数据值</returns>
        decimal[] ReadDecimalArray(in bool reverse = false);
    }
}
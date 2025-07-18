namespace AIO
{
    /// <summary>
    /// 写入指定数据类型
    /// </summary>
    public interface IWriteJsonData : IWriteBasics
    {
        /// <summary>
        /// 写入Json数据
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">泛型</typeparam>
        void WriteData<T>(T value, bool reverse = false);
    }
}
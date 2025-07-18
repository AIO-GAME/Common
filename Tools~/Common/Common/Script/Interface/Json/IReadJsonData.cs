namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadJsonData : IReadBasics
    {
        /// <summary>
        /// 读取 Json 数据
        /// </summary>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        /// <returns>数据值</returns>
        T ReadData<T>(bool reverse = false);

        /// <summary>
        /// 读取 Json 数据
        /// </summary>
        /// <param name="value"> 输出值</param>
        /// <param name="reverse">反转</param>
        /// <typeparam name="T">数据泛型</typeparam>
        void ReadData<T>(out T value, bool reverse = false);
    }
}
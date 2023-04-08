namespace AIO
{
    /// <summary>
    /// 反序列化
    /// </summary>
    public interface IBinDeserialize
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        void Deserialize(IReadIData buffer);
    }
}
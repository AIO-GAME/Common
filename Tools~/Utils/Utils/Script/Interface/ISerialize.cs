namespace AIO
{
    /// <summary>
    /// 序列化 反序列化
    /// </summary>
    public interface ISerialize
    {
        /// <summary>
        /// 数据
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// 序列化
        /// </summary>
        void Serialize();

        /// <summary>
        /// 反序列化
        /// </summary>
        void Deserialize();
    }
}

namespace AIO
{
    /// <summary>
    /// 序列化
    /// </summary>
    public interface IJsonSerialize
    {
        /// <summary>
        /// 序列化
        /// </summary>
        void Serialize(IWriteJsonData buffer);
    }
}
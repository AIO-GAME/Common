namespace AIO
{
    /// <summary>
    /// 序列化
    /// </summary>
    public interface IBinSerialize
    {
        /// <summary>
        /// 序列化
        /// </summary>
        void Serialize(IWriteData buffer);
    }
}
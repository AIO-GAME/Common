namespace AIO
{
    /// <summary>
    /// 反序列化
    /// </summary>
    public interface IJsonDeserialize
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        void Deserialize(IReadJsonData buffer);
    }
}
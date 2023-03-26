namespace AIO
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class SerializeAsAttribute : fsPropertyAttribute
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public SerializeAsAttribute(in string name) : base(name) { }
    }
}

namespace AIO
{
    using System;

    /// <summary>
    /// 序列化属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SerializeAttribute : Attribute
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public SerializeAttribute()
        {
        }
    }
}
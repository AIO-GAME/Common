namespace AIO
{
    using System;

    /// <summary>
    /// 无序列化
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DoNotSerializeAttribute : Attribute
    {
        /// <summary>
        /// 无序列化
        /// </summary>
        public DoNotSerializeAttribute() { }
    }
}

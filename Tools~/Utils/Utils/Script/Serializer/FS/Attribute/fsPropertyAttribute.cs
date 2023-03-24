namespace AIO
{
    using System;

    /// <summary>
    /// 显式地标记要序列化的属性。这也可用于指定属性在序列化期间应该使用的名称。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class fsPropertyAttribute : Attribute
    {
        /// <summary>
        /// 属性
        /// </summary>
        public fsPropertyAttribute() : this(in string.Empty) { }

        /// <summary>
        /// 属性
        /// </summary>
        /// <param name="name">名称</param>
        public fsPropertyAttribute(in string name)
        {
            Name = name;
        }

        /// <summary>
        /// 属性将在JSON序列化中使用的名称。
        /// </summary>
        public string Name;

        /// <summary>
        /// 为给定类型使用自定义转换器。指定要使用的转换器使用typeof。
        /// </summary>
        public Type Converter;
    }
}

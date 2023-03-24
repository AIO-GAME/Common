namespace AIO
{
    using System;

    /// <summary>
    /// 用[JsonIgnore]注释的给定属性或字段将不会被序列化。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class fsIgnoreAttribute : Attribute { }
}

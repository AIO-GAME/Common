namespace AIO
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 枚举描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 枚举描述属性
        /// </summary>
        /// <param name="description">描述字段</param>
        public EnumDescriptionAttribute(string description) : base() => Description = description;
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class AttributeFieldExtend
    {
        /// <summary>
        /// 获取自定义属性值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetDescription(this Enum value)
        {
            if (value is null) throw new ArgumentException(nameof(value));
            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            return description;
        }
    }
}

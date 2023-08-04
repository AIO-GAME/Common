using System;

namespace AIO.UEditor
{
    /// <summary>
    /// 属性解析
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ArgumentAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public EArgLabel Type { get; }

        /// <summary>
        /// 参数
        /// </summary>
        public ArgumentAttribute(string label, EArgLabel type)
        {
            Label = label;
            Type = type;
        }
    }
}

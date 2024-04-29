#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 属性解析
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ArgumentAttribute : Attribute
    {
        /// <summary>
        /// 参数
        /// </summary>
        public ArgumentAttribute(string label, EArgLabel type)
        {
            Label = label;
            Type  = type;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public EArgLabel Type { get; }
    }
}
namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 参数类型
    /// </summary>
    public enum EArgLabel
    {
        /// <summary>
        /// BOOL
        /// </summary>
        Bool,

        /// <summary>
        /// Int
        /// </summary>
        Integer,

        /// <summary>
        /// Int数组
        /// </summary>
        IntegerArray,

        /// <summary>
        /// 字符串
        /// </summary>
        String,

        /// <summary>
        /// 字符串数组
        /// </summary>
        StringArray,

        /// <summary>
        /// 枚举
        /// </summary>
        Enum,
    }

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

using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 显示状态检视器 - 参数condition为显示条件判断方法的名称，返回值必须为bool
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class DisplayAttribute : InspectorAttribute
    {
        public string Condition { get; private set; }

        /// <summary>
        /// 显示状态检视器
        /// </summary>
        /// <param name="condition">显示条件判断方法的名称，返回值必须为bool</param>
        public DisplayAttribute(string condition)
        {
            Condition = condition;
        }
    }
}
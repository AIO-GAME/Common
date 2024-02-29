using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 激活状态检视器 - 参数condition为激活条件判断方法的名称，返回值必须为bool
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class EnableAttribute : InspectorAttribute
    {
        public string Condition { get; private set; }

        /// <summary>
        /// 激活状态检视器
        /// </summary>
        /// <param name="condition">激活条件判断方法的名称，返回值必须为bool</param>
        public EnableAttribute(string condition)
        {
            Condition = condition;
        }
    }
}
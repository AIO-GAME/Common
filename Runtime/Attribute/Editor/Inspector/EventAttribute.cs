/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 事件、委托检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class EventAttribute : InspectorAttribute
    {
        public string Text { get; private set; }

        /// <summary>
        /// 事件、委托检视器
        /// </summary>
        /// <param name="text">显示名称</param>
        public EventAttribute(string text = null)
        {
            Text = text;
        }
    }
}
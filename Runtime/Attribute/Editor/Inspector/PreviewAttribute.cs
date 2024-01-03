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
    /// 预览检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class PreviewAttribute : InspectorAttribute
    {
        public float Size { get; private set; }

        /// <summary>
        /// 预览检视器
        /// </summary>
        /// <param name="size">预览框的大小</param>
        public PreviewAttribute(float size = 100)
        {
            Size = size;
        }
    }
}
#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 颜色检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class ColorAttribute : InspectorAttribute
    {
        /// <summary>
        /// 颜色检视器
        /// </summary>
        /// <param name="r">颜色r值</param>
        /// <param name="g">颜色g值</param>
        /// <param name="b">颜色b值</param>
        /// <param name="a">颜色a值</param>
        public ColorAttribute(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public float R { get; private set; }
        public float G { get; private set; }
        public float B { get; private set; }
        public float A { get; private set; }
    }
}
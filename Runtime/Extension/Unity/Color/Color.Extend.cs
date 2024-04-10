#region

using UnityEngine;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// Color扩展
    /// </summary>
    public static class ColorExtend
    {
        /// <summary>
        /// 设置透明度
        /// </summary>
        public static Color SetA(this ref Color color, float a)
        {
            color.a = a;
            return color;
        }

        /// <summary>
        /// 设置红色
        /// </summary>
        public static Color SetR(this ref Color color, float r)
        {
            color.r = r;
            return color;
        }

        /// <summary>
        /// 设置绿色
        /// </summary>
        public static Color SetG(this ref Color color, float g)
        {
            color.g = g;
            return color;
        }

        /// <summary>
        /// 设置蓝色
        /// </summary>
        public static Color SetB(this ref Color color, float b)
        {
            color.b = b;
            return color;
        }

        /// <summary>
        /// 设置透明度
        /// </summary>
        public static Color32 SetA(this ref Color32 color, byte a)
        {
            color.a = a;
            return color;
        }

        /// <summary>
        /// 设置红色
        /// </summary>
        public static Color32 SetR(this ref Color32 color, byte r)
        {
            color.r = r;
            return color;
        }

        /// <summary>
        /// 设置绿色
        /// </summary>
        public static Color32 SetG(this ref Color32 color, byte g)
        {
            color.g = g;
            return color;
        }

        /// <summary>
        /// 设置蓝色
        /// </summary>
        public static Color32 SetB(this ref Color32 color, byte b)
        {
            color.b = b;
            return color;
        }
    }
}
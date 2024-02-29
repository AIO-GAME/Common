using System.Globalization;
using System.Text;
using UnityEngine;

namespace AIO
{
    partial class RHelper
    {
        /// <summary>
        /// 16进制 转换为 #FFFFFF
        /// </summary>
        public static string ToConvertHtmlString(this UnityEngine.Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary>
        /// 16进制 转换为 #FFFFFF
        /// </summary>
        public static string ToConvertHtmlString(this Color32 color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        /// <summary>
        /// 颜色工具类
        /// </summary>
        public static partial class Color
        {
            /// <summary>
            /// 颜色 R G B A
            /// </summary>
            public static UnityEngine.Color IntToColor(uint col)
            {
                var b = (byte)(col & 0xff);
                var g = (byte)(col >> 8 & 0xff);
                var r = (byte)(col >> 16 & 0xff);
                var a = (byte)(col >> 24 & 0xff);
                return new Color32(r, g, b, a);
            }

            /// <summary>
            /// 颜色 R G B A
            /// </summary>
            public static uint ColorToInt(in UnityEngine.Color col)
            {
                return (uint)(col.b * 255) |
                       ((uint)(col.g * 255) << 8 & 0xff00) |
                       ((uint)(col.r * 255) << 16 & 0xff0000) |
                       ((uint)(col.a * 255) << 24 & 0xff000000);
            }

            /// <summary>
            /// #FFFFFF 转换为 16进制
            /// </summary>
            public static string ToHex(uint col)
            {
                // RGBA 顺序不可改
                var cb = new StringBuilder("#");
                AHelper.Hex.ToHex((byte)(col & 0xff), cb);
                AHelper.Hex.ToHex((byte)(col >> 8 & 0xff), cb);
                AHelper.Hex.ToHex((byte)(col >> 16 & 0xff), cb);
                AHelper.Hex.ToHex((byte)(col >> 24 & 0xff), cb);
                return cb.ToString();
            }

            /// <summary>
            /// #FFFFFF 转换为 16进制
            /// </summary>
            public static string ToHex(int red, int green, int blue, int alpha)
            {
                // RGBA 顺序不可改
                var cb = new StringBuilder("#");
                AHelper.Hex.ToHex((byte)red, cb);
                AHelper.Hex.ToHex((byte)green, cb);
                AHelper.Hex.ToHex((byte)blue, cb);
                AHelper.Hex.ToHex((byte)alpha, cb);
                return cb.ToString();
            }

            /// <summary>
            /// #FFFFFF 转换为 16进制
            /// </summary>
            public static string ToHex(int red, int green, int blue)
            {
                // RGBA 顺序不可改
                var cb = new StringBuilder("#");
                AHelper.Hex.ToHex((byte)red, cb);
                AHelper.Hex.ToHex((byte)green, cb);
                AHelper.Hex.ToHex((byte)blue, cb);
                return cb.ToString();
            }

            /// <summary>
            /// hex转换到color
            /// </summary>
            public static UnityEngine.Color HexToColor(string hex)
            {
                hex = hex.TrimStart('#');
                if (string.IsNullOrEmpty(hex) || hex.Length < 6) return UnityEngine.Color.white;
                var br = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                var bg = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                var bb = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                var r = br / 255f;
                var g = bg / 255f;
                var b = bb / 255f;
                return new UnityEngine.Color(r, g, b);
            }

            /// <summary>
            /// hex转换到color32
            /// </summary>
            public static Color32 HexToColor32(string hex)
            {
                hex = hex.TrimStart('#');
                if (string.IsNullOrEmpty(hex) || hex.Length < 8) return UnityEngine.Color.white;
                var br = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                var bg = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                var bb = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                var ba = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
                return new Color32(br, bg, bb, ba);
            }
        }
    }
}
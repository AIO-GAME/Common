/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-04                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace UnityEngine
{
    public static partial class UtilsEngine
    {
        /// <summary>
        /// 颜色工具类
        /// </summary>
        public static partial class Color
        {
            /// <summary>
            /// 颜色 R G B A
            /// </summary>
            public static uint ColorToInt(in UnityEngine.Color col)
            {
                return
                    (uint)(col.b * 255) |
                    ((uint)(col.g * 255) << 8 & 0xff00) |
                    ((uint)(col.r * 255) << 16 & 0xff0000) |
                    ((uint)(col.a * 255) << 24 & 0xff000000);
            }

            /// <summary>
            /// 颜色 R G B A
            /// </summary>
            public static UnityEngine.Color NewColor(int red, int green, int blue, int alpha)
            {
                return new UnityEngine.Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
            }

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
            /// 十六进制颜色转换为颜色
            /// </summary>
            public static UnityEngine.Color HexStringToColor(string hexColorStr, UnityEngine.Color def)
            {
                if (string.IsNullOrEmpty(hexColorStr)) return def;

                int colorValue;
                try
                {
                    colorValue = Convert.ToInt32(hexColorStr, 0x10);
                }
                catch (Exception e)
                {
                    Debug.LogException(new Exception($"invalid hexColorStr: {hexColorStr}", e));
                    return UnityEngine.Color.white;
                }

                var b = (byte)(colorValue & 0xff);
                var g = (byte)(colorValue >> 8 & 0xff);
                var r = (byte)(colorValue >> 16 & 0xff);
                return new Color32(r, g, b, byte.MaxValue);
            }

            /// <summary>
            /// 颜色转为16进制
            /// </summary>
            public static string ColorToHexString(UnityEngine.Color col)
            {
                int c = (int)(col.b * 255) |
                        ((int)(col.g * 255) << 8 & 0xff00) |
                        ((int)(col.r * 255) << 16 & 0xff0000);
                return c.ToString("X6");
            }

            /// <summary>
            /// #FFFFFF 转换为 16进制
            /// </summary>
            public static string ToHex(int red, int green, int blue, int alpha)
            {
                // RGBA 顺序不可改
                var cb = new StringBuilder("#");
                global::UtilsGen.Hex.ToHex((byte)red, cb);
                global::UtilsGen.Hex.ToHex((byte)green, cb);
                global::UtilsGen.Hex.ToHex((byte)blue, cb);
                global::UtilsGen.Hex.ToHex((byte)alpha, cb);
                return cb.ToString();
            }

            /// <summary>
            /// 
            /// </summary>
            public static UnityEngine.Color ParseHtmlString(string htmlString)
            {
                ColorUtility.TryParseHtmlString(htmlString, out var color);
                return color;
            }

            /// <summary>
            /// hex转换到color
            /// </summary>
            public static UnityEngine.Color HexToColor(string hex)
            {
                if (hex == null || hex.Length < 6) return UnityEngine.Color.white;
                var br = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                var bg = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                var bb = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                //byte cc = byte.Parse(hex.Substring(6, 2), tempVar);
                var r = br / 255f;
                var g = bg / 255f;
                var b = bb / 255f;
                //float a = cc / 255f;
                return new UnityEngine.Color(r, g, b);
            }
        }
    }
}
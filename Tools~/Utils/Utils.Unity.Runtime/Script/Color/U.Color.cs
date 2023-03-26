/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-04                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

using UnityEngine;

public static partial class UtilsEngine
{
    /// <summary>
    /// 颜色工具类
    /// </summary>
    public static partial class ColorX
    {
        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ColorToInt(in Color col)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color NewColor(int red, int green, int blue, int alpha)
        {
            return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color IntToColor(uint col)
        {
            var b = (byte)(col & 0xff);
            var g = (byte)(col >> 8 & 0xff);
            var r = (byte)(col >> 16 & 0xff);
            var a = (byte)(col >> 24 & 0xff);
            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HexStringToColor(string hexColorStr, Color def)
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
                return Color.white;
            }

            var b = (byte)(colorValue & 0xff);
            var g = (byte)(colorValue >> 8 & 0xff);
            var r = (byte)(colorValue >> 16 & 0xff);
            return new Color32(r, g, b, byte.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ColorToHexString(Color col)
        {
            int c = (int)(col.b * 255) |
                    ((int)(col.g * 255) << 8 & 0xff00) |
                    ((int)(col.r * 255) << 16 & 0xff0000);
            return c.ToString("X6");
        }

        /// <summary>
        /// #FFFFFF 转换为 16进制
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHex(int red, int green, int blue, int alpha)
        {
            // RGBA 顺序不可改
            var cb = new StringBuilder("#");
            Utils.Hex.ToHex((byte)red, cb);
            Utils.Hex.ToHex((byte)green, cb);
            Utils.Hex.ToHex((byte)blue, cb);
            Utils.Hex.ToHex((byte)alpha, cb);
            return cb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ParseHtmlString(string htmlString)
        {
            ColorUtility.TryParseHtmlString(htmlString, out var color);
            return color;
        }

        /// <summary>
        /// hex转换到color
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HexToColor(string hex)
        {
            if (hex == null || hex.Length < 6) return Color.white;
            var tempVar = NumberStyles.HexNumber;
            byte br = byte.Parse(hex.Substring(0, 2), tempVar);
            byte bg = byte.Parse(hex.Substring(2, 2), tempVar);
            byte bb = byte.Parse(hex.Substring(4, 2), tempVar);
            //byte cc = byte.Parse(hex.Substring(6, 2), tempVar);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            //float a = cc / 255f;
            return new Color(r, g, b);
        }
    }
}

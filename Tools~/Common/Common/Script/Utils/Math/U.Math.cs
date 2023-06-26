/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.Collections.Generic;
using System.Linq;
using SMath = System.Math;

public partial class UtilsGen
{
    /// <summary>
    /// 数学库
    /// </summary>
    public static class Math
    {
        ///<summary>
        ///该方法将给定的整数值限制在指定的范围内，并返回新的整数值。
        ///</summary>
        ///<param name = "value" > 要限制的整数值。</param>
        ///<param name = "min" > 范围的最小值。</param>
        ///<param name = "max" > 范围的最大值。</param>
        ///<returns>被限制的整数值，如果输入值在指定范围内则返回它本身。</returns>
        ///<remarks>
        ///如果输入值小于最小值，则返回最小值；如果输入值大于最大值，则返回最大值。
        ///该方法使用 C# 中的 "in" 关键字，表示输入参数是只读引用类型。
        ///</remarks> 
        public static int Clamp(in int value, in int min, in int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        ///<summary>
        ///该方法将给定的整数值限制在指定的范围内，并返回新的整数值。
        ///</summary>
        ///<param name = "value" > 要限制的整数值。</param>
        ///<param name = "min" > 范围的最小值。</param>
        ///<param name = "max" > 范围的最大值。</param>
        ///<returns>被限制的整数值，如果输入值在指定范围内则返回它本身。</returns>
        ///<remarks>
        ///如果输入值小于最小值，则返回最小值；如果输入值大于最大值，则返回最大值。
        ///该方法使用 C# 中的 "in" 关键字，表示输入参数是只读引用类型。
        ///</remarks> 
        public static float Clamp(in float value, in float min, in float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// 判断给定的层级是否在掩码中。
        /// </summary>
        /// <param name="layer">要判断的层级</param>
        /// <param name="mask">掩码，用二进制表示哪些层级被包括</param>
        /// <returns>如果层级在掩码中，返回 true；否则返回 false。</returns>
        public static bool InMask(in int layer, in int mask)
        {
            return (1 << layer & mask) != 0;
        }

        /// <summary>
        /// 判断给定的层级是否在掩码中。
        /// </summary>
        /// <param name="layer">要判断的层级</param>
        /// <param name="mask">掩码，用二进制表示哪些层级被包括</param>
        /// <returns>如果层级在掩码中，返回 true；否则返回 false。</returns>
        public static bool InMask(in long layer, in long mask)
        {
            return ((1 << (int)layer) & (int)mask) != 0;
        }

        /// <summary>
        /// 判断给定的层级是否在掩码中。
        /// </summary>
        /// <param name="layer">要判断的层级</param>
        /// <param name="mask">掩码，用二进制表示哪些层级被包括</param>
        /// <returns>如果层级不在掩码中，返回 true；否则返回 false。</returns>
        public static bool OutMask(in int layer, in int mask)
        {
            return (1 << layer & mask) == 0;
        }

        /// <summary>
        /// 判断给定的层级是否在掩码中。
        /// </summary>
        /// <param name="layer">要判断的层级</param>
        /// <param name="mask">掩码，用二进制表示哪些层级被包括</param>
        /// <returns>如果层级不在掩码中，返回 true；否则返回 false。</returns>
        public static bool OutMask(in long layer, in long mask)
        {
            return (1 << (int)layer & (int)mask) == 0;
        }

        /// <summary>
        /// 数组百分比计算
        /// </summary>
        public static float[] ArrayPerCent(in IList<int> weights)
        {
            var r = new float[weights.Count];
            var sum = (float)weights.Sum();
            for (var i = 0; i < weights.Count; ++i)
                r[i] = weights[i] / sum;
            return r;
        }

        /// <summary>
        /// 数组百分比计算
        /// </summary>
        public static double[] ArrayPerCent(in IList<long> weights)
        {
            var r = new double[weights.Count];
            var sum = (double)weights.Sum();
            for (var i = 0; i < weights.Count; ++i)
                r[i] = weights[i] / sum;
            return r;
        }

        /// <summary>
        /// 数组百分比计算
        /// </summary>
        public static float[] ArrayPerCent(in IList<float> weights)
        {
            var r = new float[weights.Count];
            var sum = weights.Sum();
            for (var i = 0; i < weights.Count; ++i)
                r[i] = weights[i] / sum;
            return r;
        }

        /// <summary>
        /// 单位
        /// </summary>
        public const double EARTH_RADIUS = 6378137;

        /// <summary>
        /// 单位
        /// </summary>
        public const float Deg2Rad = 0.0174532924F;

        /// <summary>
        /// 单位
        /// </summary>
        public const float Rad2Deg = 57.29578F;

        /// <summary>
        /// 计算两个点之间的直线距离。
        /// </summary>
        /// <param name="x1">第一个点的 x 坐标</param>
        /// <param name="y1">第一个点的 y 坐标</param>
        /// <param name="x2">第二个点的 x 坐标</param>
        /// <param name="y2">第二个点的 y 坐标</param>
        /// <returns>两个点之间的距离</returns>
        public static float Distance(in float x1, in float y1, in float x2, in float y2)
        {
            return Abs(x1 - x2) + Abs(y1 - y2);
        }

        /// <summary>
        /// 返回给定角度的正弦值。
        /// </summary>
        /// <param name="f">给定角度，单位为弧度</param>
        /// <returns>角度的正弦值</returns>
        public static double Sin(in double f)
        {
            return SMath.Sin(f);
        }

        /// <summary>
        /// 返回给定角度的反正弦值。
        /// </summary>
        /// <param name="f">要计算反正弦值的角度，范围为 -1 到 1 之间</param>
        /// <returns>指定角度的反正弦值，返回值的单位为弧度</returns>
        public static double Asin(in double f)
        {
            return SMath.Asin(f);
        }

        /// <summary>
        /// 返回给定角度的余弦值。
        /// </summary>
        /// <param name="f">要计算余弦值的角度，单位为弧度</param>
        /// <returns>指定角度的余弦值</returns>
        public static double Cos(in double f)
        {
            return SMath.Cos(f);
        }

        /// <summary>
        /// 返回给定角度的反余弦值。
        /// </summary>
        /// <param name="f">要计算反余弦值的角度，范围为 -1 到 1 之间</param>
        /// <returns>指定角度的反余弦值，返回值的单位为弧度</returns>
        public static double Acos(in double f)
        {
            return SMath.Acos(f);
        }

        /// <summary>
        /// 返回给定值的绝对值。
        /// </summary>
        /// <param name="value">要计算绝对值的值</param>
        /// <returns>指定值的绝对值</returns>
        public static double Abs(in double value)
        {
            return SMath.Abs(value);
        }

        /// <summary>
        /// 返回给定值的绝对值。
        /// </summary>
        /// <param name="value">要计算绝对值的值</param>
        /// <returns>指定值的绝对值</returns>
        public static float Abs(in float value)
        {
            return SMath.Abs(value);
        }

        /// <summary>
        /// 返回给定值的绝对值。
        /// </summary>
        /// <param name="value">要计算绝对值的值</param>
        /// <returns>指定值的绝对值</returns>
        public static long Abs(in long value)
        {
            return SMath.Abs(value);
        }

        /// <summary>
        /// 对传入的 double 类型数值进行四舍五入操作。
        /// </summary>
        /// <param name="value">需要进行四舍五入的 double 类型数值。</param>
        /// <returns>返回四舍五入后的 double 类型数值。</returns>
        public static double Round(in double value)
        {
            return SMath.Round(value);
        }

        /// <summary>
        /// 对传入的 double 类型数值进行向上取整操作。
        /// </summary>
        /// <param name="value">需要进行向上取整的 double 类型数值。</param>
        /// <returns>返回向上取整后的 double 类型数值。</returns>
        public static double Ceil(in double value)
        {
            return SMath.Ceiling(value);
        }

        /// <summary>
        /// 对传入的 double 类型数值进行向下取整操作。
        /// </summary>
        /// <param name="value">需要进行向下取整的 double 类型数值。</param>
        /// <returns>返回向下取整后的 double 类型数值。</returns>
        public static double Floor(in double value)
        {
            return SMath.Floor(value);
        }

        /// <summary>
        /// 对传入的 int 类型数值进行模运算操作，返回非负结果。
        /// </summary>
        /// <param name="value">需要进行模运算的 int 类型数值。</param>
        /// <param name="space">模数，即取模时的除数，必须为正整数。</param>
        /// <returns>返回非负的模运算结果。</returns>
        public static int Mod(int value, in int space)
        {
            value %= space;
            if (value < 0) value += space;
            return value;
        }

        /// <summary>
        /// 计算两个 GPS 坐标点之间的距离（单位：米）。
        /// </summary>
        /// <param name="lat1">第一个坐标点的纬度。</param>
        /// <param name="lng1">第一个坐标点的经度。</param>
        /// <param name="lat2">第二个坐标点的纬度。</param>
        /// <param name="lng2">第二个坐标点的经度。</param>
        /// <returns>返回两个 GPS 坐标点之间的距离（单位：米）。</returns>
        public static double GetGPSDistance(in double lat1, in double lng1, in double lat2, in double lng2)
        {
            var radLat1 = Rad(lat1);
            var radLng1 = Rad(lng1);
            var radLat2 = Rad(lat2);
            var radLng2 = Rad(lng2);
            var a = radLat1 - radLat2;
            var b = radLng1 - radLng2;

            var v1 = SMath.Pow(SMath.Sin(a / 2), 2);
            var v2 = SMath.Cos(radLat1) * SMath.Cos(radLat2) * SMath.Pow(SMath.Sin(b / 2), 2);
            var result = 2 * SMath.Asin(SMath.Sqrt(v1 + v2)) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 将角度转换为弧度。
        /// </summary>
        /// <param name="d">需要转换的角度。</param>
        /// <returns>返回对应的弧度值。</returns>
        public static double Rad(in double d)
        {
            return d * SMath.PI / 180d;
        }

        /// <summary>
        /// 将距离值转换为字符串，单位为米或千米。
        /// </summary>
        /// <param name="value">需要转换的距离值。</param>
        /// <returns>返回对应的字符串，包含单位。</returns>
        public static string GetDistance(double value)
        {
            if (value < 0) return "0m";
            if (value > 1000)
            {
                value /= 1000;
                value = SMath.Round(value, 2);
                return value + "km";
            }

            {
                value = SMath.Round(value);
                return value + "m";
            }
        }
    }
}
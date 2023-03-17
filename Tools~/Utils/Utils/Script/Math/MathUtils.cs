/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// 
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool InMask(int layer, in int mask)
        {
            return (1 << layer & mask) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool InMask(long layer, in long mask)
        {
            return ((1 << (int)layer) & (int)mask) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OutMask(int layer, in int mask)
        {
            return (1 << layer & mask) == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OutMask(long layer, in long mask)
        {
            return (1 << (int)layer & (int)mask) == 0;
        }

        /// <summary>
        /// 数组百分比计算
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ArrayPerCent(in IList<int> weights)
        {
            var r = new float[weights.Count];
            var sum = (float)weights.Sum();
            for (int i = 0; i < weights.Count; ++i)
                r[i] = weights[i] / sum;
            return r;
        }

        /// <summary>
        /// 数组百分比计算
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] ArrayPerCent(in IList<long> weights)
        {
            var r = new double[weights.Count];
            var sum = (double)weights.Sum();
            for (int i = 0; i < weights.Count; ++i)
                r[i] = weights[i] / sum;
            return r;
        }

        /// <summary>
        /// 数组百分比计算
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ArrayPerCent(in IList<float> weights)
        {
            var r = new float[weights.Count];
            var sum = weights.Sum();
            for (int i = 0; i < weights.Count; ++i)
                r[i] = weights[i] / sum;
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        public const double EARTH_RADIUS = 6378137;

        /// <summary>
        /// 
        /// </summary>
        public const float Deg2Rad = 0.0174532924F;

        /// <summary>
        /// 
        /// </summary>
        public const float Rad2Deg = 57.29578F;

        /// <summary> 
        /// 距离
        /// </summary>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Abs(x1 - x2) + Abs(y1 - y2);
        }

        /// <summary> 
        /// 
        /// </summary>
        public static double Sin(double f)
        {
            return Math.Sin(f);
        }

        /// <summary> 
        /// 
        /// </summary>
        public static double Asin(double f)
        {
            return Math.Asin(f);
        }

        /// <summary> 
        /// 
        /// </summary>
        public static double Cos(double f)
        {
            return Math.Cos(f);
        }

        /// <summary> 
        /// 
        /// </summary>
        public static double Acos(double f)
        {
            return Math.Acos(f);
        }

        /// <summary> 
        /// 
        /// </summary>
        public static double Abs(double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static long Abs(long value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static double Round(double value)
        {
            return Math.Round(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static double Ceil(double value)
        {
            return Math.Ceiling(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static double Floor(double value)
        {
            return Math.Floor(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static int Mod(int value, int space)
        {
            value %= space;
            if (value < 0) value += space;
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static double GetGPSDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public static double Rad(double d)
        {
            return d * Math.PI / 180d;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetDistance(double value)
        {
            if (value < 0) return "0m";
            if (value > 1000)
            {
                value = value / 1000;
                value = Math.Round(value, 2);
                return value + "km";
            }
            else
            {
                value = Math.Round(value);
                return value + "m";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetDecimal(long number)
        {
            long temp = Math.Abs(number);//ȡ����ֵ
            if (temp < 100000) return number.ToString();
            if (temp >= 100000000)
                return number / 100000000 + (number / 10000000 % 100 * 0.01) + "��";//����1�ڱ��������λ
            if (temp > 10000000)
                return number / 10000 + (number / 1000 % 10 * 0.1) + "��";          //����1000W�����ǧλ
            if (temp >= 100000)
                return number / 10000 + (number / 100 % 100 * 0.01) + "��";         //����1W �������λ
            return "";
        }
    }
}
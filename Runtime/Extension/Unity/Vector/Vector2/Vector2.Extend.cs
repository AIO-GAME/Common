#region

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    partial class VectorExtend
    {
        /// <summary>
        /// Is the <see cref="Vector2.x"/> or <see cref="Vector2.y"/> NaN?
        /// </summary>
        public static bool IsNaN(this Vector2 vector)
        {
            return float.IsNaN(vector.x) ||
                   float.IsNaN(vector.y);
        }

        /// <summary>
        /// 是否约等于另一个向量
        /// </summary>
        /// <param name="sourceValue">源向量</param>
        /// <param name="targetValue">目标向量</param>
        /// <returns>是否约等于</returns>
        public static bool Approximately(this Vector2 sourceValue, Vector2 targetValue)
        {
            return sourceValue.x.Approximately(targetValue.x) && sourceValue.y.Approximately(targetValue.y);
        }

        /// <summary>
        /// 计算距离
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector2 source, in Vector2 target)
        {
            double num1 = source.x - target.x;
            double num2 = source.y - target.y;
            return (float)Math.Sqrt(num1 * num1 + num2 * num2);
        }

        /// <summary>
        /// 计算距离
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector2 source, in float x, in float y = 0)
        {
            double num1 = source.x - x;
            double num2 = source.y - y;
            return (float)Math.Sqrt(num1 * num1 + num2 * num2);
        }

        /// <summary>
        /// 点积 内积 乘积
        /// 几何意义:
        /// 1:投影
        /// 性质
        /// 1:点积可结合标量进行乘法⿺  公式:(ka)*b=a*(kb)=k(a*b)
        /// 2:点积可结合矢量加法和减法  公式:a*(b+c)=a*b+a*c
        /// 3:一个矢量和本身进行点积的结果 是该矢量的模的平方 公式:v*v=source*source+target*target+v3*v3+v.*v.=|v|²
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(this Vector2 source, in Vector2 target) { return source.x * target.x + source.y * target.y; }

        /// <summary>
        /// 点积 内积 乘积
        /// 几何意义:
        /// 1:投影
        /// 性质
        /// 1:点积可结合标量进行乘法⿺  公式:(ka)*b=a*(kb)=k(a*b)
        /// 2:点积可结合矢量加法和减法  公式:a*(b+c)=a*b+a*c
        /// 3:一个矢量和本身进行点积的结果 是该矢量的模的平方 公式:v*v=v1*v1+v2*v2+v3*v3+v.*v.=|v|²
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(this Vector2 source, in float x, in float y = 0) { return source.x * x + source.y * y; }

        /// <summary>
        /// 叉积 外积
        /// 几何意义:
        /// 1:叉积的结果会是同时垂直于这两个矢量的新矢量
        /// 性质:
        /// 1:叉积不满足交换律 a*b!=b*a
        /// 2:叉积满足反交换律 a*b==-(b*a)
        /// 3:叉积不满足结合律 (a*b)*c!=a*(b*c)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Cross(this Vector2 source, in Vector2 target) { return new Vector3(0, 0, source.x * target.y - source.y * target.x); }

        /// <summary>
        /// 叉积 外积
        /// 几何意义:
        /// 1:叉积的结果会是同时垂直于这两个矢量的新矢量
        /// 性质:
        /// 1:叉积不满足交换律 a*b!=b*a
        /// 2:叉积满足反交换律 a*b==-(b*a)
        /// 3:叉积不满足结合律 (a*b)*c!=a*(b*c)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(this Vector2 source, in float x, in float y) { return new Vector3(0, 0, source.x * y - source.y * x); }

        /// <summary>
        /// 计算角度
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(this Vector2 source, in Vector2 target) { return Vector2.Angle(source, target); }

        /// <summary>
        /// 是否约等于另一个向量
        /// </summary>
        public static Vector2Int RoundToInt(this Vector2 v)
        {
            var x = Mathf.RoundToInt(v.x);
            var y = Mathf.RoundToInt(v.y);
            return new Vector2Int(x, y);
        }
    }
}

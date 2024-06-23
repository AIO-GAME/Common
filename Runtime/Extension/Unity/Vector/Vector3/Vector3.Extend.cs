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
        /// 计算距离
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector3 source, in float x, in float y = 0, in float z = 0)
        {
            double num1 = source.x - x;
            double num2 = source.y - y;
            double num3 = source.z - z;
            return (float)Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3);
        }

        /// <summary>
        /// 计算距离
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector3 source, in Vector3 target)
        {
            double num1 = source.x - target.x;
            double num2 = source.y - target.y;
            double num3 = source.z - target.z;
            return (float)Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3);
        }

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
        public static Vector3 Cross(this Vector3 source, in float x, in float y, in float z)
        {
            return new Vector3(
                               source.y * z - source.z * y,
                               source.z * x - source.x * z,
                               source.x * y - source.y * x);
        }

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
        public static Vector3 Cross(this Vector3 source, in Vector3 target)
        {
            return new Vector3(
                               source.y * target.z - source.z * target.y,
                               source.z * target.x - source.x * target.z,
                               source.x * target.y - source.y * target.x);
        }

        //Dot > 0 方向基本相同，夹角在0°到90°之间
        //Dot < 0 方向基本相反，夹角在90°到180°之间
        //Dot = 0 正交，相互垂直

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
        public static float Dot(this Vector3 source, in float x, in float y = 0, in float z = 0) { return source.x * x + source.y * y + source.z * z; }

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
        public static float Dot(this Vector3 source, in Vector3 target) { return source.x * target.x + source.y * target.y + source.z * target.z; }

        /// <summary>
        /// Is the <see cref="Vector3.x"/> or <see cref="Vector3.y"/> or <see cref="Vector3.z"/> NaN?
        /// </summary>
        public static bool IsNaN(this Vector3 vector)
        {
            return float.IsNaN(vector.x) ||
                   float.IsNaN(vector.y) ||
                   float.IsNaN(vector.z);
        }

        /// <summary>
        /// 是否约等于另一个向量
        /// </summary>
        /// <param name="sourceValue">源向量</param>
        /// <param name="targetValue">目标向量</param>
        /// <returns>是否约等于</returns>
        public static bool Approximately(this Vector3 sourceValue, Vector3 targetValue)
        {
            return sourceValue.x.Approximately(targetValue.x) &&
                   sourceValue.y.Approximately(targetValue.y) &&
                   sourceValue.z.Approximately(targetValue.z);
        }

        /// <summary>
        /// 是否约等于另一个向量
        /// </summary>
        public static Vector3Int RoundToInt(this Vector3 v)
        {
            var x = Mathf.RoundToInt(v.x);
            var y = Mathf.RoundToInt(v.y);
            var z = Mathf.RoundToInt(v.z);
            return new Vector3Int(x, y, z);
        }
    }
}

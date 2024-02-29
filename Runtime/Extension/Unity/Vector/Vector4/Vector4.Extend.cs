using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIO.UEngine
{
    partial class VectorExtend
    {
        /// <summary>
        /// 计算距离
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector4 source, in float x, in float y = 0, in float z = 0, in float w = 0)
        {
            double num1 = source.x - x;
            double num2 = source.y - y;
            double num3 = source.z - z;
            double num4 = source.w - w;
            return (float)Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4);
        }

        /// <summary>
        /// 计算距离
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector4 source, in Vector4 target)
        {
            double num1 = source.x - target.x;
            double num2 = source.y - target.y;
            double num3 = source.z - target.z;
            double num4 = source.w - target.w;
            return (float)Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4);
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
        public static Vector4 Cross(this in Vector4 source, in Vector4 target)
        {
            return new Vector4(
                source.y * target.z - source.z * target.y,
                source.z * target.w - source.w * target.z,
                source.w * target.x - source.x * target.w,
                source.x * target.y - source.y * target.x);
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
        public static Vector4 Cross(this in Vector4 source, in float x, in float y = 0, in float z = 0, in float w = 0)
        {
            return new Vector4(
                source.y * z - source.z * y,
                source.z * w - source.w * z,
                source.w * x - source.x * w,
                source.x * y - source.y * x);
        }

        //Dot > 0 方向基本相同，夹角在0°到90°之间
        //Dot < 0 方向基本相反，夹角在90°到180°之间
        //Dot = 0 正交，相互垂直

        /// <summary>
        /// 点积 内积 乘积
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(this in Vector4 source, in Vector4 target)
        {
            return source.x * target.x + source.y * target.y + source.z * target.z + source.w * target.w;
        }

        /// <summary>
        /// 点积 内积 乘积
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(this in Vector4 source, in float x, in float y = 0, in float z = 0, in float w = 0)
        {
            return source.x * x + source.y * y + source.z * z + source.w * w;
        }

        /// <summary>
        /// Is the <see cref="Vector4.x"/> or <see cref="Vector4.y"/> or <see cref="Vector4.z"/> or <see cref="Vector4.w"/> NaN?
        /// </summary>
        public static bool IsNaN(this Vector4 vector) =>
            float.IsNaN(vector.x) ||
            float.IsNaN(vector.y) ||
            float.IsNaN(vector.z) ||
            float.IsNaN(vector.w);
    }
}
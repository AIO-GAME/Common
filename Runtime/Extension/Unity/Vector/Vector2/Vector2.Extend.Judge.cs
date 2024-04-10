#region

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 2D 方向描述 共9种 正直方向4种 斜方向4种 重叠1种
    /// -1代表向下 0代表垂直 1代表Y向上 -3代表向左 3代表向右
    /// </summary>
    public enum Direction2D
    {
        /// <summary> 左下 </summary>
        LeftDown = -4,

        /// <summary> 正左 </summary>
        Left = -3,

        /// <summary> 左上 </summary>
        LeftUp = -2,

        /// <summary> 正下 </summary>
        Down = -1,

        /// <summary> 重叠 </summary>
        Overlap = 0,

        /// <summary> 正上 </summary>
        Up = 1,

        /// <summary> 右下 </summary>
        RightDown = 2,

        /// <summary> 正右 </summary>
        Right = 3,

        /// <summary> 右上 </summary>
        RightUp = 4
    }

    partial class VectorExtend
    {
        #region Judge

        /// <summary>
        /// 角度判断 以X轴作为基准
        /// 基础公式: a·b = |a| * |b| * cosɵ
        /// 变体公式: cosɵ = ab /|a||b|
        /// 如果两向量的夹角小于指定角度的一半 则返回flase
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool JudgeAngle(this Vector2 V1, in Vector2 target, in float Value)
        {
            // 获取宽 高
            var X = V1.x - target.x;
            var Y = target.y - V1.y;
            var Cos = Y / Math.Sqrt(X * X + Y * Y);              // Cos角度
            if (Cos > 0) return Cos >= Math.Cos(90 - Value / 2); // 左半轴角度
            if (Cos < 0) return Cos <= Math.Cos(90 + Value / 2); // 右半轴角度
            return true;
        }

        /// <summary>
        /// 长度判断 如果在 0-Value范围 则返回true 否则false
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool JudgeLength(this Vector2 V1, in Vector2 target, in float Value)
        {
            return Vector2.Distance(V1, target) < Value;
        }

        /// <summary>
        /// 计算位置
        /// 含义 (点V1 在 点target 的) (正前方 or ...... ) or 重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Direction2D JudgeDirection(this Vector2 V1, in Vector2 target)
        {
            if (V1 == target) return Direction2D.Overlap;
            var Dir = (target - V1).normalized;        //位置差，方向
            var YDot = Vector2.Dot(Vector2.down, Dir); //点乘判断前后：
            var XDot = Vector2.Dot(Vector2.left, Dir); //点乘判断左右：
            // dot > 0 方向基本相同，夹角在0°到90°之间，< 0 方向基本相反，夹角在90°到180°之间 = 0 正交，相互垂直
            // 权值 -1代表向下 0代表垂直 1代表Y向上 -3代表向左 3代表向右
            var weight = 0;

            // 点乘(0,-1) 方向为上 点乘(0,1) 方向为下
            if (YDot < 0) weight      += 1;
            else if (YDot > 0) weight -= 1;

            // 点乘(-1,0) 方向为右 点乘(1,0) 方向为左
            if (XDot < 0) weight      += 3;
            else if (XDot > 0) weight -= 3;

            return (Direction2D)weight;
        }

        /// <summary>
        /// 判断方向 1:左 0:相等 -1:右
        /// 含义 (点V1 在 点target 的) (左侧 or 右侧) or (正上方 或 正下方) or 重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int JudgeRightOrLeft(this Vector2 V1, in Vector2 target)
        {
            var Dot = Vector2.Distance(Vector2.left, (target - V1).normalized);
            if (Dot < 0) return 1;
            if (Dot > 0) return -1;
            return 0;
        }

        /// <summary>
        /// 判断方向 1:上 0:相等 -1:下
        /// 含义 (点V1 在 点target 的) (上方 or 下方) or (正左侧 或 正右侧) or 重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int JudgeUpOrDown(this Vector2 V1, in Vector2 target)
        {
            var Dot = Vector2.Distance(Vector2.down, (target - V1).normalized);
            if (Dot < 0) return 1;
            if (Dot > 0) return -1;
            return 0;
        }

        #endregion
    }
}
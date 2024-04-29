#region

using System.Runtime.CompilerServices;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 3D 位置描述 共计25种描述 正直方向6种 正斜方向8种 完全斜方向8种 重叠1种
    /// </summary>
    public enum Direction3D
    {
        /// <summary> 右 上 前 </summary>   [1,56,15,-2,35,12,-5,14,9,8,105,22,7,0,-7,-22,-105,-8,-9,-14,5,-12,-35,2,-15,-56,-1]
        RightUpFront = 15,

        /// <summary> 右 上 </summary>
        RightUp = 56,

        /// <summary> 右 上 后 </summary>
        RightUpBack = 1,

        /// <summary> 右 前 </summary>
        RightFront = 12,

        /// <summary> 右 </summary>
        Right = 35,

        /// <summary> 右 后 </summary>
        RightBack = -2,

        /// <summary> 右 下 前 </summary>
        RightDownFront = 9,

        /// <summary> 右 下 </summary>
        RightDown = 14,

        /// <summary> 右 下 后 </summary>
        RightDownBack = -5,

        /// <summary> 上 前 </summary>
        UpFront = 22,

        /// <summary> 上  </summary>
        Up = 105,

        /// <summary> 上 后 </summary>
        UpBack = 8,

        /// <summary> 后 </summary>
        Back = -7,

        /// <summary> 重叠 </summary>
        Overlap = 0,

        /// <summary> 前 </summary>
        Front = 7,

        /// <summary> 下 前 </summary>
        DownFront = -8,

        /// <summary> 下 </summary>
        Down = -105,

        /// <summary> 下 后 </summary>
        DownBack = -22,

        /// <summary> 左 上 前 </summary>
        LeftUpFront = 5,

        /// <summary> 左 上 </summary>
        LeftUp = -14,

        /// <summary> 左 上 后 </summary>
        LeftUpBack = -9,

        /// <summary> 左 前 </summary>
        LeftFront = 2,

        /// <summary> 左 </summary>
        Left = -35,

        /// <summary> 左 后</summary>
        LeftBack = -12,

        /// <summary> 左 下 前 </summary>
        LeftDownFront = -1,

        /// <summary> 左 下 </summary>
        LeftDown = -56,

        /// <summary> 左 下 后 </summary>
        LeftDownBack = -15
    }

    partial class VectorExtend
    {
        /// <summary>
        /// 计算位置 End 在 Start 的位置
        /// 左手坐标系
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Direction3D JudgeDirection(this Vector3 source, in Vector3 target)
        {
            var dir = (target - source).normalized;    //位置差，方向
            var XDot = Vector3.Dot(Vector3.left, dir); //点乘判断左右
            var YDot = Vector3.Dot(Vector3.down, dir); //点乘判断上下
            var ZDot = Vector3.Dot(Vector3.back, dir); //点乘判断前后

            var weight = 0;
            if (YDot < 0) weight      += 3; //Y权值 -3代表向下 0代表垂直Y轴 3代表向上
            else if (YDot > 0) weight -= 3;

            if (XDot < 0) weight      += 5; //X权值 -5代表向左 0代表垂直X轴 5代表向右
            else if (XDot > 0) weight -= 5;
            else weight               *= 5;

            if (ZDot < 0) weight      += 7;
            else if (ZDot > 0) weight -= 7; //Z权值 -7代表向后 0代表垂直Z轴 7代表向前
            else weight               *= 7;

            return (Direction3D)weight;
        }

        /// <summary>
        /// 判断方向 1:左 0:相等 -1:右
        /// 含义 (点source 在 点target 的) (左侧 or 右侧) or ... or 重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int JudgeRightOrLeft(this Vector3 source, in Vector3 target)
        {
            var Dot = Vector3.Distance(Vector3.left, (target - source).normalized);
            if (Dot < 0) return 1;
            if (Dot > 0) return -1;
            return 0;
        }

        /// <summary>
        /// 判断方向 1:上 0:相等 -1:下
        /// 含义 (点source 在 点target 的) (上方 or 下方) or ... or 重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int JudgeUpOrDown(this Vector3 source, in Vector3 target)
        {
            var Dot = Vector3.Distance(Vector3.down, (target - source).normalized);
            if (Dot < 0) return 1;
            if (Dot > 0) return -1;
            return 0;
        }

        /// <summary>
        /// 判断方向 1:前 0:相等 -1:后
        /// 含义 (点source 在 点target 的) (前方 or 后方) or ... or 重叠
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int JudgeFrontOrBack(this Vector3 source, in Vector3 target)
        {
            var Dot = Vector3.Distance(Vector3.back, (target - source).normalized);
            if (Dot < 0) return 1;
            if (Dot > 0) return -1;
            return 0;
        }
    }
}
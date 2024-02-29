using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIO.UEngine
{
    partial class VectorExtend
    {
        /// <summary>
        /// 计算位置 End 在 Start 的位置
        /// 左手坐标系
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Direction3D JudgeDirection(this in Vector4 source, in Vector4 target)
        {
            var dir = (target - source).normalized; //位置差，方向
            var XDot = Vector4.Dot(Vector3.left, dir); //点乘判断左右
            var YDot = Vector4.Dot(Vector3.down, dir); //点乘判断上下
            var ZDot = Vector4.Dot(Vector3.back, dir); //点乘判断前后

            var weight = 0;
            if (YDot < 0) weight += 3; //Y权值 -3代表向下 0代表垂直Y轴 3代表向上
            else if (YDot > 0) weight -= 3;

            if (XDot < 0) weight += 5; //X权值 -5代表向左 0代表垂直X轴 5代表向右
            else if (XDot > 0) weight -= 5;
            else weight *= 5;

            if (ZDot < 0) weight += 7;
            else if (ZDot > 0) weight -= 7; //Z权值 -7代表向后 0代表垂直Z轴 7代表向前
            else weight *= 7;

            return (Direction3D)weight;
        }
    }
}
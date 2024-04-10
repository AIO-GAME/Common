/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

#region

using System;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    partial class TransformExtend
    {
        /// <summary>
        /// 获取Transform的位置
        /// </summary>
        /// <param name="target">目标</param>
        /// <returns>位置</returns>
        public static Location GetLocation(this Transform target)
        {
            if (target == null) return Location.Null;
            var location = new Location
            {
                Position = target.localPosition,
                Rotation = target.localEulerAngles,
                Scale    = target.localScale
            };
            return location;
        }

        /// <summary>
        /// 设置Transform的位置
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="location">位置</param>
        public static void SetLocation(this Transform target, Location location)
        {
            if (target == null || location == Location.Null) return;
            target.localPosition    = location.Position;
            target.localEulerAngles = location.Rotation;
            target.localScale       = location.Scale;
        }
    }

    /// <summary>
    /// 物体位置（包含局部坐标、局部旋转、局部缩放）
    /// </summary>
    [Serializable]
    public struct Location : IEquatable<Location>
    {
        /// <summary>
        /// 局部坐标
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// 局部旋转
        /// </summary>
        public Vector3 Rotation;

        /// <summary>
        /// 局部缩放
        /// </summary>
        public Vector3 Scale;

        public Location(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale    = scale;
        }

        /// <summary>
        /// 是否为零
        /// </summary>
        public bool IsZero => Position == Vector3.zero && Rotation == Vector3.zero && Scale == Vector3.one;

        /// <summary>
        /// 是否为零
        /// </summary>
        public bool IsNull => Position == Vector3.zero && Rotation == Vector3.zero && Scale == Vector3.zero;

        public static Location Zero => new Location(Vector3.zero, Vector3.zero, Vector3.one);

        public static Location Null => new Location(Vector3.zero, Vector3.zero, Vector3.zero);

        public bool Equals(Location other)
        {
            return Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale);
        }

        public override bool Equals(object obj)
        {
            return obj is Location other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Rotation.GetHashCode();
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// 判断两个 Location 是否相等
        /// </summary>
        public static bool operator ==(Location a, Location b)
        {
            return a.Position == b.Position && a.Rotation == b.Rotation && a.Scale == b.Scale;
        }

        /// <summary>
        /// 判断两个 Location 是否不相等
        /// </summary>
        public static bool operator !=(Location a, Location b)
        {
            return !(a == b);
        }
    }
}
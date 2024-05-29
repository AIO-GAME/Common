#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 类成员场景处理器特性
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    public abstract class SceneHandlerAttribute : Attribute { }

    /// <summary>
    /// 移动手柄处理器（支持 Vector2、Vector3 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class MoveHandlerAttribute : SceneHandlerAttribute
    {
        /// <summary>
        /// 移动手柄处理器（支持 Vector2、Vector3 类型）
        /// </summary>
        /// <param name="display">显示名称</param>
        public MoveHandlerAttribute(string display = null)
        {
            Display = display;
        }

        public string Display { get; private set; }
    }

    /// <summary>
    /// 半径手柄处理器（支持 float、int 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class RadiusHandlerAttribute : SceneHandlerAttribute
    {
        /// <summary>
        /// 半径手柄处理器（支持 float、int 类型）
        /// </summary>
        /// <param name="display">显示名称</param>
        public RadiusHandlerAttribute(string display = null)
        {
            Display = display;
        }

        public string Display { get; private set; }
    }

    /// <summary>
    /// 包围盒处理器（支持 Bounds 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class BoundsHandlerAttribute : SceneHandlerAttribute
    {
        /// <summary>
        /// 包围盒处理器（支持 Bounds 类型）
        /// </summary>
        /// <param name="display">显示名称</param>
        public BoundsHandlerAttribute(string display = null)
        {
            Display = display;
        }

        public string Display { get; private set; }
    }

    /// <summary>
    /// 方向处理器（支持 Vector2、Vector3 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class DirectionHandlerAttribute : SceneHandlerAttribute
    {
        /// <summary>
        /// 方向处理器（支持 Vector2、Vector3 类型）
        /// </summary>
        /// <param name="isDynamic">是否动态模式</param>
        public DirectionHandlerAttribute(bool isDynamic = false)
        {
            IsDynamic = isDynamic;
        }

        public bool IsDynamic { get; private set; }
    }

    /// <summary>
    /// 圆形区域处理器（支持 float 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class CircleAreaHandlerAttribute : SceneHandlerAttribute
    {
        #region Axis enum

        public enum Axis
        {
            X,
            Y,
            Z
        }

        #endregion

        /// <summary>
        /// 圆形区域处理器（支持 float 类型）
        /// </summary>
        /// <param name="direction">方向</param>
        /// <param name="isDynamic">是否动态模式</param>
        public CircleAreaHandlerAttribute(Axis direction = Axis.Y, bool isDynamic = false)
        {
            Direction = direction;
            IsDynamic = isDynamic;
        }

        public Axis Direction { get; private set; }
        public bool IsDynamic { get; private set; }
    }
}
#region

using System;
using System.Diagnostics;

#endregion

namespace AIO
{
    /// <summary>
    /// 锁定 Transform 组件，仅可标记 HTBehaviour 的子类，挂载后将锁定目标 GameObject 的 Transform 组件，禁止在检视面板修改属性值
    /// </summary>
    [AttributeUsage(AttributeTargets.Class), Conditional("UNITY_EDITOR")]
    public sealed class LockTransformAttribute : Attribute
    {
        /// <summary>
        /// 锁定 Transform 组件，仅可标记 HTBehaviour 的子类，挂载后将锁定目标 GameObject 的 Transform 组件，禁止在检视面板修改属性值
        /// </summary>
        /// <param name="isLockPosition">锁定 Position</param>
        /// <param name="isLockRotation">锁定 Rotation</param>
        /// <param name="isLockScale">锁定 Scale</param>
        public LockTransformAttribute(bool isLockPosition = true, bool isLockRotation = true, bool isLockScale = true)
        {
            IsLockPosition = isLockPosition;
            IsLockRotation = isLockRotation;
            IsLockScale    = isLockScale;
        }

        public bool IsLockPosition { get; private set; }
        public bool IsLockRotation { get; private set; }
        public bool IsLockScale    { get; private set; }
    }
}
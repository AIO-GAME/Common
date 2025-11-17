/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using UnityEngine;

namespace AIO
{
    /// <summary>
    /// 这个动画任务用于在指定时间内将一个Transform的本地缩放从初始值插值到目标值。
    /// </summary>
    [Obsolete("这是一个测试脚本，仅用于演示如何实现IAnimationJob接口。请勿在生产环境中使用。", true)]
    [HelpURL("https://gist.github.com/yasirkula/86cf0b8cce094fbb93e97913eeda225b")]
    public class ScaleObjectJobTest : IAnimationJob
    {
        private Transform transform;
        private Vector3   initialScale;
        private Vector3   targetScale;
        private float     t, tMultiplier;

        public void Initialize(Transform transform, Vector3 targetScale, float duration)
        {
            this.transform   = transform;
            this.targetScale = targetScale;
            initialScale     = transform.localScale;

            t           = 0f;
            tMultiplier = 1f / duration;
        }

        // Should return true while the animation is running, false when the animation is finished
        public bool Execute(float deltaTime)
        {
            t += deltaTime * tMultiplier;
            if (t < 1f)
            {
                transform.localScale = Vector3.LerpUnclamped(initialScale, targetScale, t);
                return true;
            }

            transform.localScale = targetScale;
            return false;
        }

        // Should return true if the animation is animating the "animatedObject",
        // or simply return false if AnimationSystem.StopAnimation(animatedObject) won't be used
        public bool CheckAnimatedObject(object animatedObject) { return ReferenceEquals(transform, animatedObject); }

        // Should return true if the animated object is still alive; invalid animations will
        // automatically be stopped when active Scene changes
        public bool IsValid() { return transform; }

        // Should clear any object references here
        public void Clear() { transform = null; }
    }
}
using System.Linq;
using UnityEngine;

namespace AIO.UEngine
{
    /// <summary>
    /// Renderer 扩展
    /// </summary>
    public static class RendererExtend
    {
        /// <summary>
        /// 计算模型的中心点坐标
        /// </summary>
        public static Vector3 GetMeshFilterCenter<T>(this T trans) where T : Renderer
        {
            return new Bounds(trans.bounds.center, Vector3.zero).center;
        }

        /// <summary>
        /// 计算模型的中心点坐标
        /// </summary>
        public static Vector3 GetMeshFilterCenter<T>(this T[] trans) where T : Renderer
        {
            if (trans == null || trans.Length == 0) return Vector3.zero;
            var center = trans.Aggregate(Vector3.zero, (current, item) => current + current);
            center /= trans.Length;
            return new Bounds(center, Vector2.zero).center;
        }
    }
}
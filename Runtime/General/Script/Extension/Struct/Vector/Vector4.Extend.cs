using UnityEngine;

namespace UnityEngine
{
    /// <summary>
    /// Vector4 扩展
    /// </summary>
    public static class Vector4Extend
    {
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
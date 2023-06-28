using UnityEngine;

namespace UnityEngine
{
    /// <summary>
    /// Vector3 扩展
    /// </summary>
    public static class Vector3Extend
    {
        /// <summary>
        /// Is the <see cref="Vector3.x"/> or <see cref="Vector3.y"/> or <see cref="Vector3.z"/> NaN?
        /// </summary>
        public static bool IsNaN(this Vector3 vector) =>
            float.IsNaN(vector.x) ||
            float.IsNaN(vector.y) ||
            float.IsNaN(vector.z);
    }
}
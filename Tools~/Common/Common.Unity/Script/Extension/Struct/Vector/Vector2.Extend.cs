using UnityEngine;

namespace AIO
{
    /// <summary>
    /// Vector2 扩展
    /// </summary>
    public static class Vector2Extend
    {
        /// <summary>
        /// Is the <see cref="Vector2.x"/> or <see cref="Vector2.y"/> NaN?
        /// </summary>
        public static bool IsNaN(this Vector2 vector) =>
            float.IsNaN(vector.x) ||
            float.IsNaN(vector.y);
    }
}
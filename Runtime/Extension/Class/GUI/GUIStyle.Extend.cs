using UnityEngine;

namespace AIO
{
    /// <summary>
    /// GUI Style 扩展
    /// </summary>
    public static class GUIStyleExtend
    {
        /// <summary>
        /// An error message for when something has been modified after being released to the pool.
        /// 它们必须在被释放到池中之前被清除, 之后不能被修改.
        /// </summary>
        private const string NotClearError = " They must be cleared before being released to the pool and not modified after that.";

        /// <summary> [AIO Extension]
        /// Calls <see cref="GUIStyle.CalcMinMaxWidth"/> and returns the max width.
        /// </summary>
        public static float CalculateWidth(this GUIStyle style, GUIContent content)
        {
            style.CalcMinMaxWidth(content, out _, out var width);
            return Mathf.Ceil(width);
        }

        /// <summary>
        /// Calls <see cref="GUIStyle.CalcMinMaxWidth"/> and returns the max width.
        /// </summary>
        public static float CalculateWidth(this GUIStyle style, string text)
        {
            var content = new GUIContent(text);
            var width = style.CalculateWidth(content);
            content.text = null;
            return width;
        }

        /// <summary>
        /// Calls <see cref="GUIStyle.CalcMinMaxWidth"/> and returns the max width.
        /// </summary>
        public static float CalculateWidth(this GUIStyle style, Texture image)
        {
            var content = new GUIContent(image);
            var width = style.CalculateWidth(content);
            content.image = null;
            return width;
        }

        /// <summary>
        /// Calls <see cref="GUIStyle.CalcMinMaxWidth"/> and returns the max width.
        /// </summary>
        public static float CalculateWidth(this GUIStyle style, string text, string tooltip)
        {
            var content = new GUIContent(text, tooltip);
            var width = style.CalculateWidth(content);
            content.text = null;
            content.tooltip = null;
            return width;
        }
    }
}

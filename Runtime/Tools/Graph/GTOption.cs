/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    /// <summary>
    /// GUI 选项
    /// </summary>
    public static class GTOption
    {
        /// <summary>
        /// 宽度 固定值
        /// </summary>
        public static GUILayoutOption Width(in float value)
        {
            return GUILayout.Width(value);
        }

        /// <summary>
        /// 宽度 固定值
        /// </summary>
        public static GUILayoutOption Width(in int value)
        {
            return GUILayout.Width(value);
        }

        /// <summary>
        /// 宽度 固定值
        /// </summary>
        public static GUILayoutOption Width(in bool value)
        {
            return GUILayout.ExpandWidth(value);
        }

        /// <summary>
        /// 宽度 自动伸缩
        /// </summary>
        public static GUILayoutOption WidthExpand(in bool value)
        {
            return GUILayout.ExpandWidth(value);
        }

        /// <summary>
        /// 宽度 高度 自动伸缩
        /// </summary>
        public static GUILayoutOption[] WidthHeightExpand(in bool wValue = true, in bool hValue = true)
        {
            return new GUILayoutOption[] { GUILayout.ExpandWidth(wValue), GUILayout.ExpandHeight(hValue) };
        }

        /// <summary>
        /// 宽度 最小
        /// </summary>
        public static GUILayoutOption WidthMin(in float value)
        {
            return GUILayout.MinWidth(value);
        }

        /// <summary>
        /// 宽度 最大
        /// </summary>
        public static GUILayoutOption WidthMax(in float value)
        {
            return GUILayout.MaxHeight(value);
        }

        /// <summary>
        /// 高度 固定
        /// </summary>
        public static GUILayoutOption Height(in float value)
        {
            return GUILayout.Height(value);
        }

        /// <summary>
        /// 高度 固定
        /// </summary>
        public static GUILayoutOption Height(in bool value)
        {
            return GUILayout.ExpandHeight(value);
        }

        /// <summary>
        /// 高度 自动伸缩
        /// </summary>
        public static GUILayoutOption HeightExpand(in bool value)
        {
            return GUILayout.ExpandHeight(value);
        }

        /// <summary>
        /// 高度 最小
        /// </summary>
        public static GUILayoutOption HeightMin(in float value)
        {
            return GUILayout.MinHeight(value);
        }

        /// <summary>
        /// 高度 最大
        /// </summary>
        public static GUILayoutOption HeightMax(in float value)
        {
            return GUILayout.MaxHeight(value);
        }

    }
}

#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO
{
    /// <summary>
    /// GUI 选项
    /// </summary>
    public static class GTOption
    {
        private enum Type
        {
            Width,
            MinWidth,
            MaxWidth,
            Height,
            MinHeight,
            MaxHeight,
        }

        private static readonly Dictionary<Type, Dictionary<float, GUILayoutOption>> Table = new Dictionary<Type, Dictionary<float, GUILayoutOption>>(8);

        private static readonly GUILayoutOption StretchWidth_True   = GUILayout.ExpandWidth(true);
        private static readonly GUILayoutOption StretchWidth_False  = GUILayout.ExpandWidth(false);
        private static readonly GUILayoutOption StretchHeight_True  = GUILayout.ExpandHeight(true);
        private static readonly GUILayoutOption StretchHeight_False = GUILayout.ExpandHeight(false);

        /// <summary>
        /// 宽度 固定值
        /// </summary>
        public static GUILayoutOption Width(in float value)
        {
            if (!Table.TryGetValue(Type.Width, out var dic))
            {
                Table.Add(Type.Width, dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.Width(value) } });
            }

            if (!dic.TryGetValue(value, out var option))
            {
                dic[value] = option = GUILayout.Width(value);
            }

            return option;
        }

        /// <summary>
        /// 宽度 固定值
        /// </summary>
        public static GUILayoutOption Width(in int value)
        {
            if (!Table.TryGetValue(Type.Width, out var dic))
                Table[Type.Width] = dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.Width(value) } };

            if (!dic.TryGetValue(value, out var option))
                dic[value] = option = GUILayout.Width(value);

            return option;
        }

        /// <summary>
        /// 宽度 固定值
        /// </summary>
        public static GUILayoutOption Width(in bool value)
        {
            return value ? StretchWidth_True : StretchWidth_False;
        }

        /// <summary>
        /// 宽度 自动伸缩
        /// </summary>
        public static GUILayoutOption WidthExpand(in bool value)
        {
            return value ? StretchWidth_True : StretchWidth_False;
        }

        /// <summary>
        /// 宽度 高度 自动伸缩
        /// </summary>
        public static GUILayoutOption[] WidthHeightExpand(in bool wValue = true, in bool hValue = true)
        {
            return new[]
            {
                wValue ? StretchWidth_True : StretchWidth_False,
                hValue ? StretchHeight_True : StretchHeight_False
            };
        }

        /// <summary>
        /// 宽度 最小
        /// </summary>
        public static GUILayoutOption WidthMin(in float value)
        {
            if (!Table.TryGetValue(Type.MinWidth, out var dic))
                Table[Type.MinWidth] = dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.MinWidth(value) } };

            if (!dic.TryGetValue(value, out var option))
                dic[value] = option = GUILayout.Width(value);

            return option;
        }

        /// <summary>
        /// 宽度 最大
        /// </summary>
        public static GUILayoutOption WidthMax(in float value)
        {
            if (!Table.TryGetValue(Type.MaxWidth, out var dic))
                Table[Type.MaxWidth] = dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.MaxWidth(value) } };

            if (!dic.TryGetValue(value, out var option))
                dic[value] = option = GUILayout.Width(value);

            return option;
        }

        /// <summary>
        /// 高度 固定
        /// </summary>
        public static GUILayoutOption Height(in float value)
        {
            if (!Table.TryGetValue(Type.Height, out var dic))
                Table[Type.Height] = dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.Height(value) } };

            if (!dic.TryGetValue(value, out var option))
                dic[value] = option = GUILayout.Width(value);

            return option;
        }

        /// <summary>
        /// 高度 固定
        /// </summary>
        public static GUILayoutOption Height(in bool value)
        {
            return value ? StretchHeight_True : StretchHeight_False;
        }

        /// <summary>
        /// 高度 自动伸缩
        /// </summary>
        public static GUILayoutOption HeightExpand(in bool value)
        {
            return value ? StretchHeight_True : StretchHeight_False;
        }

        /// <summary>
        /// 高度 最小
        /// </summary>
        public static GUILayoutOption HeightMin(in float value)
        {
            if (!Table.TryGetValue(Type.MinHeight, out var dic))
                Table[Type.MinHeight] = dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.MinHeight(value) } };

            if (!dic.TryGetValue(value, out var option))
                dic[value] = option = GUILayout.Width(value);

            return option;
        }

        /// <summary>
        /// 高度 最大
        /// </summary>
        public static GUILayoutOption HeightMax(in float value)
        {
            if (!Table.TryGetValue(Type.MaxHeight, out var dic))
                Table[Type.MaxHeight] = dic = new Dictionary<float, GUILayoutOption>(8) { { value, GUILayout.MaxHeight(value) } };

            if (!dic.TryGetValue(value, out var option))
                dic[value] = option = GUILayout.Width(value);

            return option;
        }
    }
}
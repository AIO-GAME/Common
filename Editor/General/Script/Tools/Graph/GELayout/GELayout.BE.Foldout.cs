/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;
using UDefaultValue = UnityEngine.Internal.DefaultValueAttribute;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region 折页排版内容

#if UNITY_2019_1_OR_NEWER
        /// <summary> 折页排版内容 </summary>
        /// <param name="foldout">开关</param>
        /// <param name="content">标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="menuAction">操作菜单</param>
        /// <param name="menuIcon">菜单ICON显示风格</param>
        public static bool FoldoutHeaderGroupBegin(bool foldout, string content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, content, style, menuAction, menuIcon);
            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

        public static void FoldoutHeaderGroupEnd()
        {
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        /// <summary> 折页排版内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="foldout">开关</param>
        /// <param name="content">标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="menuAction">操作菜单</param>
        /// <param name="menuIcon">菜单ICON显示风格</param>
        public static bool FoldoutHeaderGroup(Action action, bool foldout, string content,
            GUIStyle style = null, Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, content, style, menuAction, menuIcon);
            if (foldout) action?.Invoke();
            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

        /// <summary> 折页排版内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="foldout">开关</param>
        /// <param name="content">标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="menuAction">操作菜单</param>
        /// <param name="menuIcon">菜单ICON显示风格</param>
        public static bool FoldoutHeaderGroup(Action action,
            bool foldout,
            GUIContent content,
            [UDefaultValue("EditorStyles.foldoutHeader")]
            GUIStyle style = null,
            Action<Rect> menuAction = null, GUIStyle menuIcon = null)
        {
            if (action == null) return false;
            Space();
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, content, style, menuAction, menuIcon);
            if (foldout)
            {
                action();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

#elif UNITY_2018_1_OR_NEWER
            /// <summary> 折页排版内容 </summary>
            /// <param name="action">方法体</param>
            /// <param name="foldout">开关</param>
            /// <param name="content">标签</param>
            /// <param name="style">显示风格</param>
            public static bool FoldoutHeaderGroup(Action action, bool foldout, string content, GUIStyle style = null)
            {
                if (action == null) return false;
#if UNITY_2019_1_OR_NEWER
                foldout = ToggleLeft(content, foldout, style ?? "FoldoutHeader", GTOption.WidthExpand(true));
#else
                foldout = ToggleLeft(content, foldout, style ?? "GUIEditor.BreadcrumbLeft", GTOption.WidthExpand(true));
#endif
                Space();
                if (foldout) action?.Invoke();
                return foldout;
            }

            /// <summary> 折页排版内容 </summary>
            /// <param name="action">方法体</param>
            /// <param name="foldout">开关</param>
            /// <param name="content">标签</param>
            /// <param name="style">显示风格</param>
            /// <param name="menuAction">操作菜单</param>
            /// <param name="menuIcon">菜单ICON显示风格</param>
            public static bool FoldoutHeaderGroup(Action action, bool foldout, GUIContent content, [UDefaultValue("EditorStyles.foldoutHeader")] GUIStyle style
 = null)
            {
                if (action == null) return false;
                Space();
#if UNITY_2019_1_OR_NEWER
                foldout = Toggle(content, foldout, style ?? "FoldoutHeader");
#else
                foldout = Toggle(content, foldout, style ?? "GUIEditor.BreadcrumbLeft", GTOption.WidthExpand(true));
#endif
                if (foldout) action?.Invoke();
                return foldout;
            }
#endif
        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary> 折页排版内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="foldout">开关</param>
        /// <param name="content">标签</param>
        /// <param name="style">显示风格</param>
        public static bool FoldoutHeaderToggle(Action action, bool foldout, string content, GUIStyle style = null)
        {
            if (action == null) return false;
#if UNITY_2019_1_OR_NEWER
            foldout = ToggleLeft(content, foldout, style ?? "FoldoutHeader", GTOption.WidthExpand(true));
#else
                foldout = ToggleLeft(content, foldout, style ?? "GUIEditor.BreadcrumbLeft", GTOption.WidthExpand(true));
#endif
            Space();
            if (foldout) action();
            return foldout;
        }

        /// <summary> 折页排版内容 </summary>
        /// <param name="action">方法体</param>
        /// <param name="foldout">开关</param>
        /// <param name="content">标签</param>
        /// <param name="style">显示风格</param>
        public static bool FoldoutHeaderToggle(Action action, bool foldout, GUIContent content,
            [UDefaultValue("EditorStyles.foldoutHeader")] GUIStyle style = null)
        {
            if (action == null) return false;
            Space();
#if UNITY_2019_1_OR_NEWER
            foldout = Toggle(content, foldout, style ?? "FoldoutHeader");
#else
                foldout = Toggle(content, foldout, style ?? "GUIEditor.BreadcrumbLeft");
#endif
            if (foldout) action();
            return foldout;
        }

        #endregion

        #region 折叠式箭头 Foldout

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <param name="style">显示风格</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        public static bool Foldout(string content, bool foldout, [UDefaultValue("EditorStyles.foldout")] GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, content, style);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        [ExcludeFromDocs] //从文档中排除
        public static bool Foldout(string content, bool foldout)
        {
            return EditorGUILayout.Foldout(foldout, content);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <param name="style">显示风格</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        public static bool Foldout(GUIContent content, bool foldout,
            [UDefaultValue("EditorStyles.foldout")] GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, content, style);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        [ExcludeFromDocs]
        public static bool Foldout(GUIContent content, bool foldout)
        {
            return EditorGUILayout.Foldout(foldout, content);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        public static bool Foldout(GUIContent content, bool foldout, bool toggleOnLabelClick,
            [UDefaultValue("EditorStyles.foldout")] GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick, style);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        [ExcludeFromDocs]
        public static bool Foldout(GUIContent content, bool foldout, bool toggleOnLabelClick)
        {
            return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        public static bool Foldout(string content, bool foldout, bool toggleOnLabelClick,
            [UDefaultValue("EditorStyles.foldout")] GUIStyle style)
        {
            return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick, style);
        }

        /// <summary> 折叠式箭头 </summary>
        /// <param name="foldout">显示的折叠状态</param>
        /// <param name="content">显示的标签</param>
        /// <param name="toggleOnLabelClick">是否在单击标签时切换折叠状态</param>
        /// <returns>true:呈现子对象,false:隐藏</returns>
        [ExcludeFromDocs]
        public static bool Foldout(string content, bool foldout, bool toggleOnLabelClick)
        {
            return EditorGUILayout.Foldout(foldout, content, toggleOnLabelClick);
        }

        #endregion
    }
}

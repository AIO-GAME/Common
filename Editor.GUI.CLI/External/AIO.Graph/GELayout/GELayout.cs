using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    [IgnoreConsoleJump]
    public static partial class GELayout
    {
        #region List

        /// <summary>
        /// 绘制 List 列表
        /// </summary>
        /// <param name="label">标签 <see cref="string"/></param>
        /// <param name="array">显示的折叠状态 <see cref="IList&lt;T&gt;"/></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool"/></param>
        /// <param name="tips">提示信息 <see cref="Action"/></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action"/></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;"/></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle"/></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle"/></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool"/></returns>
        private static bool FieldList<T>(GTContent label,
            IList<T> array, bool foldout, Action tips, Action<int> showFunc, Func<T> addFunc,
            GUIStyle labelStyle, GUIStyle bgStyle)
        {
            if (showFunc is null)
            {
                EditorGUILayout.HelpBox("(call back / add func) action is null", MessageType.Error, true);
                return false;
            }

            if (array is null) array = new List<T>();
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label.Content, bgStyle);
            if (foldout)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label.Content, labelStyle);
                if (addFunc != null)
                {
                    if (GUILayout.Button("+", GUILayout.Width(20))) array.Add(addFunc.Invoke());
                }

                EditorGUILayout.EndHorizontal();

                if (tips != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    tips.Invoke();
                    EditorGUILayout.EndHorizontal();
                }

                for (var i = array.Count - 1; i >= 0; i--)
                {
                    var i1 = array.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField((i1 + 1).ToString("00"), GUILayout.Width(20));
                    showFunc.Invoke(i1);
                    if (GUILayout.Button("-", GUILayout.Width(20))) array.RemoveAt(i1);
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

        private static void FieldList<T>(GTContent label,
            IList<T> array, Action tips, Action<int> showFunc, Func<T> addFunc,
            GUIStyle labelStyle, GUIStyle bgStyle)
        {
            if (showFunc is null)
            {
                EditorGUILayout.HelpBox("(call back / add func) action is null", MessageType.Error, true);
                return;
            }

            if (array is null) array = new List<T>();
            EditorGUILayout.BeginVertical(bgStyle ?? GUIStyle.none);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle ?? GUIStyle.none);
            if (addFunc != null)
            {
                if (GUILayout.Button("+", GUILayout.Width(20))) array.Add(addFunc.Invoke());
            }

            EditorGUILayout.EndHorizontal();

            if (tips != null)
            {
                EditorGUILayout.BeginHorizontal();
                tips.Invoke();
                EditorGUILayout.EndHorizontal();
            }

            for (var i = array.Count - 1; i >= 0; i--)
            {
                var i1 = array.Count - 1 - i;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i1 + 1).ToString("00"), GUILayout.Width(20));
                showFunc.Invoke(i1);
                if (GUILayout.Button("-", GUILayout.Width(20))) array.RemoveAt(i1);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region 隔行

        /// <summary>
        /// 分隔符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Separator(int num)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Separator();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Space()
        {
            EditorGUILayout.Space();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Space(float width)
        {
            EditorGUILayout.Space(width);
        }

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 隔行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Space(int num, float width, bool expand = true)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Space(width, expand);
        }

#endif

        #endregion

        private static Color HeaderBorderColor
        {
            get
            {
                var shade = EditorGUIUtility.isProSkin ? 0.12f : 0.6f;
                return new Color(shade, shade, shade, 1);
            }
        }

        private static Color HeaderNormalColor
        {
            get
            {
                var shade = EditorGUIUtility.isProSkin ? 62f / 255f : 205f / 255f;
                return new Color(shade, shade, shade, 1);
            }
        }

        private static Color HeaderHoverColor
        {
            get
            {
                var shade = EditorGUIUtility.isProSkin ? 70f / 255f : 215f / 255f;
                return new Color(shade, shade, shade, 1);
            }
        }
    }
}
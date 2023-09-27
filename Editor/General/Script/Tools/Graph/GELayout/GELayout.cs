/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(string name, float height, float width, string content, GUIStyle style = null)
        {
            if (GUILayout.Button(name, style, GUILayout.Height(height), GUILayout.Width(width)))
                GEHelper.CopyTextAction(content);
        }

        #region Path

        /// <summary>  
        /// 绘制字段 string 路径
        /// </summary>
        /// <param name="label">标题</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="tips">弹窗提示</param>
        /// <param name="defaultName">默认名称</param>
        public static string Path(
            string label,
            string value,
            string tips = "Please select the path",
            string defaultName = "")
        {
            return Path(new GUIContent(label), value, tips, defaultName);
        }

        /// <summary>  
        /// 绘制字段 string 路径
        /// </summary>
        /// <param name="label">标题</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="tips">弹窗提示</param>
        /// <param name="defaultName">默认名称</param>
        public static string Path(
            Texture label,
            string value,
            string tips = "Please select the path",
            string defaultName = "")
        {
            return Path(new GUIContent(label), value, tips, defaultName);
        }

        /// <summary>  
        /// 绘制字段 string 路径
        /// </summary>
        /// <param name="label">标题</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="tips">弹窗提示</param>
        /// <param name="defaultName">默认名称</param>
        /// <param name="width">宽度</param>
        public static string Path(
            GUIContent label,
            string value,
            string tips = "Please select the path",
            string defaultName = "", float width = 50)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label);
            if (GUILayout.Button("Select", GUILayout.Width(width)))
                value = EditorUtility.OpenFolderPanel(tips, value, defaultName);
            if (GUILayout.Button("Open", GUILayout.Width(width)))
                PrPlatform.Open.Path(value).Async();
            EditorGUILayout.EndHorizontal();
            return value;
        }

        #endregion

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
            if (showFunc is null || addFunc is null)
            {
                EditorGUILayout.HelpBox("(call back / add func) action is null", MessageType.Error, true);
                return false;
            }

            if (array is null) array = new List<T>();
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label, bgStyle);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle);
            if (GUILayout.Button("+", GUILayout.Width(20))) array.Add(addFunc.Invoke());
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

            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

        private static void FieldList<T>(GTContent label,
            IList<T> array, Action tips, Action<int> showFunc, Func<T> addFunc,
            GUIStyle labelStyle, GUIStyle bgStyle)
        {
            if (addFunc is null)
            {
                EditorGUILayout.HelpBox("(call back / add func) action is null", MessageType.Error, true);
                return;
            }

            if (array is null) array = new List<T>();
            EditorGUILayout.BeginVertical(bgStyle);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle);
            if (GUILayout.Button("+", GUILayout.Width(20))) array.Add(addFunc.Invoke());
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
        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        public static void Separator(int num)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Separator();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space()
        {
            EditorGUILayout.Space();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public new static void Space(float width)
        {
            EditorGUILayout.Space(width);
        }

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(int num, float width, bool expand = true)
        {
            for (var i = 0; i < num; i++) EditorGUILayout.Space(width, expand);
        }

#endif

        #endregion
    }
}
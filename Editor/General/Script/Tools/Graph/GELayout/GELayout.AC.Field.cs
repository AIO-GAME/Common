/*|✩ - - - - - |||
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    public partial class GELayout
    {

        #region 物体文本框 FieldObject

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="allowSceneObjects">允许场景对象</param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(T value, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="allowSceneObjects">允许场景对象</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(string label, T value, bool allowSceneObjects, params GUILayoutOption[] options)
            where T : Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="allowSceneObjects">允许场景对象</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(GUIContent label, T value, bool allowSceneObjects, params GUILayoutOption[] options)
            where T : Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects, options);
        }

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="type">Object Type</param>
        /// <param name="allowSceneObjects">允许场景对象</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(T value, bool allowSceneObjects, Type type, params GUILayoutOption[] options)
            where T : Object
        {
            return (T)EditorGUILayout.ObjectField(value, type, allowSceneObjects, options);
        }

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="type">Object Type</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(T value, Type type, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(value, type, true, options);
        }

        #endregion


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
        public static string Path(
            GUIContent label,
            string value,
            string tips = "Please select the path",
            string defaultName = "")
        {
            Horizontal(() =>
            {
                Label(label);
                if (Button("Select", GUILayout.Width(50)))
                    value = EditorUtility.OpenFolderPanel(tips, value, defaultName);
                if (string.IsNullOrEmpty(value)) return;
                if (Button("Open", GUILayout.Width(50)))
                    PrPlatform.Open.Path(value).Async();
            });
            return value;
        }

        #endregion

        #region 最小最大滑动条

        /// <summary> 最小最大滑动条 </summary>
        /// <param name="minValue">滑动条最左边的值</param>
        /// <param name="maxValue">滑动条最右边的值</param>
        /// <param name="minLimit">限制滑动条最左边的值</param>
        /// <param name="maxLimit">限制滑动条最右边的值</param>
        /// <param name="options">排版格式</param>
        public static void Slider(ref float minValue, ref float maxValue, float minLimit, float maxLimit,
            params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        /// <summary> 最小最大滑动条 </summary>
        /// <param name="label">标签</param>
        /// <param name="minValue">滑动条最左边的值</param>
        /// <param name="maxValue">滑动条最右边的值</param>
        /// <param name="minLimit">限制滑动条最左边的值</param>
        /// <param name="maxLimit">限制滑动条最右边的值</param>
        /// <param name="options">排版格式</param>
        public static void Slider(string label, ref float minValue, ref float maxValue, float minLimit,
            float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        /// <summary> 最小最大滑动条 </summary>
        /// <param name="label">标签</param>
        /// <param name="minValue">滑动条最左边的值</param>
        /// <param name="maxValue">滑动条最右边的值</param>
        /// <param name="minLimit">限制滑动条最左边的值</param>
        /// <param name="maxLimit">限制滑动条最右边的值</param>
        /// <param name="options">排版格式</param>
        public static void Slider(GUIContent label, ref float minValue, ref float maxValue, float minLimit,
            float maxLimit, params GUILayoutOption[] options)
        {
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit, options);
        }

        #endregion
    }
}
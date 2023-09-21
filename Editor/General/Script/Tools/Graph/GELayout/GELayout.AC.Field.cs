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
        /// <summary>
        /// 绘制字段 float
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float Field(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 Int
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Field(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 Double
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static double Field(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DoubleField(value, style, options);
        }

        /// <summary>
        /// 绘制字段 FieldLong
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static long Field(long value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LongField(value, style, options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="ranges">范围</param>
        /// <param name="options">排版格式</param>
        /// <param name="color">颜色</param>
        public static AnimationCurve Field(AnimationCurve value, Color color, Rect ranges,
            params GUILayoutOption[] options)
        {
            return EditorGUILayout.CurveField(value, color, ranges, options);
        }


        /// <summary> 延迟文本 string </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedTextField(value, style, options);
        }

        /// <summary> 延迟文本 double </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(double value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedDoubleField(value, style, options);
        }

        /// <summary> 延迟文本 float </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(float value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedFloatField(value, style, options);
        }

        /// <summary> 延迟文本 int </summary>
        /// <param name="value">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(int value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.DelayedIntField(value, style, options);
        }

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
        /// <param name="allowSceneObjects">允许场景对象</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options)
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

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        /// <param name="style">风格</param>
        public static string Field(string value, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(value, style, options);
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

        /// <summary>
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static string Password(string password, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(password, style, options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static int Layer(int layer, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(layer, style, options);
        }
    }
}
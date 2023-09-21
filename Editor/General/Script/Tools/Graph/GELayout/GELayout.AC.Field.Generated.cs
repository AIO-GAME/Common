
/*|✩ - - - - - |||
|||✩ Date:     ||| -> Automatic Generate
|||✩ Document: ||| ->
|||✩ - - - - - |*/
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{

    #region AnimationCurve : EditorGUILayout.CurveField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(string label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, color, ranges,options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(GUIContent label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(Texture label, AnimationCurve value, Color color, Rect ranges, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, color, ranges, options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(AnimationCurve value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(value, options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(string label, AnimationCurve value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(GUIContent label, AnimationCurve value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 AnimationCurve
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="AnimationCurve"/> </param>
        /// <param name="options">排版格式</param>
        public static AnimationCurve Field(Texture label, AnimationCurve value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.CurveField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Color : EditorGUILayout.ColorField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(string label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, showEyedropper, showAlpha, hdr,options);
        }

        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(GUIContent label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(label, value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(Texture label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(Color value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(value, options);
        }

        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(string label, Color value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(GUIContent label, Color value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Color
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Color"/> </param>
        /// <param name="options">排版格式</param>
        public static Color Field(Texture label, Color value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.ColorField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Gradient : EditorGUILayout.GradientField

    public partial class GELayout 
    {

#if UNITY_2019_1_OR_NEWER

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(string label, Gradient value, bool hdr, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, hdr,options);
        }

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(GUIContent label, Gradient value, bool hdr, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(label, value, hdr, options);
        }

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(Texture label, Gradient value, bool hdr, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, hdr, options);
        }


#endif

#if UNITY_2019_1_OR_NEWER

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(Gradient value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(value, options);
        }

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(string label, Gradient value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(GUIContent label, Gradient value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(label, value,  options);
        }

        /// <summary>  
        /// 绘制渐变字段 Gradient
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Gradient"/> </param>
        /// <param name="options">排版格式</param>
        public static Gradient Field(Texture label, Gradient value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.GradientField(new GUIContent(label), value,  options);
        }


#endif
    }

    #endregion

    #region string : EditorGUILayout.TextField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(Texture label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(value, options);
        }

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(GUIContent label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Field(Texture label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.TextField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region T : EditorGUILayout.ObjectField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(string label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(new GUIContent(label), value, type, allowSceneObjects,options);
        }

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(GUIContent label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(label, value, type, allowSceneObjects, options);
        }

        /// <summary>  
        /// 绘制 Object 字段 T
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="UnityEngine.Object"/> </param>
        /// <param name="options">排版格式</param>
        public static T Field<T>(Texture label, T value, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            return (T)EditorGUILayout.ObjectField(new GUIContent(label), value, type, allowSceneObjects, options);
        }

    }

    #endregion

    #region int : EditorGUILayout.LayerField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(string label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(Texture label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(value, options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(string label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(GUIContent label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(label, value,  options);
        }

        /// <summary>  
        /// 绘制 Layer 字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Layer(Texture label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LayerField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region string : EditorGUILayout.PasswordField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(string label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(Texture label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(value, options);
        }

        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(string label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(GUIContent label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(label, value,  options);
        }

        /// <summary>  
        /// 绘制 密码文本框 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string Password(Texture label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.PasswordField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region float : EditorGUILayout.FloatField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(string label, float value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(Texture label, float value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(value, options);
        }

        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(string label, float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(GUIContent label, float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float Field(Texture label, float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.FloatField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region int : EditorGUILayout.IntField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(string label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(Texture label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(value, options);
        }

        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(string label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(GUIContent label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int Field(Texture label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.IntField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region double : EditorGUILayout.DoubleField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(string label, double value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(Texture label, double value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(value, options);
        }

        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(string label, double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(GUIContent label, double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double Field(Texture label, double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DoubleField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region long : EditorGUILayout.LongField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(string label, long value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(GUIContent label, long value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(Texture label, long value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(long value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(value, options);
        }

        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(string label, long value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(GUIContent label, long value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 long
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="long"/> </param>
        /// <param name="options">排版格式</param>
        public static long Field(Texture label, long value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.LongField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region float : EditorGUILayout.DelayedFloatField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(string label, float value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(GUIContent label, float value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(Texture label, float value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(string label, float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(GUIContent label, float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(label, value,  options);
        }

        /// <summary>  
        /// 绘制延迟字段 float
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="float"/> </param>
        /// <param name="options">排版格式</param>
        public static float FieldDelayed(Texture label, float value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedFloatField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region int : EditorGUILayout.DelayedIntField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(string label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(GUIContent label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(Texture label, int value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(string label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(GUIContent label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(label, value,  options);
        }

        /// <summary>  
        /// 绘制延迟字段 int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="int"/> </param>
        /// <param name="options">排版格式</param>
        public static int FieldDelayed(Texture label, int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedIntField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region double : EditorGUILayout.DelayedDoubleField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(string label, double value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(GUIContent label, double value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(Texture label, double value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(string label, double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(GUIContent label, double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(label, value,  options);
        }

        /// <summary>  
        /// 绘制延迟字段 double
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="double"/> </param>
        /// <param name="options">排版格式</param>
        public static double FieldDelayed(Texture label, double value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedDoubleField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region string : EditorGUILayout.DelayedTextField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(string label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, style,options);
        }

        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(GUIContent label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(label, value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(Texture label, string value, GUIStyle style, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, style, options);
        }

        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(string label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(GUIContent label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(label, value,  options);
        }

        /// <summary>  
        /// 绘制延迟字段 string
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="options">排版格式</param>
        public static string FieldDelayed(Texture label, string value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.DelayedTextField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Bounds : EditorGUILayout.BoundsField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="value">值 <see cref="Bounds"/> </param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(Bounds value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsField(value, options);
        }

        /// <summary>  
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Bounds"/> </param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(string label, Bounds value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Bounds"/> </param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(GUIContent label, Bounds value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Bounds
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Bounds"/> </param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(Texture label, Bounds value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region BoundsInt : EditorGUILayout.BoundsIntField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="value">值 <see cref="BoundsInt"/> </param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(BoundsInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsIntField(value, options);
        }

        /// <summary>  
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="BoundsInt"/> </param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(string label, BoundsInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsIntField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="BoundsInt"/> </param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(GUIContent label, BoundsInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsIntField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 BoundsInt
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="BoundsInt"/> </param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(Texture label, BoundsInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.BoundsIntField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region RectInt : EditorGUILayout.RectIntField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="value">值 <see cref="RectInt"/> </param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(RectInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectIntField(value, options);
        }

        /// <summary>  
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="RectInt"/> </param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(string label, RectInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectIntField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="RectInt"/> </param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(GUIContent label, RectInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectIntField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 RectInt
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="RectInt"/> </param>
        /// <param name="options">排版格式</param>
        public static RectInt Field(Texture label, RectInt value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectIntField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Rect : EditorGUILayout.RectField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="value">值 <see cref="Rect"/> </param>
        /// <param name="options">排版格式</param>
        public static Rect Field(Rect value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectField(value, options);
        }

        /// <summary>  
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Rect"/> </param>
        /// <param name="options">排版格式</param>
        public static Rect Field(string label, Rect value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Rect"/> </param>
        /// <param name="options">排版格式</param>
        public static Rect Field(GUIContent label, Rect value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Rect
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Rect"/> </param>
        /// <param name="options">排版格式</param>
        public static Rect Field(Texture label, Rect value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.RectField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Vector2 : EditorGUILayout.Vector2Field

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Vector2
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector2"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector2 Field(string label, Vector2 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector2Field(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Vector2
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector2"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector2 Field(GUIContent label, Vector2 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector2Field(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Vector2
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector2"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector2 Field(Texture label, Vector2 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector2Field(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Vector2Int : EditorGUILayout.Vector2IntField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Vector2Int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector2Int"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector2Int Field(string label, Vector2Int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector2IntField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Vector2Int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector2Int"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector2Int Field(GUIContent label, Vector2Int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector2IntField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Vector2Int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector2Int"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector2Int Field(Texture label, Vector2Int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector2IntField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Vector3 : EditorGUILayout.Vector3Field

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Vector3
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector3"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector3 Field(string label, Vector3 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector3Field(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Vector3
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector3"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector3 Field(GUIContent label, Vector3 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector3Field(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Vector3
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector3"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector3 Field(Texture label, Vector3 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector3Field(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Vector3Int : EditorGUILayout.Vector3IntField

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Vector3Int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector3Int"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector3Int Field(string label, Vector3Int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector3IntField(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Vector3Int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector3Int"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector3Int Field(GUIContent label, Vector3Int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector3IntField(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Vector3Int
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector3Int"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector3Int Field(Texture label, Vector3Int value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector3IntField(new GUIContent(label), value,  options);
        }

    }

    #endregion

    #region Vector4 : EditorGUILayout.Vector4Field

    public partial class GELayout 
    {
        /// <summary>  
        /// 绘制字段 Vector4
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector4"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector4 Field(string label, Vector4 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector4Field(new GUIContent(label), value, options);
        }

        /// <summary>  
        /// 绘制字段 Vector4
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector4"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector4 Field(GUIContent label, Vector4 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector4Field(label, value,  options);
        }

        /// <summary>  
        /// 绘制字段 Vector4
        /// </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值 <see cref="Vector4"/> </param>
        /// <param name="options">排版格式</param>
        public static Vector4 Field(Texture label, Vector4 value, params GUILayoutOption[] options) 
        {
            return EditorGUILayout.Vector4Field(new GUIContent(label), value,  options);
        }

    }

    #endregion
}

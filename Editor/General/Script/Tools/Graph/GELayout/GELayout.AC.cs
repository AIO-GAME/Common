/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout:GULayout
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
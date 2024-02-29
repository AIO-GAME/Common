using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary>  
        /// 绘制字段 string 路径
        /// </summary>
        /// <param name="label">标题</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="tips">弹窗提示</param>
        /// <param name="defaultName">默认名称</param>
        /// <param name="width">宽度</param>
        public static string Path(
            string label,
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

        /// <summary>  
        /// 绘制字段 string 路径
        /// </summary>
        /// <param name="label">标题</param>
        /// <param name="value">值 <see cref="string"/> </param>
        /// <param name="tips">弹窗提示</param>
        /// <param name="defaultName">默认名称</param>
        /// <param name="width">宽度</param>
        public static string Path(
            GTContent label,
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
    }
}
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public interface IGUIContentInfo
    {
        /// <summary>
        /// 工具名称
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// 工具使用的内置图标 Unity内置图标
        /// </summary>
        public string IconBuiltin { get; set; }

        /// <summary>
        /// 相对路径图标 使用 <see cref="UnityEditor.AssetDatabase.LoadAssetAtPath"/> 加载
        /// </summary>
        /// Assets/..
        /// Packages/..
        public string IconRelative { get; set; }

        /// <summary>
        /// 资源路径图标 使用 Resources.Load 加载
        /// </summary>
        public string IconResource { get; set; }

        /// <summary>
        /// 工具使用的自定义标题
        /// </summary>
        public string Text { get; set; }
    }

    public static class GUIContentInfoExtend
    {
        public static GUIContent SetContent(this IGUIContentInfo attribute, MethodInfo method)
        {
            GUIContent Temp = null;
            if (!string.IsNullOrEmpty(attribute.IconBuiltin))
            {
                Temp      = EditorGUIUtility.IconContent(attribute.IconBuiltin);
                Temp.text = string.Empty;
            }
            else if (!string.IsNullOrEmpty(attribute.IconRelative))
            {
                Temp = new GUIContent
                {
                    image = AssetDatabase.LoadAssetAtPath<Texture2D>(attribute.@IconRelative.Replace("/", "\\"))
                };
            }
            else if (!string.IsNullOrEmpty(attribute.IconResource))
            {
                if (attribute.IconResource.StartsWith("Packages/") || attribute.IconResource.StartsWith("Assets/"))
                {
                    Temp = new GUIContent { image = AssetDatabase.LoadAssetAtPath<Texture2D>(attribute.@IconResource.Replace("/", "\\")) };
                }
                else
                {
                    Temp = new GUIContent { image = Resources.Load<Texture2D>(attribute.IconResource) };
                }
            }

            if (!Temp?.image)
                Temp = new GUIContent
                {
                    text    = string.IsNullOrEmpty(attribute.Text) ? method.Name : attribute.Text,
                    tooltip = attribute.Tooltip
                };
            else Temp.tooltip = attribute.Tooltip;

            return Temp;
        }
    }
}
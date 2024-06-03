#region

using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    // [ScriptIcon(IconResource = "Editor/Icon/Color/general")]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GWindowAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// 组
        /// </summary>
        public string Group;

        /// <summary>
        /// 最大高度
        /// </summary>
        public uint MaxSizeHeight = 0;

        /// <summary>
        /// 最大宽度
        /// </summary>
        public uint MaxSizeWidth = 0;

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Menu;

        /// <summary>
        /// 菜单顺序
        /// </summary>
        public int MenuPriority;

        /// <summary>
        /// 菜单验证函数 返回值为bool
        /// </summary>
        public MethodInfo MenuValidate;

        /// <summary>
        /// 最小高度
        /// </summary>
        public uint MinSizeHeight = 0;

        /// <summary>
        /// 最小宽度
        /// </summary>
        public uint MinSizeWidth = 0;

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order;

        public string[] Dock;

        /// <summary>
        /// 运行时 窗口类型
        /// </summary>
        public Type RuntimeType;

        /// <inheritdoc />
        public GWindowAttribute(string title, string tooltip = "", [CallerFilePath] string filePath = "") : base(title)
        {
            Title = new GUIContent
            {
                text    = title,
                tooltip = tooltip
            };
            FilePath = filePath.StartsWith(@".\Packages\") ? filePath.Substring(2) : filePath.Replace('\\', '/');
            Dock     = Array.Empty<string>();
        }

        /// <summary>
        /// 标题
        /// </summary>
        public GUIContent Title { get; private set; }

        /// <summary>
        /// 相对路径图标 使用 AssetDatabase.LoadAssetAtPath 加载
        /// </summary>
        public string IconRelative { get; set; }

        /// <summary>
        /// 资源路径图标 使用 Resources.Load 加载
        /// </summary>
        public string IconResource { get; set; }

        /// <summary>
        /// 最大宽高
        /// </summary>
        public Vector2 MaxSize => new Vector2(MaxSizeWidth, MaxSizeHeight);

        /// <summary>
        /// 最小宽高
        /// </summary>
        public Vector2 MinSize => new Vector2(MinSizeWidth, MinSizeHeight);

        public string FilePath { get; private set; }

        public GUIContent GetTitle()
        {
            if (!string.IsNullOrEmpty(IconRelative))
                Title.image                                           = AssetDatabase.LoadAssetAtPath<Texture2D>(IconRelative);
            else if (!string.IsNullOrEmpty(IconResource)) Title.image = Resources.Load<Texture2D>(IconResource);

            return Title;
        }

        public Texture2D GetTexture2D()
        {
            if (!string.IsNullOrEmpty(IconRelative)) return AssetDatabase.LoadAssetAtPath<Texture2D>(IconRelative);
            return !string.IsNullOrEmpty(IconResource) ? Resources.Load<Texture2D>(IconResource) : null;
        }
    }
}
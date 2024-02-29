using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Gitee链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GiteeUrlAttribute : UrlAttribute
    {
        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "Gitee",
                image = GEContent.NewApp("Gitee").image
            };
        }

        public GiteeUrlAttribute(string url) : base(url)
        {
        }
    }
}
#region

using System;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// Gitee链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class GiteeUrlAttribute : UrlAttribute
    {
        public GiteeUrlAttribute(string url) : base(url) { }

        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "Gitee",
                image   = GEContent.NewApp("Gitee").image
            };
        }
    }
}
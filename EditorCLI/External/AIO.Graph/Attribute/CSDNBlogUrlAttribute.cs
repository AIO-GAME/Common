#region

using System;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// CSDN 博客链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class CSDNBlogUrlAttribute : UrlAttribute
    {
        public CSDNBlogUrlAttribute(string url) : base(url) { }

        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "CSDN",
                image   = GEContent.NewApp("CSDN").image
            };
        }
    }
}
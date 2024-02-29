using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// CSDN 博客链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CSDNBlogUrlAttribute : UrlAttribute
    {
        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "CSDN",
                image = GEContent.NewApp("CSDN").image
            };
        }

        public CSDNBlogUrlAttribute(string url) : base(url)
        {
        }
    }
}
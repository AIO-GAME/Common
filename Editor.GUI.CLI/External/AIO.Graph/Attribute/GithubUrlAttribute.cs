using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Github链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GithubUrlAttribute : UrlAttribute
    {
        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "Github",
                image = GEContent.NewApp("Github-mark-white").image
            };
        }

        public GithubUrlAttribute(string url) : base(url)
        {
        }
    }
}
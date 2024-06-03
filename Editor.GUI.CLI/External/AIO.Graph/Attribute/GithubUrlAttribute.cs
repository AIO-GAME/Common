#region

using System;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// Github链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class GithubUrlAttribute : UrlAttribute
    {
        public GithubUrlAttribute(string url) : base(url) { }

        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "Github",
                image   = GEContent.NewApp("Github-mark-white").image
            };
        }
    }
}
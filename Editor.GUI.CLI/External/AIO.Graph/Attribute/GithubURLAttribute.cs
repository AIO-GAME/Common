/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Github链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GithubURLAttribute : URLAttribute
    {
        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "Github",
                image = GEContent.NewApp("Github-mark-white").image
            };
        }

        public GithubURLAttribute(string url) : base(url)
        {
        }
    }
}
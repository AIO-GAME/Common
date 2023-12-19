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
    /// Gitee链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GiteeURLAttribute : URLAttribute
    {
        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "Gitee",
                image = GEContent.NewApp("Gitee").image
            };
        }

        public GiteeURLAttribute(string url) : base(url)
        {
        }
    }
}
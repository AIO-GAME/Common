/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// CSDN博客链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CSDNBlogURLAttribute : URLAttribute
    {
        public override void OnEnable()
        {
            Content = new GUIContent
            {
                tooltip = "CSDN",
                image = GEContent.NewApp("CSDN").image
            };
        }

        public CSDNBlogURLAttribute(string url) : base(url)
        {
        }
    }
}
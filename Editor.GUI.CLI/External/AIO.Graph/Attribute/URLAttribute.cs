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
    /// CSDN博客链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public abstract class URLAttribute : Attribute
    {
        /// <summary>
        /// 链接
        /// </summary>
        public string URL { get; private set; }

        public GUIContent Content { get; protected set; }

        public abstract void OnEnable();

        private URLAttribute()
        {
        }

        protected URLAttribute(string url)
        {
            URL = url;
        }
    }
}
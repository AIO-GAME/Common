using System;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// CSDN博客链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public abstract class UrlAttribute : Attribute
    {
        /// <summary>
        /// 链接
        /// </summary>
        public string URL { get; private set; }

        public GUIContent Content { get; protected set; }

        public abstract void OnEnable();

        private UrlAttribute()
        {
        }

        protected UrlAttribute(string url)
        {
            URL = url;
        }
    }
}
#region

using System;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// CSDN博客链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public abstract class UrlAttribute : Attribute
    {
        private UrlAttribute() { }

        protected UrlAttribute(string url)
        {
            URL = url;
        }

        /// <summary>
        /// 链接
        /// </summary>
        public string URL { get; private set; }

        public GUIContent Content { get; protected set; }

        public abstract void OnEnable();
    }
}
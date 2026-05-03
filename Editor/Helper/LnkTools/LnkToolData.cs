using System;
using System.Reflection;
using UnityEngine;

namespace AIO.UEditor
{
    public class LnkToolData : IDisposable
    {
        public GUIContent Content { get; private set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor;

        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForegroundColor;

        /// <summary>
        /// 快捷工具触发模式
        /// </summary>
        public ELnkToolsMode RuntimeMode;

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority;

        /// <summary>
        /// 方法
        /// </summary>
        internal MethodInfo Method;

        /// <summary>
        /// 显示模式
        /// </summary>
        public ELnkShowMode ShowMode = ELnkShowMode.SceneView;

        internal object Target;

        public LnkToolData(GUIContent content, Action action)
        {
            Content = content;
            Method  = action.Method;
        }

        public LnkToolData(GUIContent content, Action<bool> action)
        {
            Content = content;
            Method  = action.Method;
        }

        public LnkToolData(GUIContent content, object target, Action action)
        {
            Content = content;
            Method  = action.Method;
            Target  = target;
        }

        public LnkToolData(GUIContent content, object target, Action<bool> action)
        {
            Content = content;
            Method  = action.Method;
            Target  = target;
        }

        public void Dispose()
        {
            Target  = null;
            Method  = null;
            Content = null;
        }
    }
}
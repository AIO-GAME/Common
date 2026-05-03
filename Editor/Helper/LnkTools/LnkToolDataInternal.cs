#region

using System;
using System.Reflection;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    internal class LnkToolDataInternal : IDisposable
    {
        public LnkToolDataInternal(LnkToolData data)
        {
            Content         = data.Content;
            ForegroundColor = data.ForegroundColor;
            RuntimeMode     = data.RuntimeMode;
            Priority        = data.Priority;
            ShowMode        = data.ShowMode;
            Method          = data.Method;
            Target          = data.Target;
            if (Method is null) throw new NullReferenceException(nameof(Method));
        }

        public LnkToolDataInternal(MethodInfo method)
        {
            var data = method.GetCustomAttribute<LnkToolsAttribute>();

            Content = data.SetContent(method);
            ForegroundColor = ColorUtility.TryParseHtmlString(data.ForegroundColor, out var color1)
                ? color1
                : Color.white;
            BackgroundColor = ColorUtility.TryParseHtmlString(data.BackgroundColor, out var color2)
                ? color2
                : new Color(0.3592f, 0.3592f, 0.3592f); //#616161
            RuntimeMode = data.RuntimeMode;
            Priority    = data.Priority;
            ShowMode    = data.ShowMode;
            Method      = method;
            Target      = null;
            if (Method is null) throw new NullReferenceException(nameof(Method));
        }

        public GUIContent Content { get; private set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; }

        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForegroundColor { get; }

        /// <summary>
        /// 快捷工具触发模式
        /// </summary>
        public ELnkToolsMode RuntimeMode { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// 显示模式
        /// </summary>
        public ELnkShowMode ShowMode { get; set; } = ELnkShowMode.SceneView;

        public bool HasIcon => Content.image != null;

        public bool HasReturnBool => Method.ReturnType == typeof(bool);

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool Status { get; private set; }

        private object     Target;
        private MethodInfo Method;

        public void Invoke()
        {
            if (HasReturnBool)
                Status = (bool)Method.Invoke(Target, null);
            else
                Method.Invoke(Target, null);
        }

        public void Dispose()
        {
            Content = null;
            Target  = null;
            Method  = null;
            GC.SuppressFinalize(this);
        }
    }
}
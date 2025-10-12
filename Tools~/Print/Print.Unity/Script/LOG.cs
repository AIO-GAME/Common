using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Scripting;
using Debug = UnityEngine.Debug;

namespace AIO
{
    /// <summary>
    /// 日志
    /// </summary>
    [Preserve]
    public class LOG
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string TAG { get; }

        /// <summary>
        /// 是否开启日志输出
        /// </summary>
        public bool Enabled
        {
            get => _enabled?.Invoke() ?? enabled;
            set => enabled = value;
        }

        /// <summary>
        /// 是否开启日志输出
        /// </summary>
        private bool enabled;

        /// <summary>
        /// 日志颜色
        /// </summary>
        public string COLOR_LOG { get; set; } = "#B3E5FC";

        /// <summary>
        /// 异常颜色
        /// </summary>
        public string COLOR_EXCEPTION { get; set; } = "#E91E63";

        /// <summary>
        /// 警告颜色
        /// </summary>
        public string COLOR_WARNING { get; set; } = "#FFC107";

        /// <summary>
        /// 错误颜色
        /// </summary>
        public string COLOR_ERROR { get; set; } = "#F44336";

        /// <param name="tag"> 标签 </param>
        public LOG(string tag) { TAG = tag; }

        /// <param name="tag"> 标签 </param>
        /// <param name="enable"> 是否开启日志输出 </param>
        public LOG(string tag, bool enable)
        {
            TAG     = tag;
            enabled = enable;
        }

        /// <param name="tag"> 标签 </param>
        /// <param name="enable"> 是否开启日志输出 </param>
        public LOG(string tag, Func<bool> enable)
        {
            TAG      = tag;
            _enabled = enable;
        }

        private Func<bool> _enabled;

        #region LogWarning

        /// <summary>
        /// <para>Logs a formatted warning message to the Unity Console.</para>
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void W(string format, params object[] args)
        {
            if (Enabled) Debug.unityLogger.LogFormat(LogType.Warning, $"{TAG} <color={COLOR_WARNING}>{format}</color>", args);
        }

        /// <summary>
        /// <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void W(string message)
        {
            if (Enabled) Debug.unityLogger.Log(LogType.Warning, $"{TAG} <color={COLOR_WARNING}>{message}</color>");
        }

        #endregion

        #region LogException

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void Exception(Exception exception)
        {
            if (Enabled)
                Debug.unityLogger.Log(LogType.Exception, new Exception(TAG, exception));
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void Exception(string exception)
        {
            if (Enabled)
                Debug.unityLogger.Log(LogType.Exception, new Exception($"{TAG} <color={COLOR_EXCEPTION}>{exception}</color>"));
        }

        #endregion

        #region Log

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void I(string message)
        {
            if (Enabled)
                Debug.unityLogger.Log(LogType.Log, $"{TAG} <color={COLOR_LOG}>{message}</color>");
        }

        /// <summary>
        ///     <para>Logs a formatted message to the Unity Console.</para>
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void I(string format, params object[] args)
        {
            if (Enabled)
                Debug.unityLogger.LogFormat(LogType.Log, $"{TAG} <color={COLOR_LOG}>{format}</color>", args);
        }

        #endregion

        #region LogError

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void E(string message)
        {
            if (Enabled)
                Debug.unityLogger.Log(LogType.Error, $"{TAG} <color={COLOR_ERROR}>{message}</color>");
        }

        /// <summary>
        ///     <para>Logs a formatted error message to the Unity console.</para>
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void E(string format, params object[] args)
        {
            if (Enabled)
                Debug.unityLogger.LogFormat(LogType.Error, $"{TAG} <color={COLOR_ERROR}>{format}</color>", args);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [DebuggerHidden, DebuggerNonUserCode, Preserve]
        public void E(Exception message)
        {
            if (Enabled)
                Debug.unityLogger.Log(LogType.Error, $"{TAG} <color={COLOR_ERROR}>{message.Message}</color>");
        }

        #endregion
    }
}
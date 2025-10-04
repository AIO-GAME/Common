#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using UnityEditor;

#endregion

namespace AIO.UEditor
{
    [DebuggerNonUserCode, IgnoreConsoleJump, Description("Unity Console Editor : 日志输出开关")]
    internal static class UnityConsoleEditor
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        private const string MENU_DEVELOPER_MODE = "AIO/Debug/Developer Mode";

        /// <summary>
        /// 错误日志
        /// </summary>
        private const string MENU_EDITOR_SWITCH_LOG = "AIO/Debug/Console Log";

        /// <summary>
        /// 开发者模式
        /// </summary>
        private const string MENU_EDITOR_SWITCH_ERROR = "AIO/Debug/Console Error";

        #region MethodInfo

        private static Lazy<MethodInfo> EnabledError = new Lazy<MethodInfo>(() =>
        {
            MethodInfo info = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity", StringComparison.CurrentCultureIgnoreCase)) continue;
                var type = assembly.GetType("UnityEngine.UnityConsole");
                if (type is null) continue;
                info = type.GetMethod(nameof(EnabledError), Flags);
                if (info != null) break;
            }

            return info;
        });

        private static Lazy<MethodInfo> DisableError = new Lazy<MethodInfo>(() =>
        {
            MethodInfo info = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity", StringComparison.CurrentCultureIgnoreCase)) continue;
                var type = assembly.GetType("UnityEngine.UnityConsole");
                if (type is null) continue;
                info = type.GetMethod(nameof(DisableError), Flags);
                if (info != null) break;
            }

            return info;
        });

        private static Lazy<MethodInfo> EnabledLog = new Lazy<MethodInfo>(() =>
        {
            MethodInfo info = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity", StringComparison.CurrentCultureIgnoreCase)) continue;
                var type = assembly.GetType("UnityEngine.UnityConsole");
                if (type is null) continue;
                info = type.GetMethod(nameof(EnabledLog), Flags);
                if (info != null) break;
            }

            return info;
        });

        private static Lazy<MethodInfo> DisableLog = new Lazy<MethodInfo>(() =>
        {
            MethodInfo info = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity", StringComparison.CurrentCultureIgnoreCase)) continue;
                var type = assembly.GetType("UnityEngine.UnityConsole");
                if (type is null) continue;
                info = type.GetMethod(nameof(DisableLog), Flags);
                if (info != null) break;
            }

            return info;
        });

        private static Lazy<MethodInfo> RefreshSettings = new Lazy<MethodInfo>(() => typeof(AssetDatabase).GetMethod("RefreshSettings", BindingFlags.Static | BindingFlags.Public));

        private static BindingFlags Flags => BindingFlags.Static | BindingFlags.NonPublic;

        #endregion

        private static bool IS_EDITOR_SWITCH_LOG   = EditorPrefs.GetBool(MENU_EDITOR_SWITCH_LOG, false);
        private static bool IS_EDITOR_SWITCH_ERROR = EditorPrefs.GetBool(MENU_EDITOR_SWITCH_ERROR, false);
        private static bool IS_DEVELOPER_MODE      = EditorPrefs.GetBool(MENU_DEVELOPER_MODE, false);

        [LnkTools(Tooltip = "支持读取 Console.WriteLine 日志", IconResource = "Editor/Icon/Color/-message")]
        private static bool EditorSwitchLog()
        {
            EditorSwitchLOG();
            return IS_EDITOR_SWITCH_LOG;
        }

        [MenuItem(MENU_EDITOR_SWITCH_LOG)]
        private static void EditorSwitchLOG()
        {
            IS_EDITOR_SWITCH_LOG = !IS_EDITOR_SWITCH_LOG;
            if (IS_EDITOR_SWITCH_LOG) LogEnabled();
            else LogDisabled();
            EditorPrefs.SetBool(MENU_EDITOR_SWITCH_LOG, IS_EDITOR_SWITCH_LOG);
            Menu.SetChecked(MENU_EDITOR_SWITCH_LOG, IS_EDITOR_SWITCH_LOG);
            if (RefreshSettings == null) AssetDatabase.Refresh();
            else RefreshSettings.Value.Invoke(null, null);
        }

        [MenuItem(MENU_EDITOR_SWITCH_ERROR)]
        private static void EditorSwitchERROR()
        {
            IS_EDITOR_SWITCH_ERROR = !IS_EDITOR_SWITCH_ERROR;
            if (IS_EDITOR_SWITCH_ERROR) ErrorEnabled();
            else ErrorDisabled();
            EditorPrefs.SetBool(MENU_EDITOR_SWITCH_ERROR, IS_EDITOR_SWITCH_ERROR);
            Menu.SetChecked(MENU_EDITOR_SWITCH_ERROR, IS_EDITOR_SWITCH_ERROR);
            if (RefreshSettings == null) AssetDatabase.Refresh();
            else RefreshSettings.Value.Invoke(null, null);
        }

        /// <summary>
        /// 打开开发者模式
        /// </summary>
        [MenuItem(MENU_DEVELOPER_MODE, false, 0)]
        private static void OpenDeveloperMode()
        {
            IS_DEVELOPER_MODE = !IS_DEVELOPER_MODE;
            EditorPrefs.SetBool("DeveloperMode", IS_DEVELOPER_MODE);
            EditorPrefs.SetBool(MENU_DEVELOPER_MODE, IS_DEVELOPER_MODE);
        }

        [AInit(EInitAttrMode.Both, int.MinValue)]
        private static void Initialize()
        {
            if (IS_EDITOR_SWITCH_LOG) LogEnabled();
            else LogDisabled();
            if (IS_EDITOR_SWITCH_ERROR) ErrorEnabled();
            else ErrorDisabled();
            MenuRefresh();
        }

        private static void MenuRefresh()
        {
            Menu.SetChecked(MENU_EDITOR_SWITCH_ERROR, IS_EDITOR_SWITCH_ERROR);
            Menu.SetChecked(MENU_EDITOR_SWITCH_LOG, IS_EDITOR_SWITCH_LOG);
            Menu.SetChecked(MENU_DEVELOPER_MODE, IS_DEVELOPER_MODE);
        }

        [IgnoreConsoleJump, DebuggerHidden]
        private static void ErrorEnabled() { EnabledError.Value?.Invoke(null, null); }

        [IgnoreConsoleJump, DebuggerHidden]
        private static void ErrorDisabled() { DisableError.Value?.Invoke(null, null); }

        [IgnoreConsoleJump, DebuggerHidden]
        private static void LogEnabled() { EnabledLog.Value?.Invoke(null, null); }

        [IgnoreConsoleJump, DebuggerHidden]
        private static void LogDisabled() { DisableLog.Value?.Invoke(null, null); }
    }
}
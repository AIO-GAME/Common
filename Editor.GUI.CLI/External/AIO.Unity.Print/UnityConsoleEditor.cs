﻿#region

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

        private static MethodInfo EnabledError;
        private static MethodInfo DisableError;

        private static MethodInfo EnabledLog;
        private static MethodInfo DisableLog;
        private static bool       IS_EDITOR_SWITCH_LOG   => EditorPrefs.GetInt(MENU_EDITOR_SWITCH_LOG, 1) == 1;
        private static bool       IS_EDITOR_SWITCH_ERROR => EditorPrefs.GetInt(MENU_EDITOR_SWITCH_ERROR, -1) == 1;
        private static bool       IS_DEVELOPER_MODE      => EditorPrefs.GetInt(MENU_DEVELOPER_MODE, -1) == 1;

        [LnkTools(
            Tooltip = "支持读取 Console.WriteLine 日志",
            IconResource = "Editor/Icon/Color/-message")]
        private static bool EditorSwitchLog()
        {
            EditorSwitchLOG();
            return IS_EDITOR_SWITCH_LOG;
        }

        [MenuItem(MENU_EDITOR_SWITCH_LOG)]
        private static void EditorSwitchLOG()
        {
            EditorPrefs.SetInt(MENU_EDITOR_SWITCH_LOG, !IS_EDITOR_SWITCH_LOG ? 1 : -1);
            Initialize();
            var refreshSettingsMethodInfo = typeof(AssetDatabase).GetMethod("RefreshSettings",
                                                                            BindingFlags.Static | BindingFlags.Public);
            if (refreshSettingsMethodInfo == null) AssetDatabase.Refresh();
            else refreshSettingsMethodInfo.Invoke(null, null);
        }

        [MenuItem(MENU_EDITOR_SWITCH_ERROR)]
        private static void EditorSwitchERROR()
        {
            EditorPrefs.SetInt(MENU_EDITOR_SWITCH_ERROR, !IS_EDITOR_SWITCH_ERROR ? 1 : -1);
            Initialize();
            var refreshSettingsMethodInfo = typeof(AssetDatabase).GetMethod("RefreshSettings",
                                                                            BindingFlags.Static | BindingFlags.Public);
            if (refreshSettingsMethodInfo == null) AssetDatabase.Refresh();
            else refreshSettingsMethodInfo.Invoke(null, null);
        }

        private static void MenuRefresh()
        {
            if (Menu.GetChecked(MENU_EDITOR_SWITCH_ERROR))
                Menu.SetChecked(MENU_EDITOR_SWITCH_ERROR, IS_EDITOR_SWITCH_ERROR);

            if (Menu.GetChecked(MENU_EDITOR_SWITCH_LOG))
                Menu.SetChecked(MENU_EDITOR_SWITCH_LOG, IS_EDITOR_SWITCH_LOG);

            if (Menu.GetChecked(MENU_DEVELOPER_MODE))
                Menu.SetChecked(MENU_DEVELOPER_MODE, IS_DEVELOPER_MODE);
        }

        [IgnoreConsoleJump]
        private static void ErrorEnabled()
        {
            if (EnabledError is null)
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity")) continue;
                    var type = assembly.GetType("UnityEngine.UnityConsole");
                    if (type is null) continue;
                    EnabledError = type.GetMethod(nameof(EnabledError),
                                                  BindingFlags.Static | BindingFlags.NonPublic);
                    if (EnabledError != null) break;
                }

            EnabledError?.Invoke(null, null);
        }

        [IgnoreConsoleJump]
        private static void ErrorDisabled()
        {
            if (DisableError is null)
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity")) continue;
                    var type = assembly.GetType("UnityEngine.UnityConsole");
                    if (type is null) continue;
                    DisableError = type.GetMethod(nameof(DisableError),
                                                  BindingFlags.Static | BindingFlags.NonPublic);
                    if (DisableError != null) break;
                }

            DisableError?.Invoke(null, null);
        }


        [IgnoreConsoleJump]
        private static void LogEnabled()
        {
            if (EnabledLog is null)
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity")) continue;
                    var type = assembly.GetType("UnityEngine.UnityConsole");
                    if (type is null) continue;
                    EnabledLog = type.GetMethod(nameof(EnabledLog),
                                                BindingFlags.Static | BindingFlags.NonPublic);
                    if (EnabledLog != null) break;
                }

            EnabledLog?.Invoke(null, null);
        }

        [IgnoreConsoleJump]
        private static void LogDisabled()
        {
            if (DisableLog is null)
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.GetName().Name.StartsWith("AIO.Print.Unity")) continue;
                    var type = assembly.GetType("UnityEngine.UnityConsole");
                    if (type is null) continue;
                    DisableLog = type.GetMethod(nameof(DisableLog),
                                                BindingFlags.Static | BindingFlags.NonPublic);
                    if (DisableLog != null) break;
                }

            DisableLog?.Invoke(null, null);
        }

        [AInit(EInitAttrMode.Both, int.MinValue)]
        private static void Initialize()
        {
            if (IS_EDITOR_SWITCH_ERROR) ErrorEnabled();
            else ErrorDisabled();
            if (IS_EDITOR_SWITCH_LOG) LogEnabled();
            else LogDisabled();
            MenuRefresh();
        }

        /// <summary>
        /// 打开开发者模式
        /// </summary>
        [MenuItem(MENU_DEVELOPER_MODE, false, 0)]
        private static void OpenDeveloperMode()
        {
            EditorPrefs.SetBool("DeveloperMode", !IS_DEVELOPER_MODE);
            EditorPrefs.SetInt(MENU_DEVELOPER_MODE, !IS_DEVELOPER_MODE ? 1 : -1);
            MenuRefresh();
        }
    }
}
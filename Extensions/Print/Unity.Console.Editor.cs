/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2023-01-08                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    internal static class UnityConsoleEditor
    {
        private static bool IS_EDITOR_SWITCH_LOG => EditorPrefs.GetInt(MENU_EDITOR_SWITCH_LOG, -1) == 1;
        private static bool IS_EDITOR_SWITCH_ERROR => EditorPrefs.GetInt(MENU_EDITOR_SWITCH_ERROR, -1) == 1;
        private static bool IS_DEVELOPER_MODE => EditorPrefs.GetInt(MENU_DEVELOPER_MODE, -1) == 1;

        /// <summary>
        /// 输出日志
        /// </summary>
        private const string MENU_DEVELOPER_MODE = "Tools/Debug/Developer Mode";

        /// <summary>
        /// 错误日志
        /// </summary>
        private const string MENU_EDITOR_SWITCH_LOG = "Tools/Debug/Console Log";

        /// <summary>
        /// 开发者模式
        /// </summary>
        private const string MENU_EDITOR_SWITCH_ERROR = "Tools/Debug/Console Error";

        [MenuItem(MENU_EDITOR_SWITCH_LOG)]
        private static void EditorSwitchLOG()
        {
            EditorPrefs.SetInt(MENU_EDITOR_SWITCH_LOG, !IS_EDITOR_SWITCH_LOG ? 1 : -1);
            EditorProxy();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#else
            AssetDatabase.Refresh();
#endif
        }

        [MenuItem(MENU_EDITOR_SWITCH_ERROR)]
        private static void EditorSwitchERROR()
        {
            EditorPrefs.SetInt(MENU_EDITOR_SWITCH_ERROR, !IS_EDITOR_SWITCH_ERROR ? 1 : -1);
            EditorProxy();
#if UNITY_2020_1_OR_NEWER
            AssetDatabase.RefreshSettings();
#else
            AssetDatabase.Refresh();
#endif
        }

        private static void MenuRefresh()
        {
            Menu.SetChecked(MENU_EDITOR_SWITCH_ERROR, IS_EDITOR_SWITCH_ERROR);
            Menu.SetChecked(MENU_EDITOR_SWITCH_LOG, IS_EDITOR_SWITCH_LOG);
            Menu.SetChecked(MENU_DEVELOPER_MODE, IS_DEVELOPER_MODE);
        }

        [InitializeOnLoadMethod]
        [RuntimeInitializeOnLoadMethod]
        private static void EditorProxy()
        {
            MenuRefresh();

            if (IS_EDITOR_SWITCH_ERROR) UnityConsole.EnabledError();
            else UnityConsole.DisableError();
            if (IS_EDITOR_SWITCH_LOG) UnityConsole.EnabledLog();
            else UnityConsole.DisableLog();
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